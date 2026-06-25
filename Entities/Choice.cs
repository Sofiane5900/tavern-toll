class Choice
{
    private string _text;
    private Decision _decision;

    // Impacts on StatBar 
    private int _goldImpact;
    private int _stockImpact;
    private int _reputationImpact;
    private int _authorityImpact;

    public Choice(string text, Decision decision, int goldImpact, int stockImpact,
    int reputationImpact, int authorityImpact)
    {
        Text = text;
        Decision = decision;
        GoldImpact = goldImpact;
        StockImpact = stockImpact;
        ReputationImpact = reputationImpact;
        AuthorityImpact = authorityImpact;
    }

    public string Text
    {
        get => _text;
        set => _text = value;
    }

    public Decision Decision
    {
        get => _decision;
        set => _decision = value;
    }

    public int GoldImpact
    {
        get => _goldImpact;
        set => _goldImpact = value;
    }
    public int StockImpact
    {
        get => _stockImpact;
        set => _stockImpact = value;
    }
    public int ReputationImpact
    {
        get => _reputationImpact;
        set => _reputationImpact = value;
    }
    public int AuthorityImpact
    {
        get => _authorityImpact;
        set => _authorityImpact = value;
    }


}

