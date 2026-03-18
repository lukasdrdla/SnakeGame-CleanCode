using SnakeGame.Input;
using SnakeGame.Model;
using SnakeGame.Rendering;

namespace SnakeGame.Game;

public class GameEngine
{
    private readonly GameSettings _settings;
    private readonly IInputProvider _input;
    private readonly IRenderer _renderer;

    private Snake _snake = null!;
    private Board _board = null!;
    private int _score;
    private GameState _state;

    public GameEngine(GameSettings settings, IInputProvider input, IRenderer renderer)
    {
        _settings = settings;
        _input = input;
        _renderer = renderer;
    }

    public void Run()
    {
        Initialize();

        while (_state == GameState.Running)
        {
            Render();
            ProcessInput();
            Update();
        }

        _renderer.DrawGameOver(_score);
    }

    private void Initialize()
    {
        var startPosition = new Position(_settings.BoardWidth / 2, _settings.BoardHeight / 2);

        _snake = new Snake(startPosition);
        _board = new Board(_settings.BoardWidth, _settings.BoardHeight);
        _score = 0;
        _state = GameState.Running;

        _renderer.Initialize();
        _renderer.DrawBorder();
    }

    private void Render()
    {
        _renderer.ClearPlayArea();
        _renderer.DrawSnake(_snake);
        _renderer.DrawBerry(_board.BerryPosition);
        _renderer.DrawScore(_score);
    }

    private void ProcessInput()
    {
        var timeout = TimeSpan.FromMilliseconds(_settings.TickDurationMs);
        var newDirection = _input.ReadDirection(_snake.CurrentDirection, timeout);

        if (newDirection.HasValue)
        {
            _snake.ChangeDirection(newDirection.Value);
        }
    }

    private void Update()
    {
        _snake.Move();

        if (_board.IsWall(_snake.Head) || _snake.CollidesWithSelf())
        {
            _state = GameState.GameOver;
            return;
        }

        if (_board.IsBerryEaten(_snake.Head))
        {
            _score++;
            _board.RespawnBerry();
        }

        int maxBodyLength = _settings.InitialSnakeLength + _score;
        while (_snake.Length > maxBodyLength)
        {
            _snake.TrimTail();
        }
    }
}
