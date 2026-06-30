using Raylib_cs;

class CardService
{
    private readonly Card[] _deck;
    private int _cursor;

    public CardService()
    {
        _deck = new Card[]
        {
            new Card("The Lady", Raylib.LoadTexture("Assets/the_lady.png"),
                "Bonjour, je suis une carte. Voilà tout...",
                new Choice("Choix gauche?", Decision.Left, 10, 10, 0, 0),
                new Choice("Choix droite?", Decision.Right, 0, 0, 10, 10)),
            new Card("The Jester", Raylib.LoadTexture("Assets/the_jester.png"),
                "Une nouvelle tete surgit du coin de la taverne...",
                new Choice("Choix gauche?", Decision.Left, 0, 10, 10, 0),
                new Choice("Choix droite?", Decision.Right, 10, 0, 0, 10))
        };
    }

    public Choice GetChoice(GameState gameState, Decision? decision)
    {
        return decision == Decision.Left
      ? gameState.CurrentCard.LeftChoice
      : gameState.CurrentCard.RightChoice;
    }

    public void ResolveChoice(GameState gameState, Decision decision)
    {

        Choice choice = GetChoice(gameState, decision);

        ApplyImpact(gameState, "Gold", choice.GoldImpact);
        ApplyImpact(gameState, "Stocks", choice.StockImpact);
        ApplyImpact(gameState, "Reputation", choice.ReputationImpact);
        ApplyImpact(gameState, "Authority", choice.AuthorityImpact);

        gameState.Day++;
        gameState.CurrentCard = NextCard();
    }

    private static void ApplyImpact(GameState gameState, string barName, int impact)
    {
        if (impact == 0) return;
        foreach (StatBar bar in gameState.Bars)
        {
            if (bar.Name == barName)
            {
                bar.CurrentStat += impact;
                return;
            }
        }
    }

    private Card NextCard()
    {
        Card card = _deck[_cursor % _deck.Length];
        _cursor++;
        return card;
    }
}
