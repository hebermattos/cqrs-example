using Newtonsoft.Json;

namespace Model;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class After
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }

    public class Field
    {
        public string type { get; set; }
        public List<Field> fields { get; set; }
        public bool optional { get; set; }
        public string name { get; set; }
        public string field { get; set; }
        public int? version { get; set; }
        public Parameters parameters { get; set; }
        public string @default { get; set; }
    }

    public class Parameters
    {
        public string scale { get; set; }

        [JsonProperty("connect.decimal.precision")]
        public string connectdecimalprecision { get; set; }
        public string allowed { get; set; }
    }

    public class Payload
    {
        public object before { get; set; }
        public After after { get; set; }
        public Source source { get; set; }
        public string op { get; set; }
        public long ts_ms { get; set; }
        public object transaction { get; set; }
    }

    public class ProductElastic
    {
        public Schema schema { get; set; }
        public Payload payload { get; set; }
    }

    public class Schema
    {
        public string type { get; set; }
        public List<Field> fields { get; set; }
        public bool optional { get; set; }
        public string name { get; set; }
    }

    public class Source
    {
        public string version { get; set; }
        public string connector { get; set; }
        public string name { get; set; }
        public long ts_ms { get; set; }
        public string snapshot { get; set; }
        public string db { get; set; }
        public object sequence { get; set; }
        public string schema { get; set; }
        public string table { get; set; }
        public string change_lsn { get; set; }
        public string commit_lsn { get; set; }
        public int event_serial_no { get; set; }
    }




