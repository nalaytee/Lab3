
public class Ability
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int AbilityID { get; set; }

    public Ability(string name, string description, bool isActive, int abilityID)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        AbilityID = abilityID;
    }
}