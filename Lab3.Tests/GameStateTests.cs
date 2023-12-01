
namespace Lab3.Tests
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var gameBoard = new GameBoard(10, 10);
            var player1 = new Player("Игрок1");
            var player2 = new Player("Игрок2");
            var isSetupPhase = true;
            var player1Turn = true;

            // Act
            var gameState = new GameState
            {
                GameBoard = gameBoard,
                Player1 = player1,
                Player2 = player2,
                IsSetupPhase = isSetupPhase,
                Player1Turn = player1Turn
            };

            // Assert
            Assert.AreSame(gameBoard, gameState.GameBoard);
            Assert.AreSame(player1, gameState.Player1);
            Assert.AreSame(player2, gameState.Player2);
            Assert.AreEqual(isSetupPhase, gameState.IsSetupPhase);
            Assert.AreEqual(player1Turn, gameState.Player1Turn);
        }
    }
}