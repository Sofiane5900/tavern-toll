using Raylib_cs;

class Card
{
    private string _name;
    private Texture2D _avatar;
    private string _dialogue = string.Empty;
    private int[] _leftImpacts = new int[4];
    private int[] _rightImpacts = new int[4];


    public Card(string name, Texture2D avatar, string dialogue, int[] leftImpacts,
    int[] rightImpacts)
    {
        _name = name;
        _avatar = avatar;
        Dialogue = dialogue;
        LeftImpacts = leftImpacts;
        RightImpacts = rightImpacts;

    }

    public string Name => _name;

    public Texture2D Avatar => _avatar;

    public string Dialogue
    {
        get => _dialogue;
        set => _dialogue = value;
    }

    public int[] LeftImpacts
    {
        get => _leftImpacts;
        set => _leftImpacts = value;
    }

    public int[] RightImpacts
    {
        get => _rightImpacts;
        set => _rightImpacts = value;
    }

}