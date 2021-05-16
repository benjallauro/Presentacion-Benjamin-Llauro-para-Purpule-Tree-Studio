using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameTime timer;
    GameplayManager gameplayManager;
    public void Appear(float time)
    {
        timer = new GameTime();
        timer.SetTimer(time);
        timer.Start();
        gameplayManager = GameplayManager.Instance;
    }
    public void Vanish()
    {
        timer.StopAndReset();
        this.gameObject.SetActive(false);
    }
    public void Collect()
    {
        timer.StopAndReset();
        this.gameObject.SetActive(false);
        gameplayManager.AddCoin();
    }
    void Update()
    {
        if(timer.Update(Time.deltaTime))
        {
            Vanish();
        }
    }
}
