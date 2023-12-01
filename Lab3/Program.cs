
using Newtonsoft.Json;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Добро пожаловать в игру!");
        Console.WriteLine("1) Начать игру");
        Console.WriteLine("2) Загрузить игру");
        Console.WriteLine("3) Выход");

        int choice = GetUserChoice(1, 3);

        switch (choice)
        {
            case 1:
                StartNewGame();
                break;
            case 2:
                LoadExistingGame();
                break;
            case 3:
                Environment.Exit(0);
                break;
        }


        static void StartNewGame()
        {
            GameBoard gameBoard = new(10, 10);

            Player board = new("Доска");
            Player player1 = new("Игрок1");
            Player player2 = new("Игрок2");
            gameBoard.SetObstacles(board);


            bool IsSetupPhase = true;
            bool player1Turn = true;

            RunGameLoop(gameBoard, player1, player2, IsSetupPhase, player1Turn);
        }

        static void LoadExistingGame()
        {
            GameState gameState = LoadGame();
            if (gameState != null)
            {
                Console.WriteLine("Загружаем сохраненную игру...");
                RunGameLoop(gameState.GameBoard, gameState.Player1, gameState.Player2, gameState.IsSetupPhase, gameState.Player1Turn);
            }
            else
            {
                Console.WriteLine("Сохраненная игра не найдена. Запуск новой игры.");
                StartNewGame();
            }
        }
    }

    static void RunGameLoop(GameBoard gameBoard, Player player1, Player player2, bool IsSetupPhase, bool player1Turn)
    {
        
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
                Console.WriteLine($"Ходит {(player1Turn ? player1.Name : player2.Name)}.");
                Player currentPlayer = player1Turn ? player1 : player2;
                gameBoard.DisplayAllUnits();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Ходить");
                Console.WriteLine("2 - Атаковать");
                Console.WriteLine("3 - Способность\n");
                Console.WriteLine("4 - Сохранить");
                Console.WriteLine("5 - Выход");

                int actionChoice = GetUserChoice(1, 5);

                switch (actionChoice)
                {
                    case 1:
                        currentPlayer.MoveUnit(gameBoard);
                        break;
                    case 2:
                        currentPlayer.AttackUnit(gameBoard);
                        break;
                    case 3:
                        currentPlayer.UseActiveAbility();
                        break;
                    case 4:
                        SaveGame(gameBoard, player1, player2, IsSetupPhase, player1Turn);
                        break;
                    case 5:
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
            Console.WriteLine("Выберите:");
            Console.WriteLine("1 - Воин");
            Console.WriteLine("2 - Лучник");
            Console.WriteLine("3 - Маг");
            Console.WriteLine("4 - Паладин");
            Console.WriteLine("5 - Берсерк");
            Console.WriteLine("6 - Целитель");

            int unitTypeChoice = GetUserChoice(1, 6);

            Console.WriteLine("Введите x координату: ");
            int x = GetUserChoice(0, 9);
            int y;

            // Проверка на соответствие правилам расстановки
            if (player.Name == "Игрок1")
            {
                Console.WriteLine("Введите y координату (0, 1 или 2): ");
                y = GetUserChoice(0, 2);

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
                Console.WriteLine("Введите y координату (7, 8 или 9): ");
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
            Unit unit = player.GetUnitByName(unitName);

            if (gameBoard.IsValidPosition(x, y) && gameBoard.IsCellEmpty(x, y))
            {
                switch (unitTypeChoice)
                {
                    case 1:
                        gameBoard.SetUnit(player, unitName, x, y, 120, 15, 3, "Воин", symbol, new List<Ability>{
                            new Ability("|Разящий удар|", "+5 к атаке", true, 1),
                            new Ability("|Ладные доспехи|", "+1 к атаке", false, 4)});
                        player.SetPassiveAbility(unit);
                        break;
                    case 2:
                        gameBoard.SetUnit(player, unitName, x, y, 80, 12, 2, "Лучник", symbol, new List<Ability>{
                            new Ability("|Острые стрелы|", "+5 к атаке", true, 1),
                            new Ability("|Ретирование|", "+1 к выносливости", false, 6)});
                        player.SetPassiveAbility(unit);
                        break;
                    case 3:
                        gameBoard.SetUnit(player, unitName, x, y, 60, 20, 2, "Маг", symbol, new List<Ability>{
                            new Ability("|Огненный шар|", "+5 к атаке", true, 1),
                            new Ability("|Восстановление|", "Восстанавливает 5% здоровья за ход", false, 5)});
                        player.SetPassiveAbility(unit);
                        break;
                    case 4:
                        gameBoard.SetUnit(player, unitName, x, y, 150, 10, 2, "Паладин", symbol, new List<Ability>{
                            new Ability("|Ни шагу назад|", "-2 к выносливости, +50 к здоровью", true, 7),
                            new Ability("|Щит паладина|", "+1 к выносливости", false, 6)});
                        player.SetPassiveAbility(unit);
                        break;
                    case 5:
                        gameBoard.SetUnit(player, unitName, x, y, 80, 25, 2, "Берсерк", symbol, new List<Ability>{
                            new Ability("|Рывок|", "+2 к выносливости", true, 3),
                            new Ability("|Ярость|", "+1 к выносливости", false, 6)});
                        player.SetPassiveAbility(unit);
                        break;
                    case 6:
                        gameBoard.SetUnit(player, unitName, x, y, 80, 8, 3, "Целитель", symbol, new List<Ability>{
                            new Ability("|Лечение|", "Восстанавливает 50% своего здоровья", true, 2),
                            new Ability("|Аура целителя|", "Восстанавливает 5% здоровья за ход", false, 5)});
                        player.SetPassiveAbility(unit);
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

    static void SaveGame(GameBoard gameBoard, Player player1, Player player2, bool IsSetupPhase, bool player1Turn)
    {
        var gameState = new { GameBoard = gameBoard, Player1 = player1, Player2 = player2, IsSetupPhase, player1Turn };
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        string json = JsonConvert.SerializeObject(gameState, settings);

        // Сохранение в текущей директории
        File.WriteAllText("savefile.json", json);
    }

    static GameState LoadGame()
    {
        string path = "savefile.json";

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                Console.WriteLine("Пытаемся загрузить следующие данные:");
                Console.WriteLine(json); // Выводим содержимое файла
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var gameState = JsonConvert.DeserializeObject<GameState>(json, settings);
                return gameState;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
            }
        }
        return null;
    }
}
