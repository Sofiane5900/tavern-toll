using Raylib_cs;
using System.Numerics;
class CardManager
{


    private Rectangle _cardCollision =
     HUDUtils.CreateCenteredRectangle(UIConstant.CardWidth, UIConstant.CardHeight,
     UIConstant.GameWidth, UIConstant.GameHeight);
    private Rectangle _cardScreen = new(UIConstant.GameWidth / 2, UIConstant.GameHeight / 2,
    UIConstant.CardWidth, UIConstant.CardHeight);

    private float _cardRotation;
    private readonly SwipeService _swipeService = new();
    private readonly CardService _cardService = new();

    public void DisplayDialogue(GameState gameState)
    {
        int dialogueWidth = Raylib.MeasureText(gameState.CurrentCard.Dialogue, UIConstant.DefaultFontSize);
        // X fixed position of dialogue is center of screen - half of the dialogue
        int dialogueX = UIConstant.GameWidth / 2 - dialogueWidth / 2;
        // Y fixed position 
        int dialogueY = 280;
        Raylib.DrawText(gameState.CurrentCard.Dialogue, dialogueX, dialogueY, UIConstant.DefaultFontSize, Color.White);
    }

    public void DisplayName(GameState gameState)
    {
        int nameWidth = Raylib.MeasureText(gameState.CurrentCard.Name, UIConstant.DefaultFontSize);
        // X Position of name is center of image - half of the name
        int nameX = UIConstant.GameWidth / 2 - (nameWidth / 2);
        // Y fixed position
        int nameY = 630;
        Raylib.DrawText(gameState.CurrentCard.Name, nameX, nameY, UIConstant.DefaultFontSize, Color.White);
    }

    public void DisplayImage(GameState gameState)
    {
        Raylib.DrawRectangleRec(_cardCollision, Color.Red);
        // First, define how much of the Texture to draw (in our case, total width & height)
        Rectangle cardDest = new(0, 0, gameState.CurrentCard.Avatar.Width,
     gameState.CurrentCard.Avatar.Height);

        // Second, define where we want to draw on screen and which size the Texture will take.

        // Third, define the anchor (origin)
        // to be in center of our Texture size (contained in screen)
        Vector2 cardOrigin = new(_cardScreen.Width / 2,
       _cardScreen.Height / 2);

        // ** Render first card image
        Raylib.DrawTexturePro(gameState.CurrentCard.Avatar, cardDest, _cardScreen,
        cardOrigin, _cardRotation, Color.White);
        DisplayChoices(gameState);
    }

    /// <summary>
    /// Method dedicated to display choices based on card position 
    /// </summary>
    /// <param name="gameState"></param>
    public void DisplayChoices(GameState gameState)
    {
        SwipeCoordinates swipeResult = _swipeService.CalculateCardPos(_cardScreen);
        float centerX = UIConstant.GameWidth / 2f;
        float threshold = 25f;
        // gap is simply the card position - the center of the screen
        float gap = swipeResult.Position.X - centerX;

        Decision currentDecision = Decision.None;
        // if gap is higher the threshold card is moving right
        if (gap > threshold) currentDecision = Decision.Right;
        // if gap is lower the threshold card is moving left
        else if (gap < -threshold) currentDecision = Decision.Left;


        if (currentDecision != Decision.None)
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                Choice choice = _cardService.GetChoice(gameState, currentDecision);
                DrawChoiceInsideCard(choice, swipeResult, currentDecision);
            }
            else if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                Console.WriteLine($"choice {currentDecision} !");
                _cardService.ResolveChoice(gameState, currentDecision);
            }
        }
    }

    /// <summary>
    /// Method dedicated to draw choice text inside the current card
    /// </summary>
    /// <param name="choice">Current choice to display</param>
    /// <param name="swipe">Cordinates of the card (position & rotation)</param>
    /// <param name="decision">Decision that is called by the player</param>
    public void DrawChoiceInsideCard(Choice choice, SwipeCoordinates swipe, Decision decision)
    {
        float rotRad = swipe.Rotation * (MathF.PI / 180f);
        Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), choice.Text, UIConstant.DefaultFontSize, 1);

        // offset, X change from engative to positive based on side (left/right), Y is fixed
        float side = (decision == Decision.Left) ? -1f : 1f;
        Vector2 offset = new Vector2((125f - 25f) * side, -110f);

        // apply position & rotation
        Vector2 pos = swipe.Position + Raymath.Vector2Rotate(offset, rotRad);
        Vector2 origin = new Vector2((decision == Decision.Right) ? textSize.X : 0f, 0f);

        Raylib.DrawTextPro(Raylib.GetFontDefault(), choice.Text, pos, origin, swipe.Rotation, UIConstant.DefaultFontSize, 1, Color.White);
    }

    public void DisplayCard(GameState gameState)
    {

        DisplayImage(gameState);
        DisplayDialogue(gameState);
        DisplayName(gameState);

    }

    /// <summary>
    /// Method dedicated to give card pos to draw it on screen
    /// </summary>
    public void MoveCard()
    {
        SwipeCoordinates swipeResult = _swipeService.CalculateCardPos(_cardScreen);
        _cardScreen.X = swipeResult.Position.X;
        _cardScreen.Y = swipeResult.Position.Y;
        _cardRotation = swipeResult.Rotation;

    }

}
