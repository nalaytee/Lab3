
public class GameState
{
    public GameBoard GameBoard { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public bool IsSetupPhase { get; set; }
    public bool Player1Turn { get; set; }
}
