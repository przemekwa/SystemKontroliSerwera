using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;


namespace PingIVR
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new PingSenderModel();

            if (args.Length != 0 && args.Length >= 3)
            {
                model.IpAddress = args[0];
                model.Option = args[1];
                model.Parametr = args[2];

                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].Contains("-t"))
                    {
                        var pingTime = long.Parse(args[i + 1]);
                    }

                    model.IslogEnabled = args[i].Contains("-l");
                }

                Console.WriteLine("Inicjalizacja SystemuKontroliSerwera " + args[0]);
                Console.WriteLine("Opcja:  -l (logownie Ping do pliku)");
                Console.WriteLine("Opcja:  -t {czas w milisekundach}  (odstępy między Ping-ami)");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("W razie problemów skorzystam z " + args[1]);
                Console.WriteLine("Powiadomie: " + args[2]);
                Console.WriteLine("Logi: " + model.IslogEnabled.ToString());
                Console.WriteLine("Czas: " + Consts.PING_TIME.ToString());
                Console.WriteLine("Ilość zdarzeń: " + Consts.MAX_ALERT_COUNT.ToString());
                Console.WriteLine("-----------------------------------------------");
            }
            else
            {
                Console.WriteLine("Błądne parametry ");
                Console.WriteLine("SystemuKontroliSerwera.exe {ASSET|IP SERWERA} {POCZTA|SMS} {e-mail|numer telefonu} {logowanie: true|false} ");
                Console.WriteLine("Opcja:  -l (logownie Ping do pliku)");
                Console.WriteLine("Opcja:  -t {czas w milisekundach}  (odstępy między Ping-ami)");
                Console.Read();
            }

            model.KomunikatAwaria = true;
            model.NapiszKomunikat = true;
            model.UseOption = true;

            CreateTimer(model);
        }

        private static void CreateTimer(PingSenderModel model)
        {
            var pingSender = new PingSender(model);

            var timerCallback = new TimerCallback(pingSender.SendPing);

            using (var timer = new Timer(timerCallback))
            {
                timer.Change(0, Consts.PING_TIME);
                Console.Read();
            }
        }
    }
}
