using SnakeGame.Model;

namespace SnakeGame.Rendering;

public interface IRenderer
{
    void Initialize();
    void ClearPlayArea();
    void DrawBorder();
    void DrawSnake(Snake snake);
    void DrawBerry(Position berryPosition);
    void DrawScore(int score);
    void DrawGameOver(int score);
}
