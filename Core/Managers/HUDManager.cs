using System.Diagnostics.Metrics;
using System.Numerics;
using Raylib_cs;

class HUDManager
{

    private readonly CardManager cardManager = new();
    private readonly StatBarManager statBarManager = new();
    public void DisplayHUD(GameState gameState, int screenWidth, int screenHeight)
    {
        cardManager.DisplayCard(gameState, screenWidth, screenHeight);
        statBarManager.DisplayBars(gameState, screenWidth);
    }

}
