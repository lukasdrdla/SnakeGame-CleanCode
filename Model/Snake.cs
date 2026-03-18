using SnakeGame.Game;

namespace SnakeGame.Model;

public class Snake
{
    private readonly List<Position> _body = new();

    public Position Head { get; private set; }
    public IReadOnlyList<Position> Body => _body;
    public Direction CurrentDirection { get; private set; }
    public int Length => _body.Count;

    public Snake(Position startPosition)
    {
        Head = startPosition;
        CurrentDirection = Direction.Right;
    }

    public void Move()
    {
        _body.Add(Head);
        Head = Head.Move(CurrentDirection);
    }

    public void TrimTail()
    {
        if (_body.Count > 0)
        {
            _body.RemoveAt(0);
        }
    }

    public bool CollidesWithSelf()
    {
        return _body.Contains(Head);
    }

    public void ChangeDirection(Direction newDirection)
    {
        if (!CurrentDirection.IsOpposite(newDirection))
        {
            CurrentDirection = newDirection;
        }
    }
}
