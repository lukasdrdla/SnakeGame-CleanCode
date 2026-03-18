using SnakeGame.Game;

namespace SnakeGame.Input;

public interface IInputProvider
{
    Direction? ReadDirection(Direction currentDirection, TimeSpan timeout);
}
