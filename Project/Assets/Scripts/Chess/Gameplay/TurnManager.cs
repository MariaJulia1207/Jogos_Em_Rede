using UnityEngine;

public enum TeamColor
{
    White,
    Black
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public TeamColor currentTurn =
        TeamColor.White;

    private void Awake()
    {
        Instance = this;
    }

    public void EndTurn()
    {
        ChessTimer.Instance
            .AddIncrement(currentTurn);

        currentTurn =
            currentTurn == TeamColor.White
                ? TeamColor.Black
                : TeamColor.White;

        Debug.Log("Turno de: " + currentTurn);
    }
}