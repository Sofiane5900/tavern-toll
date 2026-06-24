using Raylib_cs;
using System.Numerics;
class CardManager
{

    // TODO: Make a method that display a background for the moving card.
    public void DisplayBackgroundCard()
    {

    }

    public void DisplayCard(GameState gameState, int screenWidth, int screenHeight)
    {

        // First, define how much of the Texture to draw (in our case, total width & height)
        Rectangle cardRectangle = new(0, 0, gameState.CurrentCard.Avatar.Width,
     gameState.CurrentCard.Avatar.Height);

        // Second, define where we want to draw on screen and which size the Texture will take.
        Rectangle screen = new(screenWidth / 2, screenHeight / 2, 250, 250);

        // Third, define the anchor (origin)
        // to be in center of our Texture size (contained in screen)
        Vector2 cardCenter = new(screen.Width / 2,
       screen.Height / 2);

        // ** Render first card image
        Raylib.DrawTexturePro(gameState.CurrentCard.Avatar, cardRectangle, screen,
        cardCenter, 0, Color.White);


        // ** Render dialogue 
        int dialogueWidth = Raylib.MeasureText(gameState.CurrentCard.Dialogue, UIConstant.DefaultFontSize);
        // X position of dialogue should be center of image - half of the dialogue
        int dialogueX = (int)screen.X - (dialogueWidth / 2);
        // Y position of dialogue should be center of image - image height in pixels
        int dialogueY = (int)screen.Y - (int)screen.Height + 55;
        Raylib.DrawText(gameState.CurrentCard.Dialogue, dialogueX, dialogueY, UIConstant.DefaultFontSize, Color.White);



        // ** Render name
        int nameWidth = Raylib.MeasureText(gameState.CurrentCard.Name, UIConstant.DefaultFontSize);
        // X Position of name is center of image - half of the name
        int nameX = (int)screen.X - (nameWidth / 2);
        // Y Position of Name is Y center of image  + image height in pixels 
        //  - 100 (to bring text closer)
        int nameY = (int)screen.Y + (int)screen.Height - 100;
        Raylib.DrawText(gameState.CurrentCard.Name, nameX, nameY, UIConstant.DefaultFontSize, Color.White);
    }
}