using System.Numerics;
using Raylib_cs;
static class InputUtils
{
    // Get the raw mouse position using Raylib default GetMousePosition()
    // Return a new mouse position based on a desired game screen.
    public static Vector2 GetGameMouse(int windowWidth, int windowHeight, int gameWidth, int gameHeight)
    {
        float scale = MathF.Min((float)windowWidth / gameWidth, (float)windowHeight / gameHeight);

        float offsetX = (windowWidth - (gameWidth * scale)) / 2f;
        float offsetY = (windowHeight - (gameHeight * scale)) / 2f;

        Vector2 realMouse = Raylib.GetMousePosition();

        return new Vector2(
            (realMouse.X - offsetX) / scale,
            (realMouse.Y - offsetY) / scale
        );
    }
}