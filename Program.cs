using SnakeGame.Game;
using SnakeGame.Input;
using SnakeGame.Rendering;

var settings = new GameSettings();
var input = new ConsoleInputProvider();
var renderer = new ConsoleRenderer(settings.BoardWidth, settings.BoardHeight);

var game = new GameEngine(settings, input, renderer);
game.Run();
