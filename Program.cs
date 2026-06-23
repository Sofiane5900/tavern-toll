using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

internal static class Program
{


    const int totalScreenWidth = 1280;
    const int totalScreenHeight = 720;


    const int playableScreenWidth = 540;
    const int playableScreenHeight = 960;



    // STAThread is required if you deploy using NativeAOT on Windows
    // See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(totalScreenWidth, totalScreenHeight, "Tavern's Toll");


        RenderTexture2D playableScreen = Raylib.LoadRenderTexture(playableScreenWidth, playableScreenHeight);
        // Variables to draw our playableScreen 
        Rectangle source = new(0, 0, playableScreenWidth, -playableScreenHeight);
        Vector2 origin = new(playableScreenWidth / 2, playableScreenHeight / 2);
        Rectangle dest = new(totalScreenWidth / 2, totalScreenHeight / 2, playableScreenWidth, playableScreenHeight);

        GameState gameState = new();
        HUDManager hudManager = new();

        while (!Raylib.WindowShouldClose())
        {
            // Render HUD to framebuffer
            Raylib.BeginTextureMode(playableScreen);
            hudManager.DisplayBars(gameState, playableScreenWidth);
            hudManager.DisplayCard(gameState, playableScreenWidth, playableScreenHeight);
            Raylib.DrawRectangleLines(0, 0, playableScreenWidth, playableScreenHeight,
Color.Black);
            Raylib.EndTextureMode();

            // Draw to the window 
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Brown);
            Raylib.DrawTexturePro(playableScreen.Texture, source, dest, origin, 0, Color.White);


            Raylib.EndDrawing();
        }


        Raylib.CloseWindow();
    }
}