using Raylib_cs;

static class HUDUtils
{
    public static Rectangle CreateCenteredRectangle(int targetWidth, int targetHeight, int screenWidth, int screenHeight)
    {
        int x = (screenWidth / 2) - (targetWidth / 2);
        int y = (screenHeight / 2) - (targetHeight / 2);

        return new Rectangle(x, y, targetWidth, targetHeight);
    }
}