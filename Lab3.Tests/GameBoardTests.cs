
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
            var player = new Player("�����");
            string unitName = "����1";
            char unitSymbol = 'W';
            gameBoard.SetUnit(player, unitName, 0, 0, 100, 10, 5, "����", unitSymbol, new List<Ability>());

            var addedUnit = gameBoard.units.FirstOrDefault(u => u.Name == unitName);
            Assert.IsNotNull(addedUnit);
            Assert.AreEqual(unitSymbol, gameBoard.grid[0, 0]);
        }

        [TestMethod]
        public void SetObstacles_CorrectlyPlacesObstacles()
        {
            var gameBoard = new GameBoard(10, 10);
            var player = new Player("�����");
            gameBoard.SetObstacles(player);

            // ���������, ��� ����������� ��������� � ������ ������
            Assert.IsTrue(gameBoard.units.Any(u => u.UnitType == "�����������"));

            // ���������, ��� ������� ����������� ��������� ��������� �� �����
            Assert.AreEqual('#', gameBoard.grid[2, 0]);
            Assert.AreEqual('#', gameBoard.grid[2, 1]);
            // ��������� ������ ������� ����������� � ����������� �� ����� ������ �� �����������
        }
    }
}
