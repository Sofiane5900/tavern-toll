using System.Diagnostics.Metrics;
using System.Numerics;
using Raylib_cs;

class HUDManager
{

    const int defaultFontSize = 20;
    const int dialogueFontSize = 15;
    public void DisplayBars(GameState gameState, int screenWidth)
    {
        int totalWidth = 0;
        int spaceInBetween = 25;
        int[] totalBars = new int[gameState.Bars.Count()];
        // Get the total width of all bars
        for (int i = 0; i < totalBars.Count(); i++)
        {
            string name = gameState.Bars[i].Name;
            int stats = gameState.Bars[i].CurrentStat;
            totalBars[i] = Raylib.MeasureText($"{name}: \n{stats}", defaultFontSize);
            totalWidth += totalBars[i];
        }
        // 4 Bars in total (we don't need space after the last one)
        totalWidth += spaceInBetween * (totalBars.Count() - 1);
        int cursor = (screenWidth - totalWidth) / 2; // Position of the first bar
        int Y = 150;

        // Draw all bars to the top of the screen
        for (int i = 0; i < totalBars.Count(); i++)
        {
            string name = gameState.Bars[i].Name;
            int stats = gameState.Bars[i].CurrentStat;
            Raylib.DrawText($"{name}: \n{stats}", cursor, Y, defaultFontSize, Color.White);

            // only if we aren't to the last element, keep incrementing cursor
            if (i < totalBars.Count() - 1) cursor += totalBars[i] + spaceInBetween;
        }

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

        // Render first card image
        Raylib.DrawTexturePro(gameState.CurrentCard.Avatar, cardRectangle, screen,
        cardCenter, 0, Color.White);


        // Render dialogue 
        int dialogueWidth = Raylib.MeasureText(gameState.CurrentCard.Dialogue, dialogueFontSize);
        // X position of dialogue should be center of image - half of the dialogue
        int dialogueX = (int)screen.X - (dialogueWidth / 2);
        // Y position of dialogue should be center of image - image height in pixels
        int dialogueY = (int)screen.Y - (int)screen.Height + 45;
        Raylib.DrawText(gameState.CurrentCard.Dialogue, dialogueX, dialogueY, defaultFontSize, Color.White);



        // Render name
        int nameWidth = Raylib.MeasureText(gameState.CurrentCard.Name, defaultFontSize);
        // X Position of name is center of image - half of the name
        int nameX = (int)screen.X - (nameWidth / 2);
        // Y Position of Name is Y center of image  + image height in pixels 
        //  - 100 (to bring text closer)
        int nameY = (int)screen.Y + (int)screen.Height - 100;
        Raylib.DrawText(gameState.CurrentCard.Name, nameX, nameY, defaultFontSize, Color.White);

    }


}