using Raylib_cs;

namespace HelloWorld;

internal static class Program
{

    const int screenWidth = 800;
    const int screenHeight = 450;
    // STAThread is required if you deploy using NativeAOT on Windows
    // See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Tavern's Toll");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Brown);

            GameState gameState = new();
            HUDManager hudManager = new();

            hudManager.DisplayBars(gameState, screenWidth);
            hudManager.DisplayCard(gameState, screenWidth, screenHeight);

            Raylib.EndDrawing();
        }


        Raylib.CloseWindow();
    }
}