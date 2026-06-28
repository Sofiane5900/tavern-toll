using Raylib_cs;

class Card
{
    private string _name;
    private Texture2D _avatar;
    private string _dialogue = string.Empty;
    private Choice _leftChoice;
    private Choice _rightChoice;



    public Card(string name, Texture2D avatar, string dialogue, Choice leftChoice,
     Choice rightChoice)
    {
        _name = name;
        _avatar = avatar;
        Dialogue = dialogue;
        LeftChoice = leftChoice;
        RightChoice = rightChoice;
    }

    public string Name => _name;

    public Texture2D Avatar => _avatar;

    public string Dialogue
    {
        get => _dialogue;
        set => _dialogue = value;
    }

    public Choice LeftChoice
    {
        get => _leftChoice;
        set => _leftChoice = value;
    }
    public Choice RightChoice
    {
        get => _rightChoice;
        set => _rightChoice = value;
    }
}
