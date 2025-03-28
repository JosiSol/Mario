using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerUp 
    {
        Coin,
        OneUp,
        Star,
        MagicMushroom
    }
    public PowerUp type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }
    private void Collect(GameObject player)
    {
        switch (type)
        {
            case PowerUp.Coin:
                GameManager.Instance.AddCoin(); 
                break;
            case PowerUp.OneUp:
                GameManager.Instance.AddLives();
                break;
            case PowerUp.Star:
                player.GetComponent<Player>().StarPower();
                break;
            case PowerUp.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
        }
        Destroy(gameObject);
    }
}
