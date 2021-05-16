using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    RandomAreaPosition randomPosition;
    GameTime timer;
    [SerializeField]
    float timeBetweenCoinSpawns;
    [SerializeField]
    Coin[] coins;
    [SerializeField]
    Transform player;
    bool coinSelected;
    [SerializeField]
    float secondsBeforeCoinDissappears;
    [SerializeField]
    float secondsBeforeCoinDissappearsEasyMode;
    [SerializeField]
    int rocksBasketedNeeded;
    [SerializeField]
    int rocksBasketedNeededEasyMode;
    int rocksBasketNeededUpdated;
    Difficulty difficulty;
    GameplayManager gameplayManager;
    void Start()
    {
        difficulty = Difficulty.Instance;
        randomPosition = GetComponent<RandomAreaPosition>();
        timer = new GameTime();
        timer.SetTimer(timeBetweenCoinSpawns);
        timer.Start();
        coinSelected = false;
        gameplayManager = GameplayManager.Instance;
        rocksBasketNeededUpdated = rocksBasketedNeeded;
    }

    void Update()
    {
        if(timer.Update(Time.deltaTime) && gameplayManager.GetRocksBasketed() >= rocksBasketNeededUpdated)
        {
            if(difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
                rocksBasketNeededUpdated += rocksBasketedNeededEasyMode;
            else
                rocksBasketNeededUpdated += rocksBasketedNeeded;
            if (randomPosition != null)
                randomPosition.ChangePositionInRangeAvoidingArea(player.transform.position.x - 10, player.transform.position.x + 10);
            foreach (Coin coin in coins)
            {
                if(!coin.isActiveAndEnabled && !coinSelected)
                {
                    coin.gameObject.SetActive(true);
                    if (difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
                        coin.Appear(secondsBeforeCoinDissappearsEasyMode);
                    else
                        coin.Appear(secondsBeforeCoinDissappears);
                    coin.gameObject.transform.position = transform.position;
                    coinSelected = true;
                }
            }
            coinSelected = false;
            timer.StopAndReset();
            timer.Start();
        }
    }
}
