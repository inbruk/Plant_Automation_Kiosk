using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;

namespace Test4ProximityCarRequest
{
    public class PassLeanLog
    {
        public int KioskNumber { get; set; }
        public string PassNumber { get; set; }
        public DateTime Time { get; set; }
    }

    class Program
    {
        private static HttpClient client = new HttpClient();
        private static String _commonURIPart = null;

        private static string CreatePassLean(PassLeanLog passLean)
        {
            String requestUri = _commonURIPart +
                "(Kiosknumber=" + passLean.KioskNumber + ",Passnumber='" + passLean.PassNumber + "')?$format=json";
            HttpResponseMessage response = client.GetAsync(requestUri).Result;

            Console.WriteLine(requestUri);
            Console.WriteLine(response.StatusCode.ToString() + ":" + response.RequestMessage.ToString() );
            Console.ReadKey();

            return response.StatusCode.ToString();
        }

        static void Main(string[] args)
        {
            _commonURIPart = ConfigurationManager.AppSettings["ODataV4ServiceURI"];
            if (String.IsNullOrWhiteSpace(_commonURIPart)==false)
            {
                PassLeanLog currPassLean = new PassLeanLog()
                {
                    KioskNumber = 1,
                    PassNumber = "128,44633"
                };

                CreatePassLean(currPassLean);
            }
        }
    }
}
