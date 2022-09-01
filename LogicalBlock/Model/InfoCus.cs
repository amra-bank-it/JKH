using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalBlock.Model
{
    public class InfoCus
    {
        [JsonProperty("DebtElev")]
        public double DebtElev { get; set; }

        [JsonProperty("DebtApart")]
        public double DebtApart { get; set; }

        [JsonProperty("Master")]
        public string Master { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("ControlHouse")]
        public string ControlHouse { get; set; }

        [JsonProperty("AccRKC")]
        public string AccRKC { get; set; }

        [JsonProperty("LcHous")]
        public string LcHous { get; set; }

        [JsonProperty("DatePay")]
        public string DatePay { get; set; }

        [JsonProperty("INN")]
        public string INN { get; set; }

        [JsonProperty("KPP")]
        public string KPP { get; set; }
    }
}