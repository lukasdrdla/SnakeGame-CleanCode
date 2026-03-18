namespace SnakeGame.Game;

public record GameSettings(
    int BoardWidth = 32,
    int BoardHeight = 16,
    int TickDurationMs = 500,
    int InitialSnakeLength = 5
);
