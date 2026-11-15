namespace SoloTracker;

public enum Raid
{
    LastWish,
    GardenOfSalvation,
    DeepstoneCrypt,
    VaultOfGlass,
    VowOfTheDisciple,
    KingsFall,
    RootOfNightmares,
    CrotasEnd,
    SalvationsEdge,
    DesertPerpetual,
    Pantheon,
}

public static class Raid_Extensions
{
    public static string Stringify_Raid(Raid raid) => raid switch
    {
        Raid.LastWish           => "last_wish",
        Raid.GardenOfSalvation  => "garden_of_salvation",
        Raid.DeepstoneCrypt     => "deepstone_crypt",
        Raid.VaultOfGlass       => "vault_of_glass",
        Raid.VowOfTheDisciple   => "vow_of_the_disciple",
        Raid.KingsFall          => "kings_fall",
        Raid.RootOfNightmares   => "root_of_nightmares",
        Raid.CrotasEnd          => "crotas_end",
        Raid.SalvationsEdge     => "salvations_edge",
        Raid.DesertPerpetual    => "desert_perpetual",
        Raid.Pantheon           => "pantheon",
        _ => "-1"
    };

    public static Raid Enumify_Raid(string raid) => raid switch
    {
        "last_wish"             => Raid.LastWish,
        "garden_of_salvation"   => Raid.GardenOfSalvation,
        "deepstone_crypt"       => Raid.DeepstoneCrypt,
        "vault_of_glass"        => Raid.VaultOfGlass,
        "vow_of_the_disciple"   => Raid.VowOfTheDisciple,
        "kings_fall"            => Raid.KingsFall,
        "root_of_nightmares"    => Raid.RootOfNightmares,
        "crotas_end"            => Raid.CrotasEnd,
        "salvations_edge"       => Raid.SalvationsEdge,
        "desert_perpetual"      => Raid.DesertPerpetual,
        "pantheon"              => Raid.Pantheon,
        _ => Raid.LastWish // NOTE: It will never reach this point
    };
}

public class Row
{
    public string Encounter     { get; set; }
    public float Difficulty     { get; set; }
    public bool isCompleted     { get; set; }
    public string Variants      { get; set; }
    public string Additional    { get; set; }
    public bool Uploaded        { get; set; }
    public string Upload_Notes  { get; set; }
    public bool isDoneOnWarlock { get; set; }
    public bool isDoneOnTitan   { get; set; }
    public bool isDoneOnHunter  { get; set; }
    public Raid _Raid           { get; set; }
    public bool isArchived      { get; set; }

    public Row()
    {
        Encounter = "";
        Difficulty = 0.0f;
        isCompleted = false;
        Variants = "";
        Additional = "";
        Uploaded = false;
        Upload_Notes = "";
        isDoneOnWarlock = false;
        isDoneOnTitan = false;
        isDoneOnHunter = false;
        isArchived = false;
        _Raid = Raid.LastWish;
    }
}
