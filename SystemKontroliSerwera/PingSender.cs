using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace PingIVR
{
    public class PingSender
    {
        private readonly PingSenderModel pingSenderModel;
        private int currentAlertCount;

        public PingSender(PingSenderModel pingSenderModel)
        {
            this.pingSenderModel = pingSenderModel;
        }

        public void SendPing(object adres)
        {
            var ping = new Ping();

            var reply = ping.Send(pingSenderModel.IpAddress);

            ProcessReply(reply);
            LogReply(reply);
        }

        private void LogReply(PingReply reply)
        {
            if (pingSenderModel.IslogEnabled)
            {
                using (StreamWriter plik = new StreamWriter("logi_" + pingSenderModel.IpAddress + ".log", true))
                {
                    plik.WriteLine("{0} - {1} - {2}", DateTime.Now, reply.Status, reply.RoundtripTime);
                }
            }
        }

        private void ProcessReply(PingReply reply)
        {
            switch (reply.Status)
            {
                case IPStatus.Success:
                    ProcessSuccess();
                    break;
                default:
                    ProcessOtherStatus(reply.Status);
                    break;
            }
        }

        private void ProcessOtherStatus(IPStatus iPStatus)
        {
            pingSenderModel.WriteMsg = true;

            if (pingSenderModel.IsAlertMsg == false)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Ping do {pingSenderModel.IpAddress} nie odpowiada. Rezultat: {iPStatus}");

            if (Consts.MAX_ALERT_COUNT > currentAlertCount)
            {
                SendAlert();
                return;
            }

            Console.WriteLine("{0} {1} {2}", DateTime.Now, "Osiagnięto limit zdarzeń:", currentAlertCount);
        }

        private void SendAlert()
        {
            if (pingSenderModel.Option == AlertType.SMS && pingSenderModel.UseOption)
            {
                //sms s = new sms();
                //s.WysliSms(adresIP + " nie działa", parametr);
                pingSenderModel.UseOption = false;
                Console.WriteLine("{0} {1}", DateTime.Now, "Wysłałem sms na numer " + pingSenderModel.Parametr);
                currentAlertCount++;
            }

            if (pingSenderModel.Option == AlertType.MAIL && pingSenderModel.UseOption)
            {
                //Poczta.SendMailServiceImplClient p = new Poczta.SendMailServiceImplClient();
                //p.SendMail("przemyslaw.walkowski@bzwbk.pl", parametr, "Aplikacja Systemu Kontroli Serwera", adresIP + " nie odpowiada ping");
                pingSenderModel.UseOption = false;
                Console.WriteLine("{0} {1}", DateTime.Now, "Wysłałem meila do " + pingSenderModel.Parametr);
                currentAlertCount++;
            }
        }

        private void ProcessSuccess()
        {
            pingSenderModel.UseOption = true;
            pingSenderModel.IsAlertMsg = true;

            if (pingSenderModel.WriteMsg)
            {
                Console.WriteLine("{0} {1} {2}", DateTime.Now, "Działa", pingSenderModel.IpAddress);
                pingSenderModel.WriteMsg = false;
            }
        }
    }
}
