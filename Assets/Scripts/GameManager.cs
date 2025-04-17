using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives {get; private set; }
    public int coins {get; private set; }
    public new AudioSource audio;
    public AudioClip coinClip;
    public AudioClip oneUpClip;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (Instance != null){
            DestroyImmediate(gameObject);
        } else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this){
            Instance = null;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }
    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }
    public void LoadLevel(int world, int stage)
    {
       this.world = world;
       this.stage = stage;
       SceneManager.LoadScene($"{world}-{stage}"); 
    }
    public void ResetLevel(float delay)
    {
        StartCoroutine(ResetCoroutine(delay));
    }

    private IEnumerator ResetCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        lives--;
        if (lives > 0){
            LoadLevel(world, stage);
        } else {
            GameOver();
            yield break;
        }

        yield return new WaitForSeconds(0.1f);

        if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.PlayMusic(MusicType.Background);
        }
    }
    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");
        NewGame();
    }
    public void NextLevel()
    {
        // stage++; Implement this when you build more stages
        if (stage > 3){
            world++;
            stage = 1;
        }
        LoadLevel(world, stage);
    }
    /*
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        NewGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    */
    public void AddCoin()
    {
        audio.PlayOneShot(coinClip);
        coins++;
        if (coins == 100)
        {
            AddLives();
            coins = 0;
        }
    }
    public void AddLives()
    {
        audio.PlayOneShot(oneUpClip);
        lives++;
    }
}
