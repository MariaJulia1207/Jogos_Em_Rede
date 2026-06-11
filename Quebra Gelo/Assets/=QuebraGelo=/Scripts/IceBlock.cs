using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public Sprite[] damageSprites; // 0: Normal, 1: Leve, 2: Médio, 3: Pesado
    private int hitCount = 0;
    private SpriteRenderer sr;

    void Awake() { sr = GetComponent<SpriteRenderer>(); }

    public void Hit()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            Destroy(gameObject);
        }
        else
        {
            sr.sprite = damageSprites[hitCount];
        }
    }
    private void OnMouseDown()
    {
        GameController.instance.HandleBlockClick(this);
    }
}