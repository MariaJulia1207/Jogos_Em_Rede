using UnityEngine;
using UnityEngine.UI; // Para manipular a UI (nomes, telas)
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance; // Singleton para fácil acesso

    public int currentPlayer = 1; // 1 ou 2
    public bool isGameOver = false;
    
    [Header("UI")]
    public GameObject gameOverPanel;
    public Text winnerText;

    private void Awake()
    {
        instance = this;
    }

    // Chamado quando um jogador clica em um bloco
    public void HandleBlockClick(IceBlock block)
    {
        if (isGameOver) return;

        // Executa a lógica do bloco
        block.Hit();

        // Verifica estabilidade após a ação
        FindObjectOfType<BoardManager>().CheckStability();

        // Troca o turno
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        Debug.Log("Turno do jogador: " + currentPlayer);
    }

    public void TriggerGameOver(int loserPlayer)
    {
        isGameOver = true;
        int winner = (loserPlayer == 1) ? 2 : 1;
        
        gameOverPanel.SetActive(true);
        winnerText.text = "Jogador " + winner + " venceu! O pinguim caiu.";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}