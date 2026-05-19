using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TMP_Text whiteTimerText;
    public TMP_Text blackTimerText;

    private void Update()
    {
        whiteTimerText.text =
            ChessTimer.Instance.GetFormattedTime(
                ChessTimer.Instance.whiteTime
            );

        blackTimerText.text =
            ChessTimer.Instance.GetFormattedTime(
                ChessTimer.Instance.blackTime
            );

        if (TurnManager.Instance.currentTurn
            == TeamColor.White)
        {
            whiteTimerText.color = Color.green;
            blackTimerText.color = Color.white;
        }
        else
        {
            blackTimerText.color = Color.green;
            whiteTimerText.color = Color.white;
        }
    }
}