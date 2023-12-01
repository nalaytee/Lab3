
namespace Lab3.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var player = new Player("Player");
            var unit = new Unit(player, "Warrior", 0, 0, 100, 10, 5, "Warrior", 'W', new List<Ability>());

            Assert.AreEqual("Warrior", unit.Name);
            Assert.AreEqual(100, unit.Health);
        }
    }
}
