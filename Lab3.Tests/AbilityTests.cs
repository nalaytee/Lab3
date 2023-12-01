
namespace Lab3.Tests
{
    [TestClass]
    public class AbilityTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var ability = new Ability("Fireball", "Deals 25% more damage", true);

            Assert.AreEqual("Fireball", ability.Name);
            Assert.IsTrue(ability.IsActive);
        }
    }
}
