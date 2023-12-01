//Player.cs file:

public class Player
{
    public string Name { get; set; }
    private List<Unit> units = new();

    public Player(string name)
    {
        Name = name;
        Units = new List<Unit>();
    }

    public List<Unit> Units
    {
        get { return units; }
        set { units = value; }
    }

    public Unit GetUnitByName(string unitName)
    {
        return Units.FirstOrDefault(unit => unit.Name == unitName);
    }

    public void MoveUnit(GameBoard gameBoard)
    {
        Console.WriteLine("Выберите юнит: ");
        string unitName = Console.ReadLine();
        Unit selectedUnit = GetUnitByName(unitName);

        if (selectedUnit != null)
        {
            gameBoard.MoveUnit(selectedUnit, gameBoard);
        }
        else
        {
            Console.WriteLine("Неверное имя юнита.");
            Console.ReadLine();
        }
    }

    public void AttackUnit(GameBoard gameBoard)
    {
        Console.WriteLine("Выберите атакующего юнита: ");
        string attackingUnitName = Console.ReadLine();
        Unit attackingUnit = GetUnitByName(attackingUnitName);

        if (attackingUnit != null)
        {
            Console.WriteLine("Выберите цель атаки: ");
            string targetUnitName = Console.ReadLine();

            Unit targetUnit = gameBoard.units.FirstOrDefault(unit => unit.Name.Equals(targetUnitName, StringComparison.OrdinalIgnoreCase));

            if (targetUnit != null)
            {
                gameBoard.AttackUnit(attackingUnit, targetUnit, gameBoard);
            }
            else
            {
                Console.WriteLine("Неверное имя цели.");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Неверное имя атакующего.");
            Console.ReadLine();
        }
    }
}