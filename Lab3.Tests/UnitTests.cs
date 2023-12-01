
namespace Lab3.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var player = new Player("Игрок");
            var abilities = new List<Ability>
            {
                new Ability("Способность1", "Описание1", true, 1)
            };
            var unit = new Unit(player, "Юнит1", 1, 2, 100, 10, 5, "Воин", 'W', abilities);

            // Act & Assert
            Assert.AreEqual("Юнит1", unit.Name);
            Assert.AreEqual(1, unit.X);
            Assert.AreEqual(2, unit.Y);
            Assert.AreEqual(100, unit.Health);
            Assert.AreEqual(10, unit.Attack);
            Assert.AreEqual(5, unit.Endurance);
            Assert.AreEqual("Воин", unit.UnitType);
            Assert.AreEqual('W', unit.Symbol);
            Assert.AreSame(player, unit.Owner);
            Assert.AreEqual(1, unit.Abilities.Count);
            Assert.IsTrue(unit.Abilities.Contains(abilities[0]));
            Assert.IsTrue(unit.AbilityFlag);
        }
    }
}
