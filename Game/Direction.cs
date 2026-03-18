namespace SnakeGame.Game;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public static class DirectionExtensions
{
    public static bool IsOpposite(this Direction direction, Direction other)
    {
        return direction switch
        {
            Direction.Up => other == Direction.Down,
            Direction.Down => other == Direction.Up,
            Direction.Left => other == Direction.Right,
            Direction.Right => other == Direction.Left,
            _ => false
        };
    }
}
