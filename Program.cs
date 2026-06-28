using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

internal static class Program
{





    // STAThread is required if you deploy using NativeAOT on Windows
    // See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(UIConstant.WindowWidth, UIConstant.WindowHeight, "Tavern's Toll");


        RenderTexture2D playableScreen = Raylib.LoadRenderTexture(UIConstant.GameWidth, UIConstant.GameHeight);
        // Variables to draw our playableScreen 
        Rectangle source = new(0, 0, UIConstant.GameWidth, -UIConstant.GameHeight);
        Vector2 origin = new(UIConstant.GameWidth / 2, UIConstant.GameHeight / 2);
        Rectangle dest = new(UIConstant.WindowWidth / 2, UIConstant.WindowHeight / 2, UIConstant.GameWidth, UIConstant.GameHeight);

        GameState gameState = new();
        HUDManager hudManager = new();

        while (!Raylib.WindowShouldClose())
        {
            // 1. Charge card cordinates 
            hudManager.UpdateCard(gameState);


            // 2. Render HUD into Framebuffer
            Raylib.BeginTextureMode(playableScreen);
            Raylib.ClearBackground(Color.Brown);

            hudManager.DrawHUD(gameState);
            Raylib.DrawRectangleLines(0, 0, UIConstant.GameWidth, UIConstant.GameHeight,
Color.Black);
            Raylib.EndTextureMode();

            // Draw to the window 
            Raylib.BeginDrawing();
            Raylib.DrawTexturePro(playableScreen.Texture, source, dest, origin, 0, Color.White);
            Raylib.ClearBackground(Color.Brown);

            Raylib.EndDrawing();
        }


        Raylib.CloseWindow();
    }
}