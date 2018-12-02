using Newtonsoft.Json;

namespace function_shelly3
{
    public class data
    {
        [JsonProperty(PropertyName="@type")]
        public string type { get; set; }
        public string optContext { get; set; }
        public string[] permissions { get; set; }
        public data()
        {
        }
    }
}