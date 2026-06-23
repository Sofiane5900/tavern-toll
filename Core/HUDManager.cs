using System.Numerics;
using Raylib_cs;

class HUDManager
{

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
            totalBars[i] = Raylib.MeasureText($"{name}: \n{stats}", 20);
            totalWidth += totalBars[i];
        }
        // 4 Bars in total (we don't need space after the last one)
        totalWidth += spaceInBetween * (totalBars.Count() - 1);
        int cursor = (screenWidth - totalWidth) / 2; // Position of the first bar
        int Y = 200;

        // Draw all bars to the top of the screen
        for (int i = 0; i < totalBars.Count(); i++)
        {
            string name = gameState.Bars[i].Name;
            int stats = gameState.Bars[i].CurrentStat;
            Raylib.DrawText($"{name}: \n{stats}", cursor, Y, 20, Color.White);

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

        Raylib.DrawTexturePro(gameState.CurrentCard.Avatar, cardRectangle, screen,
        cardCenter, 0, Color.White);
    }


}