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
        public bool KomunikatAwaria { get; internal set; }
        public bool NapiszKomunikat { get; internal set; }
        public string Option { get; internal set; }
        public string Parametr { get; internal set; }
    }
}
