
namespace Lab3.Tests
{
    [TestClass]
    public class AbilityTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            string name = "Огенный шар"; 
            var ability = new Ability(name, "+5 к урону", true, 1);

            Assert.AreEqual(name, ability.Name);
            Assert.IsTrue(ability.IsActive);
        }
    }
}
