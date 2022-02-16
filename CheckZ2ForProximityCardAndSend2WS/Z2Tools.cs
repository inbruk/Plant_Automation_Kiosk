using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

using ZREADER;
using ZPort;


namespace CheckZ2ForProximityCardAndSend2WS
{
    public class Z2Tools // : IDisposable
    {
        private string _RdPortName;
        public string ReadPortName { get { return _RdPortName; } }

        private const ZP_PORT_TYPE _RdPortType = ZP_PORT_TYPE.ZP_PORT_COM;
        private readonly string[] _ReaderTypeStrs = { "Неизвестно", "Z-2 USB", "Matrix III Rd-All", "Z-2 USB MF", "Matrix III Net", "CP-Z-2MF", "Z-2 EHR", "Z-2 Base", "Matrix V" };
        private readonly string[] _CardTypeStrs = { "Неизвестно", "EM", "HID", "IC", "UL", "1K", "4K", "DF", "PX",
                                                    "Cod433 Fix", "Cod433", "Dallas", "CAME", "Plus", "Plus 1K", "Plus 2K", "Plus 4K", "Mini" };
        private IntPtr m_hRd; // текущий считыватель

        private bool _ToolStarted = false;
        public bool ToolStarted { get { return _ToolStarted; } }

        private bool _ReaderAttached = false;
        public bool ReaderAttached { get { return _ReaderAttached; } }
        
        private string _CardNumber = null;
        public string CardNumber { get { return _CardNumber; } }

        private void InitializeTools()
        {
            Logger.Log.Info("Начинаем инициализацию утилит работы со считывателем Z-2 USB");

            try
            {
                int hr;
                hr = ZRIntf.ZR_Initialize(ZPIntf.ZP_IF_NO_MSG_LOOP);
                if (hr < 0)
                {
                    Logger.Log.Error("Ошибка ZR_Initialize " + hr.ToString());
                    _ToolStarted = false;
                }
                else
                {
                    _ToolStarted = true;
                }
            }
            catch
            {
                Logger.Log.Error("Инициализация утилит работы со считывателем Z-2 USB: неожиданное исключение !");
            }
        }

        private void DeinitializeTools()
        {
            Logger.Log.Info("Заканчиваем работу утилит со считывателем Z-2 USB");

            if (_ReaderAttached)
                DeattachFromReader();

            try
            {
                if (m_hRd != IntPtr.Zero)
                    ZRIntf.ZR_CloseHandle(m_hRd);
                ZRIntf.ZR_Finalyze();

                _ToolStarted = false;
            }
            catch
            {
                Logger.Log.Error("Деинициализация утилит работы со считывателем Z-2 USB: неожиданное исключение !");
            }
        }

        private void AttachToReader()
        {
            if (_ReaderAttached == true) return; // не подключаемся второй раз

            if (_ToolStarted == false)
                InitializeTools();

            try
            {
                int hr;
                ZR_RD_INFO rRdInf = new ZR_RD_INFO();
                Logger.Log.Info("Начинаем подключение к считывателю " + _RdPortName.ToString());
                ZR_RD_OPEN_PARAMS rOpen = new ZR_RD_OPEN_PARAMS(_RdPortType, _RdPortName);
                hr = ZRIntf.ZR_Rd_Open(ref m_hRd, ref rOpen, ref rRdInf);
                if (hr < 0)
                {
                    Logger.Log.Error("Ошибка ZR_Rd_Open " + hr.ToString());
                    _ReaderAttached = false;
                    return;
                }

                _ReaderAttached = true;
                Logger.Log.Info
                (
                    "Подключение к считывателю завершено успешно. " +
                    "тип:" + _ReaderTypeStrs[(int)rRdInf.nType].ToString() + ", " +
                    "с/н:" + rRdInf.rBase.nSn.ToString() + ", " +
                    "вер. ст:" + (rRdInf.rBase.nVersion & 0xff).ToString() + ", " +
                    "вер. мл:" + ((rRdInf.rBase.nVersion >> 8) & 0xff).ToString()
                );
            }
            catch
            {
                Logger.Log.Error("Открытие считывателя: неожиданное исключение !");
                _ReaderAttached = false;
            }
        }

