using Raylib_cs;

class UIManager
{

    public void DisplayBars(GameState gameState, int screenWidth)
    {
        int totalWidth = 0;
        int spaceInBetween = 50;
        int[] totalBars = new int[gameState.Bars.Count()];
        // Get the total width of all bars
        for (int i = 0; i < totalBars.Count(); i++)
        {
            string name = gameState.Bars[i].Name;
            int stats = gameState.Bars[i].CurrentStat;
            totalBars[i] = Raylib.MeasureText($"{name}: {stats}", 20);
            totalWidth += totalBars[i];
        }
        // 4 Bars in total (we don't need space after the last one)
        totalWidth += spaceInBetween * (totalBars.Count() - 1);
        int cursor = (screenWidth - totalWidth) / 2; // Position of the first bar
        int Y = 50;

        // Draw all bars to the top of the screen
        for (int i = 0; i < totalBars.Count(); i++)
        {
            string name = gameState.Bars[i].Name;
            int stats = gameState.Bars[i].CurrentStat;
            Raylib.DrawText($"{name}: {stats}", cursor, Y, 20, Color.White);

            // only if we aren't to the last element, keep incrementing cursor
            if (i < totalBars.Count() - 1) cursor += totalBars[i] + spaceInBetween;
        }

    }
}