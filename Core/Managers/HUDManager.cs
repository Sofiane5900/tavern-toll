using System.Diagnostics.Metrics;
using System.Numerics;
using Raylib_cs;

class HUDManager
{

    private readonly CardManager _cardManager = new();
    private readonly StatBarManager _statBarManager = new();
    public void DrawHUD(GameState gameState)
    {
        _cardManager.DisplayCard(gameState);
        _statBarManager.DisplayBars(gameState, UIConstant.GameWidth);
    }

    public void UpdateCard(GameState gameState)
    {
        _cardManager.MoveCard();
    }
}
