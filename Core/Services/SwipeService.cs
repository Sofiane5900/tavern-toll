using Raylib_cs;
using System.Numerics;

class SwipeService
{
    private bool _isDragging;
    private Vector2 _dragOffset;


    public Vector2 CalculateCardPos(Rectangle cardRec)
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
            cardPos = mousePos - _dragOffset;

            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                _isDragging = false;
            }

        }
        // return new card pos based on mouse
        return new Vector2(cardPos.X, cardPos.Y);
    }
}
