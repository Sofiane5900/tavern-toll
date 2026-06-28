using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;
class CardManager
{


    private Rectangle _cardCollision =
     HUDUtils.CreateCenteredRectangle(250, 250, UIConstant.GameWidth, UIConstant.GameHeight);
    private Rectangle _cardScreen = new(UIConstant.GameWidth / 2, UIConstant.GameHeight / 2, 250, 250);

    private bool _isDragging;
    private Vector2 _dragOffset;

    // TODO: Make a method that display a background for the moving card.
    public void DisplayBackgroundCard()
    {

    }

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
        cardOrigin, 0, Color.White);


        // ** Render dialogue 
        int dialogueWidth = Raylib.MeasureText(gameState.CurrentCard.Dialogue, UIConstant.DefaultFontSize);
        // X position of dialogue should be center of image - half of the dialogue
        int dialogueX = (int)_cardScreen.X - (dialogueWidth / 2);
        // Y position of dialogue should be center of image - image height in pixels
        int dialogueY = (int)_cardScreen.Y - (int)_cardScreen.Height + 55;
        Raylib.DrawText(gameState.CurrentCard.Dialogue, dialogueX, dialogueY, UIConstant.DefaultFontSize, Color.White);



        // ** Render name
        int nameWidth = Raylib.MeasureText(gameState.CurrentCard.Name, UIConstant.DefaultFontSize);
        // X Position of name is center of image - half of the name
        int nameX = (int)_cardScreen.X - (nameWidth / 2);
        // Y Position of Name is Y center of image  + image height in pixels 
        //  - 100 (to bring text closer)
        int nameY = (int)_cardScreen.Y + (int)_cardScreen.Height - 100;
        Raylib.DrawText(gameState.CurrentCard.Name, nameX, nameY, UIConstant.DefaultFontSize, Color.White);
    }

    public void MoveCard()
    {
        // 1: Is mouse left-click pressed on top of card-rectangle?
        Rectangle cardRec;
        cardRec = _cardScreen;
        Vector2 mousePos = InputUtils.GetGameMouse(UIConstant.WindowWidth, UIConstant.WindowHeight,
        UIConstant.GameWidth, UIConstant.GameHeight);

        // card rectangle converted to vector2 cordinates
        Vector2 cardPos = new(cardRec.X, cardRec.Y);

        if (Raylib.CheckCollisionPointRec(mousePos, cardRec) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Console.WriteLine("BOUTTON PRESSÉ & SOURIS SUR L'AXE X DE LA CARTE");
            _isDragging = true;
            _dragOffset = mousePos - cardPos;
        }

        // 2. Move card accordingly to mouse position.
        if (_isDragging == true)
        {
            cardPos = mousePos - _dragOffset;

            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                _isDragging = false;
            }

            // displaying card based on our card position calcualted from mouse
            _cardScreen.X = cardPos.X;
            _cardScreen.Y = cardPos.Y;
        }


    }
}