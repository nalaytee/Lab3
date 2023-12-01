
namespace Lab3.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Constructor_AssignsNameCorrectly()
        {
            var player = new Player("Player1");
            Assert.AreEqual("Player1", player.Name);
        }

        [TestMethod]
        public void AddUnit_IncreasesUnitCount()
        {
            var player = new Player("Player");
            var unit = new Unit(player, "Unit", 0, 0, 100, 10, 5, "Warrior", 'W', new List<Ability>());
            player.Units.Add(unit);

            Assert.AreEqual(1, player.Units.Count);
        }
    }
}
