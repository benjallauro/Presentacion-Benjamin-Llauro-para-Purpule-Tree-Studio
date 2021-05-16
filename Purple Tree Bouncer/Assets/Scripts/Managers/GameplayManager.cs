using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager _instance;
    public static GameplayManager Instance { get { return _instance; } }
    int coinsCollected;
    int rocksBasketed;
    [SerializeField]
    float gameplayTimeInSeconds;
    [SerializeField]
    float gameplayTimeInSecondsEasyMode;
    ReverseGameTime reverseTimer;
    [SerializeField]
    string sceneToLoad;
    Difficulty difficulty;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        difficulty = Difficulty.Instance;
        coinsCollected = 0;
        rocksBasketed = 0;
        reverseTimer = new ReverseGameTime();
        reverseTimer.StopAndReset();
        if (difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
            reverseTimer.SetTimer(gameplayTimeInSecondsEasyMode);
        else
            reverseTimer.SetTimer(gameplayTimeInSeconds);
        reverseTimer.Start();
    }
    public float GetTime() { return reverseTimer.GetTimeRemaining(); }
    public int GetRocksBasketed() { return rocksBasketed; }
    public int GetCoinsCollected() { return coinsCollected; }
    public void AddCoin()
    {
        coinsCollected++;
        Debug.Log("COINS: " + coinsCollected);
    }
    public void AddBasketedRock()
    {
        rocksBasketed++;
        Debug.Log("ROCKS BASKETED: " + rocksBasketed);
    }
    private void Update()
    {
        if(reverseTimer.Update(Time.deltaTime))
        {
            Debug.Log("GAME OVER");
            reverseTimer.StopAndReset();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}