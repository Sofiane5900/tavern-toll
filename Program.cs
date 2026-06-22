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
            UIManager uiManager = new();

            uiManager.DisplayBars(gameState, screenWidth);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }


        Raylib.CloseWindow();
    }
}