using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalBlock.Model
{
    public class ResponsePayment
    {
        [JsonProperty("AccFace")]
        public string AccFace { get; set; }

        [JsonProperty("Summa")]
        public string Summa { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}