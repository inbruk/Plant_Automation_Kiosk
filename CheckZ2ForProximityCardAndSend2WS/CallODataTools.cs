using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace CheckZ2ForProximityCardAndSend2WS
{
    public class CallODataTools
    {
        private class PassLeanLog
        {
            public int KioskNumber { get; set; }
            public string PassNumber { get; set; }
            public DateTime Time { get; set; }
        }

        private static String _commonURIPart;
        private static HttpClient client;

        public CallODataTools(string servUri)
        {
            client = new HttpClient();
            _commonURIPart = servUri;
        }

        //private static string CreatePassLean(PassLeanLog passLean)
        //{
        //    HttpResponseMessage response = client.PostAsJsonAsync(_commonURIPart, passLean).Result;
        //    return response.StatusCode.ToString();
        //}

        // http://ktzsapas06.powerm.ru:8000/sap/opu/odata/sap/ZPASSLEANLOG_SRV_02/zpassleanlogSet(Kiosknumber=1,Passnumber='128,44633')?$format=json

        private static string CreatePassLean(PassLeanLog passLean)
        {
            String requestUri = _commonURIPart +
                "(Kiosknumber=" + passLean.KioskNumber + ",Passnumber='" + passLean.PassNumber + "')?$format=json";
            HttpResponseMessage response = client.GetAsync(requestUri).Result;
            return response.StatusCode.ToString();
        }
               
        private static void RunServiceRequestAsync(PassLeanLog passLean)
        {
            Logger.Log.Info
            (
                "Отправаляем запрос о поднесении пропуска на SAP сервис. "+
                "URI сервиса: " + _commonURIPart + " " +
                "номер киоска: " + passLean.KioskNumber + " " +
                "номер пропуска: " + passLean.PassNumber + " " +
                "дата и время приближения: " + passLean.Time
            );

            try
            {
                string statusCode = CreatePassLean(passLean);
                Logger.Log.Info( "Статус код ответа на запрос о поднесении пропуска на SAP сервис: " + statusCode );
            }
            catch(Exception ex)
            {
                Logger.Log.Info("Во время отправки запрос о поднесении пропуска на SAP сервис произошло исключение: " + ex.ToString() );
            }
        }

        public static void SendRequest(int kioskNumber, string passNumber, DateTime time)
        {
            PassLeanLog passLean = new PassLeanLog()
            {
                KioskNumber = kioskNumber,
                PassNumber = passNumber,
                Time = time
            };

            RunServiceRequestAsync(passLean);
        }

    }
}
