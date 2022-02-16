using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CheckZ2ForProximityCardAndSend2WS
{
    public partial class Form1 : Form
    {
        private Z2Tools z2Tools;
        private CallODataTools callODataTools;
        private int _kioskNumber;
        private string _comPortName;
        private string _OData4ServURI;
        private bool _IsNeed2StartTestRequestSend = false;
        private string _StartTestRequestPassNumber;

        private void LoadConfiguration()
        {
            string kioskNumberStr = ConfigurationManager.AppSettings["KioskNumber"];
            if (int.TryParse(kioskNumberStr, out _kioskNumber) == false)
            {
                Logger.Log.Error("Ошибка конфигурации: неверно задан параметр configuration//appSettings//KioskNumber !");
            }

            _comPortName = ConfigurationManager.AppSettings["COMPortName"];
            if (String.IsNullOrWhiteSpace(_comPortName) || _comPortName.Contains("COM") == false)
            {
                Logger.Log.Error("Ошибка конфигурации: неверно задан параметр configuration//appSettings//COMPortName !");
            }

            _OData4ServURI = ConfigurationManager.AppSettings["ODataV4ServiceURI"];
            if ( String.IsNullOrWhiteSpace(_OData4ServURI) )
            {
                Logger.Log.Error("Ошибка конфигурации: неверно задан параметр configuration//appSettings//ODataV4ServiceURI !");
            }

            
            string StartTestRequestSendStr =  ConfigurationManager.AppSettings["StartTestRequestSend"];
            if (string.IsNullOrWhiteSpace(StartTestRequestSendStr))
            {
                Logger.Log.Error("Ошибка конфигурации: неверно задан параметр configuration//appSettings//StartTestRequestSend !");
            }
            if( StartTestRequestSendStr=="true" )
            {
                _IsNeed2StartTestRequestSend = true;
            }

            _StartTestRequestPassNumber = ConfigurationManager.AppSettings["StartTestRequestPassNumber"];
            if (String.IsNullOrWhiteSpace(_StartTestRequestPassNumber) == true )
            {
                Logger.Log.Error("Ошибка конфигурации: неверно задан параметр configuration//appSettings//StartTestRequestPassNumber !");
            }
        }

        private void ProcessCardReading() // внимание 
        {
            if (txbKioskNumber.InvokeRequired)
            {
                txbKioskNumber.BeginInvoke((MethodInvoker)(() => FillTheForm()));
                if (z2Tools.CardNumber != null)
                {
                    txbKioskNumber.BeginInvoke((MethodInvoker)(() => SendPassLeanRequestToODataService()));
                }
            }
            else
            {
                FillTheForm();
                if (z2Tools.CardNumber != null)
                {
                    SendPassLeanRequestToODataService();
                }
            }
        }

        private void FillTheForm()
        {
            txbKioskNumber.Text = _kioskNumber.ToString();
            txbVirtCOMPort.Text = z2Tools.ReadPortName;

            if (z2Tools.ToolStarted)
                txbToolsInitialized.Text = "Да";
            else
                txbToolsInitialized.Text = "Нет";

            if (z2Tools.ReaderAttached)
                txbReaderPlugged.Text = "Да";
            else
                txbReaderPlugged.Text = "Нет";

            if (z2Tools.CardNumber == null)
            {
                txbCardNumber.Text = "отсутствует";
                // DEBUG ONLY ------------------------------------------------------
                //Logger.Log.Info
                //(
                //    "ProcessCardReading: сейчас к считывателю карт не приложено, " + 
                //    "киоск номер " + _kioskNumber.ToString() + ", " +
                //    "имя вирт. COM порта " + z2Tools.ReadPortName + ", " +
                //    "средства инициализированы " + txbToolsInitialized.Text + ", " +
                //    "считыватель присоединен " + txbReaderPlugged.Text 
                //);
                // DEBUG ONLY ------------------------------------------------------
            }
            else
            {
                txbCardNumber.Text = z2Tools.CardNumber;
                Logger.Log.Info("Form1.ProcessCardReading: сейчас к считывателю была приложена карта номер " + z2Tools.CardNumber);
            }
        }

        private void SendPassLeanRequestToODataService()
        {
            CallODataTools.SendRequest(_kioskNumber, z2Tools.CardNumber, DateTime.Now );
        }

        public Form1()
        {
            components = new Container();

            InitializeComponent();
            LoadConfiguration();

            z2Tools = new Z2Tools(_comPortName);
            z2Tools.CurrentProcessCardMovementHandler = ProcessCardReading;

            callODataTools = new CallODataTools(_OData4ServURI);

            if( _IsNeed2StartTestRequestSend==true)
            {
                CallODataTools.SendRequest(_kioskNumber, _StartTestRequestPassNumber, DateTime.Now);
            }
        }

        // стартовать задачу считывателя можно только после полной загрузки формы
        private void Form1_Shown(object sender, EventArgs e)
        {
            FillTheForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            z2Tools.DoneCardTools();
            Environment.Exit(Environment.ExitCode);
        }
    }
}
