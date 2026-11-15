namespace SoloTracker;

enum Raid
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

    private bool _isArchived;

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

        JsonManager.Write_Json(_Encounter, _Difficulty, _isCompleted, _Uploaded,
            _isDoneOnWarlock, _isDoneOnTitan, _isDoneOnHunter, _Variants,
            _Upload_Notes, _Additional, _isArchived);
    }
}
