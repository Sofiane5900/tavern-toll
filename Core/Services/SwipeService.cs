using Raylib_cs;
using System.Numerics;

public struct SwipeCoordinates
{
    public Vector2 Position;
    public float Rotation;
}

class SwipeService
{
    private bool _isDragging;
    private Vector2 _dragOffset;

    /// <summary>
    /// Calculate card rotation based on the center of the game screen
    /// </summary>
    /// <param name="cardPos"></param>
    /// <returns>A floating number which will be used to rotate card</returns>
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

    /// <summary>
    /// Calculate the position & rotation when left mouse button is pressed
    /// </summary>
    /// <param name="cardRec"></param>
    /// <returns>A SwipeCoordinates struct with card pos & rotation</returns>
    public SwipeCoordinates CalculateCardPos(Rectangle cardRec)
    {
        Vector2 mousePos = InputUtils.GetGameMouse(UIConstant.WindowWidth, UIConstant.WindowHeight,
        UIConstant.GameWidth, UIConstant.GameHeight);

        // card rectangle converted to vector2 cordinates
        Vector2 cardPos = new(cardRec.X, cardRec.Y);

        // get card clickable hitbox for mouse
        Rectangle hitBox = new(
            cardRec.X - (cardRec.Width / 2f),
            cardRec.Y - (cardRec.Height / 2f),
            cardRec.Width,
            cardRec.Height
        );


        if (Raylib.CheckCollisionPointRec(mousePos, hitBox) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            _isDragging = true;
            _dragOffset = mousePos - cardPos;
        }

        if (_isDragging == true)
        {
            // get current mouse pos
            Vector2 rawCardPos = mousePos - _dragOffset;
            cardPos = SmoothCardPos(rawCardPos);
            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                _isDragging = false;
            }

        }
        else
        {
            cardPos = ReleaseCard(cardPos);
        }
        SwipeCoordinates result;
        result.Position = cardPos;
        result.Rotation = CalculateCardRotation(cardPos);

        return result;
    }

    /// <summary>
    /// Smooth the movement of the card and prevent it to move too far from screen.
    /// </summary>
    /// <param name="mousePos"></param>
    /// <param name="cardPos"></param>
    /// <returns>A Vector2 struct with smoothened coordinates</returns>
    public Vector2 SmoothCardPos(Vector2 cardPos)
    {
        float centerX = UIConstant.GameWidth / 2f;
        float centerY = UIConstant.GameHeight / 2f;
        // calcualte gap between card pos and center of screen
        float deltaX = cardPos.X - centerX;
        float deltaY = cardPos.Y - centerY;
        cardPos.X = centerX + (deltaX * 0.2f);
        cardPos.Y = centerY + (deltaY * 0.1f);
        return cardPos;
    }

    /// <summary>
    /// Release back the card into the center of the screen when called
    /// </summary>
    /// <param name="cardPos"></param>
    /// <returns>A Vector2 struct with centered coordinates</returns>
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
