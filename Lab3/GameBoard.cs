//GameBoard.cs file:

public class GameBoard
{
    public int width;
    public int height;
    public char[,] grid;
    public List<Unit> units = new();

    public GameBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new char[height, width];
        units = new List<Unit>();
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[i, j] = '.';
            }
        }
    }

    public void SetObstacles(Player player)
    {
        // Расставляем препятствия по своему усмотрению
        units.Add(new Unit(player, "", 2, 0, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 2, 1, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 2, 2, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 2, 7, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 2, 8, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 2, 9, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 7, 0, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 7, 1, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 7, 2, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 7, 7, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 7, 8, 0, 0, 0, "Препядствие", '#', new List<Ability>()));
        units.Add(new Unit(player, "", 7, 9, 0, 0, 0, "Препядствие", '#', new List<Ability>()));

        // Заменяем символы препятствий в массиве grid
        foreach (var obstacle in units)
        {
            grid[obstacle.X, obstacle.Y] = obstacle.Symbol;
        }
        units.AddRange(units);
    }

    public void SetUnit(Player player, string unitName, int x, int y, int health, int attack, int endurance, string unitType, char symbol, List<Ability> abilities)
    {
        Unit unit = new(player, unitName, x, y, health, attack, endurance, unitType, symbol, new List<Ability>(abilities));
        player.Units.Add(unit);
        units.Add(unit);
        grid[y, x] = symbol;
    }

    public void DisplayAllUnits()
    {
        int i = 1;
        foreach (var unit in units)
        {
            if (unit.Owner.Name != "Доска")
            {
                Console.WriteLine($"---------------------------------------------------------------");
                Console.WriteLine($"Юнит {i} | Владелец: {unit.Owner.Name} Имя: {unit.Name}({unit.Symbol}), Здаровье: {unit.Health}, Атака: {unit.Attack}");
                Console.WriteLine($"       | Выносливость: {unit.Endurance}, Тип: {unit.UnitType}, Символ: {unit.Symbol}");
                Console.WriteLine($"---------------------------------------------------------------");
                unit.DisplayAbilities();
                Console.WriteLine($"---------------------------------------------------------------");
                i++;
            }
        }
        Console.WriteLine("");
    }

    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height && IsCellEmpty(x, y) && !IsObstacle(x, y);
    }

    private bool IsObstacle(int x, int y)
    {
        return units.Exists(obstacle => obstacle.X == y && obstacle.Y == x);
    }

    public bool IsCellEmpty(int x, int y)
    {
        return grid[y, x] == '.';
    }

    public void MoveUnit(Unit unit, GameBoard gameBoard)
    {
        Console.WriteLine("Выберите направление: 1 - Вверх, 2 - Вправо, 3 - Вниз, 4 - Влево, 5 - Отмена");
        int directionChoice = int.Parse(Console.ReadLine());

        if (directionChoice >= 1 && directionChoice <= 4)
        {
            Console.WriteLine("Количесво клеток: ");
            int distance = int.Parse(Console.ReadLine());

            int targetX = unit.X;
            int targetY = unit.Y;

            switch (directionChoice)
            {
                case 1: // Up
                    targetY += distance;
                    break;
                case 2: // Right
                    targetX += distance;
                    break;
                case 3: // Down
                    targetY -= distance;
                    break;
                case 4: // Left
                    targetX -= distance;
                    break;
            }

            if (IsValidMove(unit, targetX, targetY, distance, gameBoard))
            {
                grid[unit.Y, unit.X] = '.';
                unit.X = targetX;
                unit.Y = targetY;
                grid[unit.Y, unit.X] = unit.Symbol;
            }
            else
            {
                Console.WriteLine("Неверный ввод. Проверьте свободна ли клитка и хватает ли юниту выносливости.");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод.");
            Console.ReadLine();
        }
    }

    public void AttackUnit(Unit attackingUnit, Unit targetUnit, GameBoard gameBoard)
    {
        Console.WriteLine($"Атакующий: {attackingUnit.Name}, Владелец: {attackingUnit.Owner.Name}");
        Console.WriteLine($"Атакованный: {targetUnit.Name}, Владелец: {targetUnit.Owner.Name}");

        if (attackingUnit.Owner != targetUnit.Owner)
        {
            // Perform the attack
            targetUnit.Health -= attackingUnit.Attack;

            Console.WriteLine($"{attackingUnit.Name} атакует {targetUnit.Name}!");

            if (targetUnit.Health <= 0)
            {
                RemoveUnit(targetUnit);
                Console.WriteLine($"{targetUnit.Name} повержен!");
            }
        }
        else
        {
            Console.WriteLine("Вы не можете атаковать свои юниты.");
        }

        Console.ReadLine();
    }

    private bool IsValidMove(Unit unit, int targetX, int targetY, int distance, GameBoard gameBoard)
    {
        if (units.Exists(obstacle => obstacle.X == targetX && obstacle.Y == targetY))
        {
            return false;
        }

        int distanceX = Math.Abs(targetX - unit.X);
        int distanceY = Math.Abs(targetY - unit.Y);

        if ((distanceX + distanceY) <= unit.Endurance && IsCellEmpty(targetX, targetY))
        {
            return true;
        }

        return false;
    }

    private void RemoveUnit(Unit unit)
    {
        grid[unit.Y, unit.X] = '.';
        units.Remove(unit);
    }

    public void Display()
    {
        Console.WriteLine(" +----------");

        for (int i = height - 1; i >= 0; i--)
        {
            Console.Write(i + "|");
            for (int j = 0; j < width; j++)
            {
                char cellContent = grid[i, j];
                Console.Write(cellContent);
            }
            Console.WriteLine();
        }

        Console.WriteLine(" +----------");
        Console.WriteLine("  0123456789\n");
    }
}