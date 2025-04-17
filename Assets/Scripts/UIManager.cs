using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private float timeLeft = 300f;
    private GameManager gameManager;
    public TextMeshProUGUI timeLeftText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI worldText;

    void Start()
    {
        gameManager = GameManager.Instance;
        UpdateUI();
    }

    void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                GameManager.Instance.ResetLevel(0f);
            }
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        timeLeftText.text = "Time " + Mathf.CeilToInt(timeLeft).ToString("000");
        coinsText.text = "Coins " + gameManager.coins;
        livesText.text = "Lives " + gameManager.lives;
        worldText.text = "World " + "1 - 1"; // Actually fix this when creating more stages
    }
}