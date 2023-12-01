
namespace Lab3.Tests
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void GameBoard_Initialization_CreatesCorrectSize()
        {
            var gameBoard = new GameBoard(10, 10);
            Assert.AreEqual(10, gameBoard.width);
            Assert.AreEqual(10, gameBoard.height);
        }

        [TestMethod]
        public void SetUnit_AddsUnitToGameBoard()
        {
            var gameBoard = new GameBoard(10, 10);
            var player = new Player("Player");
            gameBoard.SetUnit(player, "Unit", 0, 0, 100, 10, 5, "Warrior", 'W', new List<Ability>());

            Assert.IsTrue(gameBoard.IsValidPosition(0, 0));
        }
    }
}