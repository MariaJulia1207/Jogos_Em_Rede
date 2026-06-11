using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public LayerMask iceLayer;
    public LayerMask borderLayer;
    public float gridSize = 1.1f; // Ajuste conforme o tamanho do seu sprite

    public void CheckStability()
    {
        // Pega todos os blocos ativos na cena
        IceBlock[] allBlocks = FindObjectsOfType<IceBlock>();
        
        foreach (var block in allBlocks)
        {
            if (!IsConnectedToBorder(block.transform.position))
            {
                // Aplica física para ele cair
                block.gameObject.AddComponent<Rigidbody2D>();
            }
        }
    }

    bool IsConnectedToBorder(Vector2 position)
    {
        Queue<Vector2> queue = new Queue<Vector2>();
        HashSet<Vector2> visited = new HashSet<Vector2>();
        queue.Enqueue(position);

        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        while (queue.Count > 0)
        {
            Vector2 current = queue.Dequeue();

            foreach (var dir in directions)
            {
                Vector2 neighbor = current + (dir * gridSize);
                
                // Se encontrar a borda, o bloco está seguro
                if (Physics2D.OverlapPoint(neighbor, borderLayer)) return true;

                // Se encontrar outro bloco não visitado, continua a busca
                if (Physics2D.OverlapPoint(neighbor, iceLayer) && !visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
        return false;
    }
}