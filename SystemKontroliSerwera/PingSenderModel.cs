using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingIVR
{
    public class PingSenderModel
    {
        public string IpAddress { get; internal set; }
        public bool IslogEnabled { get; internal set; }
        public bool UseOption { get; internal set; }

        public bool IsAlertMsg { get; internal set; }
        public bool WriteMsg { get; internal set; }
        public AlertType Option { get; internal set; }
        public string Parametr { get; internal set; }
    }
}
