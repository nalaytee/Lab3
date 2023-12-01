
namespace Lab3.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Constructor_AssignsNameCorrectly()
        {
            var player = new Player("Игрок1");
            Assert.AreEqual("Игрок1", player.Name);
        }

        [TestMethod]
        public void AddUnit_IncreasesUnitCount()
        {
            var player = new Player("Игрок");
            var unit = new Unit(player, "Юнит1", 0, 0, 100, 10, 5, "Воин", 'W', new List<Ability>());
            player.Units.Add(unit);

            Assert.AreEqual(1, player.Units.Count);
        }

        [TestMethod]
        public void GetUnitByName_ReturnsCorrectUnit()
        {
            var player = new Player("Игрок");
            var unit = new Unit(player, "Юнит1", 0, 0, 100, 10, 5, "Воин", 'W', new List<Ability>());
            player.Units.Add(unit);

            var retrievedUnit = player.GetUnitByName("Юнит1");

            Assert.IsNotNull(retrievedUnit);
            Assert.AreEqual("Юнит1", retrievedUnit.Name);
        }

        [TestMethod]
        public void GetUnitByName_ReturnsNullForUnknownName()
        {
            var player = new Player("Игрок");
            var retrievedUnit = player.GetUnitByName("Неизвестный Юнит");

            Assert.IsNull(retrievedUnit);
        }
    }
}
