using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.AddCoin();      
        StartCoroutine(Animate()); 
    }
    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 targetPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, targetPosition);
        yield return Move(targetPosition, restingPosition);

        Destroy(gameObject);
    }
    private IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;
    }
}
