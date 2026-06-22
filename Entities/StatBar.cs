class StatBar
{
    private string _name;
    private int _currentStat;
    private int _maxStat;


    public StatBar(string name, int currentStat, int maxStat)
    {
        _name = name;
        MaxStat = maxStat;
        CurrentStat = currentStat;
    }

    public string Name => _name;
    public int CurrentStat
    {
        get => _currentStat;
        set
        {
            if (value >= 0 && value <= _maxStat) _currentStat = value;
        }
    }

    public int MaxStat
    {
        get => _maxStat;
        set
        {
            if (value >= 0) _maxStat = value;
        }
    }
}