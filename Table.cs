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
}

public class Table
{
    private string _Encounter;
    private float _Difficulty;
    private bool _isCompleted;
    private string _Variants;
    private string _Additional;
    private bool _Uploaded;
    private string _Upload_Notes;
    private bool _isDoneOnWarlock;
    private bool _isDoneOnTitan;
    private bool _isDoneOnHunter;
    private Raid _Raid;

    private bool _isArchived;

    public static string Stringify_Raid(Raid raid)
    {
        switch(raid)
        {
            case Raid.LastWish:
                return "last_wish";
            case Raid.GardenOfSalvation:
                return "garden_of_salvation";
            case Raid.DeepstoneCrypt:
                return "deepstone_crypt";
            case Raid.VaultOfGlass:
                return "vault_of_glass";
            case Raid.VowOfTheDisciple:
                return "vow_of_the_disciple";
            case Raid.KingsFall:
                return "kings_fall";
            case Raid.RootOfNightmares:
                return "root_of_nightmares";
            case Raid.CrotasEnd:
                return "crotas_end";
            case Raid.SalvationsEdge:
                return "salvations_edge";
            case Raid.DesertPerpetual:
                return "desert_perpetual";
            default:
                break;
        }
        // NOTE: Because this is a UI data entry thing this should never be reached
        return "-1";
    }

    public Table()
    {
        _Encounter = "yes";
        _Difficulty = 0.04f;
        _isCompleted = true;
        _Variants = "";
        _Additional = "";
        _Uploaded = true;
        _Upload_Notes = "";
        _isDoneOnWarlock = true;
        _isDoneOnTitan = true;
        _isDoneOnHunter = true;
        _isArchived = true;
        _Raid = Raid.LastWish;

        JsonManager.Write_Json(_Encounter, _Difficulty, _isCompleted, _Uploaded,
            _isDoneOnWarlock, _isDoneOnTitan, _isDoneOnHunter, _Raid, _Variants,
            _Upload_Notes, _Additional, _isArchived);
    }
}
