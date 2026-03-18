namespace SnakeGame.Model;

public readonly record struct Position(int X, int Y)
{
    public Position Move(Game.Direction direction)
    {
        return direction switch
        {
            Game.Direction.Up => this with { Y = Y - 1 },
            Game.Direction.Down => this with { Y = Y + 1 },
            Game.Direction.Left => this with { X = X - 1 },
            Game.Direction.Right => this with { X = X + 1 },
            _ => this
        };
    }
}
