
public class OLAuthor
{
    public Type type { get; set; }
    public string[] alternate_names { get; set; }
    public Remote_Ids remote_ids { get; set; }
    public string personal_name { get; set; }
    public string birth_date { get; set; }
    public Bio? bio { get; set; }
    public string key { get; set; }
    public string[] source_records { get; set; }
    public int[] photos { get; set; }
    public string name { get; set; }
    public int latest_revision { get; set; }
    public int revision { get; set; }
    public Created created { get; set; }
    public Last_Modified last_modified { get; set; }
}

public class Remote_Ids
{
    public string storygraph { get; set; }
    public string amazon { get; set; }
    public string viaf { get; set; }
    public string goodreads { get; set; }
    public string wikidata { get; set; }
    public string isni { get; set; }
    public string librarything { get; set; }
}

public class Bio
{
    public string type { get; set; }
    public string value { get; set; }
}


public class Last_Modified
{
    public string type { get; set; }
    public DateTime value { get; set; }
}
