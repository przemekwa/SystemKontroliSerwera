using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace PingIVR
{
    class sms
    {
        public void WysliSms(string tekst, string telefony)
        {
            string[] numery = telefony.Split(';');

            foreach (string numer in numery)
            {
                String URI = "http://10.50.36.10:9080/EasyAdmin/SmsSender?login=FRAUD&password=dominika486&remote_id=1234&class0=0&phone=48" + numer + "&text=" + tekst;
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(URI);
            }

           
        }

    }


}
