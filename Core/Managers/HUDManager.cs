using System.Diagnostics.Metrics;
using System.Numerics;
using Raylib_cs;

class HUDManager
{

    private readonly CardManager _cardManager = new();
    private readonly StatBarManager _statBarManager = new();
    public void DisplayHUD(GameState gameState, int screenWidth, int screenHeight)
    {
        _cardManager.DisplayCard(gameState, screenWidth, screenHeight);
        _statBarManager.DisplayBars(gameState, screenWidth);
    }

}
