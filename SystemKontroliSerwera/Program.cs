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
        static string IPAddress;
        static string option = "";
        static string parametr = "";
        static bool islogEnabled = false;
        static bool useOption = true;
        static bool napiszKomunikat = true;
        static bool komunikatAwaria = true;
        static int MAX_ILOSC_ZDARZEN = 3;
        static int iloscZdarzen = 0;
        static long czasWyslaniaPinga = 10000;

        static void Main(string[] args)
        {

            if (args.Length != 0  && args.Length >= 3)
            {
                
                IPAddress = args[0];
                option = args[1];
                parametr = args[2];

                for (int i=1;i<args.Length;i++)
                {
                    if (args[i].Contains("-t"))
                    {
                        czasWyslaniaPinga = long.Parse(args[i+1]);
                    }

                    if (args[i].Contains("-l"))
                    {
                        islogEnabled = true;
                    }

                }
            

                Console.WriteLine("Inicjalizacja SystemuKontroliSerwera " + args[0]);
                Console.WriteLine("Opcja:  -l (logownie Ping do pliku)");
                Console.WriteLine("Opcja:  -t {czas w milisekundach}  (odstępy między Ping-ami)");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("W razie problemów skorzystam z " + args[1]);
                Console.WriteLine("Powiadomie: " + args[2]);
                Console.WriteLine("Logi: " + islogEnabled.ToString());
                Console.WriteLine("Czas: " + czasWyslaniaPinga.ToString());
                Console.WriteLine("Ilość zdarzeń: " + MAX_ILOSC_ZDARZEN.ToString());

                
               

                Console.WriteLine("-----------------------------------------------");


                TimerCallback tcb = new TimerCallback(WysliPing);
                Timer wyzwalacz = new Timer(tcb);
                wyzwalacz.Change(0, czasWyslaniaPinga);
                Console.Read();
                wyzwalacz.Dispose();
            }
            else
            {
                Console.WriteLine("Błądne parametry ");
                Console.WriteLine("SystemuKontroliSerwera.exe {ASSET|IP SERWERA} {POCZTA|SMS} {e-mail|numer telefonu} {logowanie: true|false} ");
                Console.WriteLine("Opcja:  -l (logownie Ping do pliku)");
                Console.WriteLine("Opcja:  -t {czas w milisekundach}  (odstępy między Ping-ami)");
                Console.Read();    
            }
        }

        static void WysliPing(object adres)
        {
            Ping pingIVR = new Ping();
            PingReply odpowiedzIVR = null;

            try
            {
                odpowiedzIVR = pingIVR.Send(IPAddress);

                if (islogEnabled)
                {
                    using (StreamWriter plik = new StreamWriter("logi_" + IPAddress + ".log", true))
                    {
                        plik.WriteLine("{0} - {1} - {2}", DateTime.Now, odpowiedzIVR.Status, odpowiedzIVR.RoundtripTime);
                        
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
            }


            switch (odpowiedzIVR.Status)
            {

                case IPStatus.Success:
                    useOption = true;
                    komunikatAwaria = true;
                    if (napiszKomunikat)
                    {
                        Console.WriteLine("{0} {1} {2}", DateTime.Now, "Działa", IPAddress);
                        napiszKomunikat = false;
                    }
                    break;

                default:

                    
                    napiszKomunikat = true;

                    if (komunikatAwaria)
                    {

                        Console.WriteLine("{0} {1} {2} {3} {4}", DateTime.Now, "Nie dziala maszyna/serwer", IPAddress, odpowiedzIVR.Status,"IVR-POZ-Numer 1650. IVR-SRO-Numer 1750");

                        if (MAX_ILOSC_ZDARZEN > iloscZdarzen)
                        {

                            if (option == "sms" && useOption)
                            {
                                //sms s = new sms();
                                //s.WysliSms(adresIP + " nie działa", parametr);
                                useOption = false;
                                Console.WriteLine("{0} {1}", DateTime.Now, "Wysłałem sms na numer " + parametr);
                                iloscZdarzen++;
                            }

                            if (option == "poczta" && useOption)
                            {
                                //Poczta.SendMailServiceImplClient p = new Poczta.SendMailServiceImplClient();
                                //p.SendMail("przemyslaw.walkowski@bzwbk.pl", parametr, "Aplikacja Systemu Kontroli Serwera", adresIP + " nie odpowiada ping");
                                useOption = false;
                                Console.WriteLine("{0} {1}", DateTime.Now, "Wysłałem meila do " + parametr);
                                iloscZdarzen++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("{0} {1} {2}", DateTime.Now, "Osiagnięto limit zdarzeń:", iloscZdarzen);
                        }
                    }
                    komunikatAwaria = false;
                    break;
            }
        }
    }
}