        private void DeattachFromReader()
        {
            if (_ReaderAttached == false)
            {
                Logger.Log.Error("Нельзя отсоединиться от считывателя не присоединившись к нему");
                return;
            }

            try
            {
                Logger.Log.Info("Начинаем отключение от считывателя " + _RdPortName.ToString());
                ZR_RD_OPEN_PARAMS rOpen = new ZR_RD_OPEN_PARAMS(_RdPortType, _RdPortName);
                int hr = ZRIntf.ZR_CloseHandle(m_hRd);                   
                if (hr < 0)
                {
                    Logger.Log.Error("Ошибка ZR_CloseHandle " + hr.ToString());
                    return;
                }

                Logger.Log.Info("Отключение от считывателя завершено успешно" + _RdPortName.ToString());
                return;
            }
            catch
            {
                Logger.Log.Error("Отключение от считывателя: неожиданное исключение !");
                return;
            }
        }

        private ManualResetEvent m_oEvent = null;
        private bool m_fThreadActive;
        private Thread m_oThread = null;

        private void DoNotifyWork()
        {
            while (m_fThreadActive)
            {
                if (m_oEvent.WaitOne())
                {
                    m_oEvent.Reset();
                    if (m_hRd != IntPtr.Zero)
                        CheckNotifyMsgs();
                }
            }
        }

        private void StartNotifyThread()
        {
            if (m_oThread != null)
                return;
            m_fThreadActive = true;
            m_oThread = new Thread(DoNotifyWork);
            m_oThread.Start();
        }

        private void StopNotifyThread()
        {
            if (m_oThread == null)
                return;
            m_fThreadActive = false;
            m_oEvent.Set();
            m_oThread.Join();
            m_oThread = null;
        }

        public delegate void ProcessCardMovement();
        public ProcessCardMovement CurrentProcessCardMovementHandler { set; get; }

        private int CheckNotifyMsgs()
        {
            int hr;
            UInt32 nMsg = 0;
            IntPtr nMsgParam = IntPtr.Zero;
            while ((hr = ZRIntf.ZR_Rd_GetNextMessage(m_hRd, ref nMsg, ref nMsgParam)) == ZRIntf.S_OK)
            {
                switch (nMsg)
                {
                    case ZRIntf.ZR_RN_CARD_INSERT:
                    {
                        ZR_CARD_INFO pInfo = (ZR_CARD_INFO)Marshal.PtrToStructure(nMsgParam, typeof(ZR_CARD_INFO));
                        _CardNumber = ZRIntf.CardNumToStr(pInfo.nNum, pInfo.nType);
                        string cardType = _CardTypeStrs[(int)pInfo.nType];

                        Logger.Log.Info("Проверка карт: Сейчас к считывателю приложили карту с типом " + cardType + " и номером " + _CardNumber);
                        CurrentProcessCardMovementHandler();
                    }
                    break;
                    case ZRIntf.ZR_RN_CARD_REMOVE:
                    {
                        _CardNumber = null;

                        ZR_CARD_INFO pInfo = (ZR_CARD_INFO)Marshal.PtrToStructure(nMsgParam, typeof(ZR_CARD_INFO));
                        string cardNumber = ZRIntf.CardNumToStr(pInfo.nNum, pInfo.nType);
                        string cardType = _CardTypeStrs[(int)pInfo.nType];

                        Logger.Log.Info("Проверка карт: Сейчас от считывателя убрали карту с типом " + cardType + " и номером " + cardNumber);
                        CurrentProcessCardMovementHandler();
                    }
                    break;
                }
            }

            if (hr == ZPIntf.ZP_S_NOTFOUND)
                hr = ZRIntf.S_OK;
            return hr;
        }

        /// <summary>
        /// Инициализируем средства работы с картами 
        /// </summary>
        /// <param name="comPStr"> строка с виртуальным ком портом, к которому подключен считыватель карт, например "COM3"</param>
        public Z2Tools(string comPStr)
        {
            _RdPortName = comPStr;
            InitializeTools();
            AttachToReader();

            // инициализируем задачу, которая будет срабатывать при поднесении, удалении карты
            m_oEvent = new ManualResetEvent(false);
            ZR_RD_NOTIFY_SETTINGS rNS = new ZR_RD_NOTIFY_SETTINGS(ZRIntf.ZR_RNF_EXIST_CARD, m_oEvent.SafeWaitHandle);
            int hr = ZRIntf.ZR_Rd_SetNotification(m_hRd, rNS);
            if (hr < 0)
            {
                Logger.Log.Error("Ошибка ZR_Rd_SetNotification " + hr.ToString());                
                return;
            }

            StartNotifyThread();
        }

        /// <summary>
        /// Отключаем средства работы с картами
        /// </summary>
        public void DoneCardTools()
        {
            StopNotifyThread();
            DeattachFromReader();
            DeinitializeTools();
        }        
    }
}
