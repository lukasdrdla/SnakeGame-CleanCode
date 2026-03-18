namespace SnakeGame.Model;

public class Board
{
    private readonly Random _random = new();

    public int Width { get; }
    public int Height { get; }
    public Position BerryPosition { get; private set; }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        BerryPosition = SpawnBerry();
    }

    public bool IsWall(Position position)
    {
        return position.X <= 0
            || position.X >= Width - 1
            || position.Y <= 0
            || position.Y >= Height - 1;
    }

    public bool IsBerryEaten(Position headPosition)
    {
        return headPosition == BerryPosition;
    }

    public void RespawnBerry()
    {
        BerryPosition = SpawnBerry();
    }

    private Position SpawnBerry()
    {
        int x = _random.Next(1, Width - 2);
        int y = _random.Next(1, Height - 2);
        return new Position(x, y);
    }
}
