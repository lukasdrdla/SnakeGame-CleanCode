using System.Diagnostics;
using SnakeGame.Game;

namespace SnakeGame.Input;

public class ConsoleInputProvider : IInputProvider
{
    public Direction? ReadDirection(Direction currentDirection, TimeSpan timeout)
    {
        Direction? newDirection = null;
        var stopwatch = Stopwatch.StartNew();

        while (stopwatch.Elapsed < timeout)
        {
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(10);
                continue;
            }

            var key = Console.ReadKey(intercept: true).Key;
            var candidate = MapKeyToDirection(key);

            if (candidate.HasValue && !currentDirection.IsOpposite(candidate.Value))
            {
                newDirection = candidate.Value;
                break;
            }
        }

        return newDirection;
    }

    private static Direction? MapKeyToDirection(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            _ => null
        };
    }
}
