using SnakeGame.Model;

namespace SnakeGame.Rendering;

public class ConsoleRenderer : IRenderer
{
    private const char BlockChar = '■';

    private readonly int _width;
    private readonly int _height;

    public ConsoleRenderer(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void Initialize()
    {
        Console.CursorVisible = false;

        if (OperatingSystem.IsWindows())
        {
            Console.WindowHeight = _height;
            Console.WindowWidth = _width;
        }
    }

    public void ClearPlayArea()
    {
        var emptyLine = new string(' ', _width - 2);
        Console.ForegroundColor = ConsoleColor.Black;

        for (int row = 1; row < _height - 1; row++)
        {
            Console.SetCursorPosition(1, row);
            Console.Write(emptyLine);
        }
    }

    public void DrawBorder()
    {
        Console.ForegroundColor = ConsoleColor.White;
        var horizontalLine = new string(BlockChar, _width);

        Console.SetCursorPosition(0, 0);
        Console.Write(horizontalLine);

        Console.SetCursorPosition(0, _height - 1);
        Console.Write(horizontalLine);

        for (int row = 1; row < _height - 1; row++)
        {
            DrawPixel(0, row);
            DrawPixel(_width - 1, row);
        }
    }

    public void DrawSnake(Snake snake)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var segment in snake.Body)
        {
            DrawPixel(segment.X, segment.Y);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        DrawPixel(snake.Head.X, snake.Head.Y);
    }

    public void DrawBerry(Position berryPosition)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        DrawPixel(berryPosition.X, berryPosition.Y);
    }

    public void DrawScore(int score)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(1, 0);
        Console.Write($" Score: {score} ");
    }

    public void DrawGameOver(int score)
    {
        int centerX = _width / 5;
        int centerY = _height / 2;

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(centerX, centerY);
        Console.Write($"Game Over! Score: {score}");
        Console.SetCursorPosition(centerX, centerY + 1);
        Console.Write("Press any key to exit...");
        Console.ReadKey(intercept: true);
    }

    private static void DrawPixel(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(BlockChar);
    }
}
