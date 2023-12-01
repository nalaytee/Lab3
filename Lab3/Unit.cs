//Unit.cs file:

public class Unit
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Endurance { get; set; }
    public string UnitType { get; set; }
    public List<Ability>? Abilities { get; set; }
    public char Symbol { get; set; }
    public Player Owner { get; set; }

    public Unit(Player owner, string name, int x, int y, int health, int attack, int endurance, string unitType, char symbol, List<Ability> abilities)
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
}