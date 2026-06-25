using Raylib_cs;

class GameState
{
    private StatBar[] _bars;
    private Card _currentCard;
    private int _day;
    private bool _isGameOver;

    public GameState()
    {
        _isGameOver = false;

        _bars = new StatBar[]
        {
            new StatBar("Gold", 50, 100),
            new StatBar("Stocks", 50, 100),
            new StatBar("Reputation", 50, 100),
            new StatBar("Authority", 50, 100)
        };

        Choice choice = new();
        _currentCard = new Card("The Lady", Raylib.LoadTexture("Assets/the_lady.png"),
        "Bonjour, je suis une carte. Voilà tout...",


        _day = 1;

    }


    public StatBar[] Bars
    {
        get => _bars;
        set => _bars = value;
    }

    public Card CurrentCard
    {
        get => _currentCard;
        set => _currentCard = value;
    }

    public int Day
    {
        get => _day;
        set => _day = value;
    }

    public bool IsGameOver
    {
        get => _isGameOver;
        set => _isGameOver = value;
    }
}