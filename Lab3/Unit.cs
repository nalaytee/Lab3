
public class Unit
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public double Health { get; set; }
    public double Attack { get; set; }
    public int Endurance { get; set; }
    public string UnitType { get; set; }
    public List<Ability>? Abilities { get; set; }
    public char Symbol { get; set; }
    public Player Owner { get; set; }
    public bool AbilityFlag { get; set; }

    public Unit(Player owner, string name, int x, int y, int health, int attack, int endurance, string unitType, char symbol, List<Ability> abilities, bool abilityFlag = true)
    {
        Name = name;
        X = x;
        Y = y;
        Health = health;
        Attack = attack;
        Endurance = endurance;
        UnitType = unitType;

        if (unitType != "Препядствие")
        {
            Abilities = new List<Ability>(abilities);
        }
        else
        {
            Abilities = new List<Ability>(); // Создаем пустой список для препятствий
        }

        Symbol = symbol;
        Owner = owner;
        AbilityFlag = abilityFlag;
    }

    public void DisplayAbilities()
    {
        Console.WriteLine($"Способности:");

        if (Abilities.Count != 0)
        {
            foreach (var ability in Abilities)
            {

                Console.WriteLine($"{ability.Name} - {ability.Description} ({(ability.IsActive ? "Активная" : "Пассивная")})");
            }
        }
        else
        {
            Console.WriteLine("(нет)");
        }
    }

    public void GetActiveAbility(Ability ability, Unit unit)
    {
        if (unit.AbilityFlag)
        {
            if (ability.IsActive) {
                switch (ability.AbilityID)
                {
                    case 1:
                        // Увеличение атаки (активная)
                        unit.Attack += 5;
                        Console.WriteLine($"{unit.Name} получает +5 к атаке");
                        break;
                    case 2:
                        // Лечение (активная)
                        unit.Health *= 0.25;
                        Console.WriteLine($"{unit.Name} восстанавливает 50% своего здоровья!");
                        break;
                    case 3:
                        // Увеличение мобильности (активная)
                        unit.Endurance += 2;
                        Console.WriteLine($"{unit.Name} может передвигаться на 2 клетки дальше!");
                        break;
                    case 4:
                        // Увеличение мобильности (активная)
                        unit.Endurance += 1;
                        unit.Health += 50;
                        Console.WriteLine($"{unit.Name} может передвигаться на 1 клетки дальше!");
                        break;
                    default:
                        Console.WriteLine("Неизвестная способность.");
                        break;
                }
            }
        }
    }

    public void GetPassiveAbility(Ability ability, Unit unit)
    {
        if (!ability.IsActive)
        {
            switch (ability.AbilityID)
            {
                case 1:
                    // Увеличение атаки (пассивная)
                    unit.Attack += 1;
                    Console.WriteLine($"{unit.Name} получает +1 к атаке");
                    break;
                case 2:
                    // Лечение (пассивная)
                    unit.Health *= 0.05;
                    Console.WriteLine($"{unit.Name} восстанавливает 5% своего здоровья!");
                    break;
                case 3:
                    // Увеличение мобильности (пассивная)
                    unit.Endurance += 1;
                    Console.WriteLine($"{unit.Name} может передвигаться на 1 клетки дальше!");
                    break;

                default:
                    Console.WriteLine("Неизвестная способность.");
                    break;
            }
        }
        Console.WriteLine();
    }
}
