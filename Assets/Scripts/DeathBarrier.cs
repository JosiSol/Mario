using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(3f);
        }
        else 
        {
            Destroy(collision.gameObject);
        }
    }
}
