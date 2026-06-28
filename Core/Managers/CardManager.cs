using Raylib_cs;
using System.Numerics;
class CardManager
{


    private Rectangle _cardCollision =
     HUDUtils.CreateCenteredRectangle(250, 250, UIConstant.GameWidth, UIConstant.GameHeight);
    private Rectangle _cardScreen = new(UIConstant.GameWidth / 2, UIConstant.GameHeight / 2, 250, 250);

    private float _cardRotation;
    private readonly SwipeService _swipeService = new();

    public void DisplayCard(GameState gameState)
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


        // ** Render dialogue 
        int dialogueWidth = Raylib.MeasureText(gameState.CurrentCard.Dialogue, UIConstant.DefaultFontSize);
        // X fixed position of dialogue is center of screen - half of the dialogue
        int dialogueX = UIConstant.GameWidth / 2 - dialogueWidth / 2;
        // Y fixed position 
        int dialogueY = 280;
        Raylib.DrawText(gameState.CurrentCard.Dialogue, dialogueX, dialogueY, UIConstant.DefaultFontSize, Color.White);



        // ** Render name
        int nameWidth = Raylib.MeasureText(gameState.CurrentCard.Name, UIConstant.DefaultFontSize);
        // X Position of name is center of image - half of the name
        int nameX = UIConstant.GameWidth / 2 - (nameWidth / 2);
        // Y fixed position
        int nameY = 630;
        Raylib.DrawText(gameState.CurrentCard.Name, nameX, nameY, UIConstant.DefaultFontSize, Color.White);
    }

    public void MoveCard()
    {
        SwipeResult swipeResult = _swipeService.CalculateCardPos(_cardScreen);
        _cardScreen.X = swipeResult.Position.X;
        _cardScreen.Y = swipeResult.Position.Y;
        _cardRotation = swipeResult.Rotation;

    }

}
