using Raylib_cs;
using System.Numerics;

public struct SwipeResult
{
    public Vector2 Position;
    public float Rotation;
}

class SwipeService
{
    private bool _isDragging;
    private Vector2 _dragOffset;

    public float CalculateCardRotation(Vector2 cardPos)
    {
        // gap between card and center of screen (positive if we swipe right, negative at right)
        float gapFromCenter = cardPos.X - (UIConstant.GameWidth / 2f);

        // card rotate more if gap is bigger
        float cardRotation = gapFromCenter * 0.1f;
        // rotation should be between -15f and 15f degree max
        cardRotation = Math.Clamp(cardRotation, -15f, 15f);
        return cardRotation;
    }

    public SwipeResult CalculateCardPos(Rectangle cardRec)
    {
        // 1: Is mouse left-click pressed on top of card-rectangle?
        Vector2 mousePos = InputUtils.GetGameMouse(UIConstant.WindowWidth, UIConstant.WindowHeight,
        UIConstant.GameWidth, UIConstant.GameHeight);

        // card rectangle converted to vector2 cordinates
        Vector2 cardPos = new(cardRec.X, cardRec.Y);

        if (Raylib.CheckCollisionPointRec(mousePos, cardRec) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Console.WriteLine("BOUTTON PRESSÉ & SOURIS SUR L'AXE X DE LA CARTE");
            _isDragging = true;
            _dragOffset = mousePos - cardPos;
        }

        // 2. Move card accordingly to mouse position.
        if (_isDragging == true)
        {
            // get current mouse pos
            cardPos = mousePos - _dragOffset;
            cardPos = SmoothCardPos(mousePos, cardPos);
            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                _isDragging = false;
            }

        }
        else
        {
            cardPos = ReleaseCard(cardPos);
        }
        SwipeResult result;
        result.Position = cardPos;
        result.Rotation = CalculateCardRotation(cardPos);

        return result;

    }

    public Vector2 SmoothCardPos(Vector2 mousePos, Vector2 cardPos)
    {
        // limit X & Y axis to keep card on "track"
        float centerX = UIConstant.GameWidth / 2f;
        float centerY = UIConstant.GameHeight / 2f;
        // calcualte gap between mouse and center of screen
        float deltaX = mousePos.X - _dragOffset.X - centerX;
        float deltaY = mousePos.Y - _dragOffset.Y - centerY;
        cardPos.X = centerX + (deltaX * 0.4f);
        cardPos.Y = centerY + (deltaY * 0.1f);
        return cardPos;

    }

    public Vector2 ReleaseCard(Vector2 cardPos)
    {

        // target cordinates cards to be sent to
        float targetX = UIConstant.GameWidth / 2f;
        float targetY = UIConstant.GameHeight / 2f;

        cardPos.X = Raymath.Lerp(cardPos.X, targetX, 0.015f);
        cardPos.Y = Raymath.Lerp(cardPos.Y, targetY, 0.015f);
        return cardPos;

    }
}
