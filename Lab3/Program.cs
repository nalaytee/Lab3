//Program.cs file:

class Program
{
    static void Main()
    {
        GameBoard gameBoard = new(10, 10);

        Player board = new("Доска");
        Player player1 = new("Игрок1");
        Player player2 = new("Игрок2");

        gameBoard.SetObstacles(board);
        bool IsSetupPhase = true;

        while (true)
        {
            Console.Clear();
            gameBoard.Display();

            if (IsSetupPhase)
            {
                // Фаза расстановки
                SetupPhase(player1, gameBoard);
                SetupPhase(player2, gameBoard);
                IsSetupPhase = false;
            }
            else
            {
                // Фаза обычной игры
                bool player1Turn = true;

                Console.WriteLine($"Ходит {(player1Turn ? player1.Name : player2.Name)}.");
                Player currentPlayer = player1Turn ? player1 : player2;
                gameBoard.DisplayAllUnits();
                Console.WriteLine("Выберите действие: 1 - Ходить, 2 - Атаковать, 3 - Выход");

                int actionChoice = GetUserChoice(1, 3);

                switch (actionChoice)
                {
                    case 1:
                        currentPlayer.MoveUnit(gameBoard);
                        break;
                    case 2:
                        currentPlayer.AttackUnit(gameBoard);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        Console.ReadLine();
                        break;
                }

                if (IsGameFinished(player1, player2))
                {
                    Console.WriteLine("Игра окончена!");
                    Environment.Exit(0);
                }

                player1Turn = !player1Turn; // Переключение хода на следующего игрока
            }
        }
    }

    static void SetupPhase(Player player, GameBoard gameBoard)
    {
        Console.Clear();
        gameBoard.Display();

        int endOfSetupPhase = 1;
        Console.WriteLine($"{player.Name}, фаза расстановки. У вас есть {endOfSetupPhase} ходов, чтобы расставить свои юниты.");

        for (int turn = 1; turn <= endOfSetupPhase; turn++)
        {
            Console.WriteLine($"{turn} - {player.Name}, расставьте свои юниты:");
            Console.WriteLine("Выберите: 1 - Воин, 2 - Лучник, 3 - Маг, 4 - Паладин, 5 - Берсерк, 6 - Целитель");
            int unitTypeChoice = GetUserChoice(1, 6);

            Console.WriteLine("Введите x координату: ");
            int x = GetUserChoice(0, 9);
            int y;

            // Проверка на соответствие правилам расстановки
            if (player.Name == "Игрок1")
            {
                Console.WriteLine("Введите y координату (0, 1, or 2): ");
                y = GetUserChoice(1, 2);

                if (!(y >= 0 && y <= 2))
                {
                    Console.WriteLine("Неверная координата y.");
                    Console.ReadLine();
                    turn--; // Повторить попытку для того же юнита
                    continue;
                }
            }
            else // Player2
            {
                Console.WriteLine("Введите y координату (7, 8, or 9): ");
                y = GetUserChoice(7, 9);

                if (!(y >= 7 && y <= 9))
                {
                    Console.WriteLine("Неверная координата y.");
                    Console.ReadLine();
                    turn--; // Повторить попытку для того же юнита
                    continue;
                }
            }

            Console.WriteLine("Дайте имя юниту: ");
            string unitName = Console.ReadLine();
            Console.WriteLine("Введите символ для обозначения юнита: ");
            char symbol = char.Parse(Console.ReadLine());

            if (gameBoard.IsValidPosition(x, y) && gameBoard.IsCellEmpty(x, y))
            {
                switch (unitTypeChoice)
                {
                    case 1:
                        gameBoard.SetUnit(player, unitName, x, y, 120, 15, 3, "Воин", symbol, new List<Ability>{
                            new Ability("|Разящий удар|", "+5 к атаке на 3 хода", true),
                            new Ability("|Ладные доспехи|", "10% шанс поглатить 30% урона", false)});
                        break;
                    case 2:
                        gameBoard.SetUnit(player, unitName, x, y, 80, 12, 2, "Лучник", symbol, new List<Ability>{
                            new Ability("|Острые стрелы|", "+2 к атаке на 5 ходов", true),
                            new Ability("|Ретирование|", "+1 к выносливости,\nесли уровень здоровья меньше 25%", false)});
                        break;
                    case 3:
                        gameBoard.SetUnit(player, unitName, x, y, 60, 20, 2, "Маг", symbol, new List<Ability>{
                            new Ability("|Огненный шар|", "Наносит на 25% больше урона,\n чем обычная атака", true),
                            new Ability("|Магический щит|", "25% шанс поглатить 50% урона", false)});
                        break;
                    case 4:
                        gameBoard.SetUnit(player, unitName, x, y, 150, 10, 2, "Паладин", symbol, new List<Ability>{
                            new Ability("|Ни шагу назад|", "-2 к выносливости, +50 к здоровью", true),
                            new Ability("|Щит паладина|", "Если уровень здоровья меньше 25%, урон x2", false)});
                        break;
                    case 5:
                        gameBoard.SetUnit(player, unitName, x, y, 80, 25, 2, "Берсерк", symbol, new List<Ability>{
                            new Ability("|Рывок|", "+2 к выносливости на один ход", true),
                            new Ability("|Ярость|", "10% шанс нанести x2 урон", false)});
                        break;
                    case 6:
                        gameBoard.SetUnit(player, unitName, x, y, 80, 8, 3, "Целитель", symbol, new List<Ability>{
                            new Ability("|Лечение|", "Восстанавливает 50% здоровья,\nвыбранному юниту в радиусе 1 клетки", true),
                            new Ability("|Аура целителя|", "Каждый ход восстанавливает\n2 единицы здоровья союзным юнитам", false)});
                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
                Console.ReadLine();
                turn--; // Повторить попытку для того же юнита
            }
        }

        Console.WriteLine($"{player.Name}, фаза расстановки завершена.");
    }

    static int GetUserChoice(int minValue, int maxValue)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= minValue && choice <= maxValue)
            {
                return choice;
            }
            else
            {
                Console.WriteLine($"Неверный ввод. Пожалуйста введите число между {minValue} и {maxValue}.");
            }
        }
    }

    static bool IsGameFinished(Player player1, Player player2)
    {
        return player1.Units.Count == 0 || player2.Units.Count == 0;
    }
}