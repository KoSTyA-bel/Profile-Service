namespace ProfilerService.BLL.Entities;

public class WaxApiResponse
{
    public bool success { get; set; }
    public Datum[] data { get; set; }
    public long query_time { get; set; }
}

public class Datum
{
    public string contract { get; set; }
    public string asset_id { get; set; }
    public string owner { get; set; }
    public bool is_transferable { get; set; }
    public bool is_burnable { get; set; }
    public Collection collection { get; set; }
    public Schema schema { get; set; }
    public Template template { get; set; }
    public Mutable_Data mutable_data { get; set; }
    public Immutable_Data1 immutable_data { get; set; }
    public string template_mint { get; set; }
    public object[] backed_tokens { get; set; }
    public object burned_by_account { get; set; }
    public object burned_at_block { get; set; }
    public object burned_at_time { get; set; }
    public string updated_at_block { get; set; }
    public string updated_at_time { get; set; }
    public string transferred_at_block { get; set; }
    public string transferred_at_time { get; set; }
    public string minted_at_block { get; set; }
    public string minted_at_time { get; set; }
    public Data data { get; set; }
    public string name { get; set; }
}

public class Collection
{
    public string collection_name { get; set; }
    public string name { get; set; }
    public string img { get; set; }
    public string author { get; set; }
    public bool allow_notify { get; set; }
    public string[] authorized_accounts { get; set; }
    public string[] notify_accounts { get; set; }
    public float market_fee { get; set; }
    public string created_at_block { get; set; }
    public string created_at_time { get; set; }
}

public class Schema
{
    public string schema_name { get; set; }
    public Format[] format { get; set; }
    public string created_at_block { get; set; }
    public string created_at_time { get; set; }
}

public class Format
{
    public string name { get; set; }
    public string type { get; set; }
}

public class Template
{
    public string template_id { get; set; }
    public string max_supply { get; set; }
    public bool is_transferable { get; set; }
    public bool is_burnable { get; set; }
    public string issued_supply { get; set; }
    public Immutable_Data immutable_data { get; set; }
    public string created_at_time { get; set; }
    public string created_at_block { get; set; }
}

public class Immutable_Data
{
    public string img { get; set; }
    public string name { get; set; }
    public string unpack { get; set; }
    public string description { get; set; }
    public int _class { get; set; }
    public string rarity { get; set; }
    public string set { get; set; }
    public string discord { get; set; }
    public string website { get; set; }
    public string legal { get; set; }
    public string video { get; set; }
}

public class Mutable_Data
{
}

public class Immutable_Data1
{
}

public class Data
{
    public string img { get; set; }
    public string name { get; set; }
    public string unpack { get; set; }
    public string description { get; set; }
    public int _class { get; set; }
    public string rarity { get; set; }
    public string set { get; set; }
    public string discord { get; set; }
    public string website { get; set; }
    public string legal { get; set; }
    public string video { get; set; }
}
