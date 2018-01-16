using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dlpziplib
{
    public class DLPZipConfig
    {
        [JsonProperty("configRefreshPeriod")]
        public uint configRefreshPeriod { get; set; }

        [JsonProperty("defaultKey")]
        public String defaultKey { get; set; }
    }
}
