using UnityEngine;

public class ChessTimer : MonoBehaviour
{
    public static ChessTimer Instance;

    [Header("Tempo Inicial (segundos)")]
    public float whiteTime = 300f;
    public float blackTime = 300f;

    [Header("Incremento por jogada")]
    public float increment = 2f;

    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (gameEnded)
            return;

        if (TurnManager.Instance.currentTurn ==
            TeamColor.White)
        {
            whiteTime -= Time.deltaTime;

            if (whiteTime <= 0)
            {
                whiteTime = 0;
                EndGame(TeamColor.Black);
            }
        }
        else
        {
            blackTime -= Time.deltaTime;

            if (blackTime <= 0)
            {
                blackTime = 0;
                EndGame(TeamColor.White);
            }
        }
    }

    public void AddIncrement(TeamColor team)
    {
        if (team == TeamColor.White)
        {
            whiteTime += increment;
        }
        else
        {
            blackTime += increment;
        }
    }

    void EndGame(TeamColor winner)
    {
        gameEnded = true;

        Debug.Log("Tempo esgotado!");
        Debug.Log("Vencedor: " + winner);
    }

    public string GetFormattedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}",
            minutes,
            seconds);
    }
}