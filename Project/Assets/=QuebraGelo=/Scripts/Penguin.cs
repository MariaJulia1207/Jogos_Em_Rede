using UnityEngine;

public class Penguin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Se o Pinguim cair do tabuleiro (Y muito baixo)
        if (transform.position.y < -5f && !GameController.instance.isGameOver)
        {
            GameController.instance.TriggerGameOver(GameController.instance.currentPlayer);
        }
    }
}
