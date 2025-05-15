using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKILSAPRFC.Model.ViewModels
{
    public class mdl_UOM
    {
        public class UOMExtractItem
        {
            [JsonProperty("User Def Code")]
            public string UserDefCode { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }
        }

        public class TKILSAPRFCDBREQ_UOMExtractResponse
        {
            [JsonProperty("TKILSAPRFCDBREQ_UOMExtract")]
            public List<UOMExtractItem> TKILSAPRFCDBREQ_UOMExtract { get; set; }

            [JsonProperty("jde__status")]
            public string JdeStatus { get; set; }

            [JsonProperty("jde__startTimestamp")]
            public DateTime JdeStartTimestamp { get; set; }

            [JsonProperty("jde__endTimestamp")]
            public DateTime JdeEndTimestamp { get; set; }

            [JsonProperty("jde__serverExecutionSeconds")]
            public double JdeServerExecutionSeconds { get; set; }
        }
    }
}
