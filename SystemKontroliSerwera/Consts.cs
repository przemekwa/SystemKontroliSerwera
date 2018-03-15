using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingIVR
{
    public enum AlertType
    {
        SMS,
        MAIL
    };

    public static class Consts
    {
        public const int MAX_ALERT_COUNT = 3;
        public const long PING_TIME = 10000;
    }
}
