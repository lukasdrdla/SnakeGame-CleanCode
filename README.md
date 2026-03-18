# Snake Game - Clean Code Refactoring

Refaktorovaná verze klasické konzolové hry Snake v C#. Původní legacy kód byl kompletně přepracován podle principů Clean Code.

## Spuštění

```bash
dotnet run
```

Ovládání: šipky na klávesnici. Had sbírá bobule (cyan), narůstá a zrychluje skóre. Hra končí nárazem do zdi nebo do vlastního těla.

## Aplikované principy Clean Code

### Smysluplná jména
- Původní holandské názvy (`hoofd`, `lijf`, `schermkleur`, `tijd`, `toets`) nahrazeny anglickými (`Head`, `Body`, `ScreenColor`, atd.)
- Třídy pojmenované podle odpovědnosti: `Snake`, `Board`, `GameEngine`, `ConsoleRenderer`
- Proměnné popisují svůj účel: `currentDirection`, `berryPosition`, `initialSnakeLength`

### Malé metody s jednou odpovědností
- Původní 100+ řádků v `Main` rozděleno do malých metod
- `GameEngine.Run()` orchestruje na vysoké úrovni: `Render()`, `ProcessInput()`, `Update()`
- Každá metoda dělá jednu věc

### Typová bezpečnost místo magic values
- `string movement ("UP"/"DOWN"/...)` → `Direction` enum
- `int gameover (0/1)` → `GameState` enum
- `string buttonpressed ("yes"/"no")` → eliminováno (else-if logika)
- Paralelní `List<int>` pro x/y → `Position` record struct

### Decoupling logiky od GUI (klíčový požadavek)
- `IRenderer` interface: herní logika nikdy přímo nepracuje s `Console`
- `IInputProvider` interface: vstup je abstrahován
- `Snake`, `Board` jsou čisté doménové třídy bez závislosti na UI
- `ConsoleRenderer` je jediná třída, která zná `System.Console`
- Díky tomuto oddělení by bylo možné snadno vyměnit konzolové UI za WPF, WinForms nebo jakýkoli jiný framework

### Žádná magická čísla
- Rozměry hrací plochy, rychlost hry, počáteční délka hada → `GameSettings` record

### Konzistentní úrovně abstrakce
- `GameEngine.Run()` čte na vysoké úrovni: inicializuj, renderuj, zpracuj vstup, aktualizuj stav
- Nízkoúrovňové detaily skryty v příslušných třídách

## Struktura projektu

```
SnakeGame/
├── Program.cs                  # Vstupní bod, propojení závislostí
├── Game/
│   ├── GameEngine.cs           # Herní smyčka a orchestrace
│   ├── GameState.cs            # Enum: Running, GameOver
│   ├── GameSettings.cs         # Konfigurace hry
│   └── Direction.cs            # Enum směrů + IsOpposite() extension
├── Model/
│   ├── Position.cs             # Value object (X, Y) s Move()
│   ├── Snake.cs                # Model hada: hlava, tělo, pohyb, kolize
│   └── Board.cs                # Herní deska: hranice, bobule, kolize se zdí
├── Input/
│   ├── IInputProvider.cs       # Abstrakce vstupu
│   └── ConsoleInputProvider.cs # Konzolová implementace
└── Rendering/
    ├── IRenderer.cs            # Abstrakce vykreslování
    └── ConsoleRenderer.cs      # Konzolová implementace
```
