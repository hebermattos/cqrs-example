using Nest;

namespace Model;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class After
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
}

public class Before
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
}

public class ProductElastic
{
    public Before before { get; set; }
    public After after { get; set; }
    public Source source { get; set; }
    public string op { get; set; }
    public object ts_ms { get; set; }
    public object transaction { get; set; }
}

public class Source
{
    public string version { get; set; }
    public string connector { get; set; }
    public string name { get; set; }
    public object ts_ms { get; set; }
    public string snapshot { get; set; }
    public string db { get; set; }
    public object sequence { get; set; }
    public string schema { get; set; }
    public string table { get; set; }
    public string change_lsn { get; set; }
    public string commit_lsn { get; set; }
    public int event_serial_no { get; set; }
}




