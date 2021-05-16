using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDataText : MonoBehaviour
{
    GameplayManager gameplayManager;
    Text text;
    public enum TypeOfDataRequired
    {
        RocksBasketed,
        Time,
        CoinsCollected
    }
    public TypeOfDataRequired typeOfDataRequired;
    void Start()
    {
        gameplayManager = GameplayManager.Instance;
        text = GetComponent<Text>();
    }
    void Update()
    {
        if (typeOfDataRequired == TypeOfDataRequired.Time)
            text.text = "TIME: " + (int)gameplayManager.GetTime();
        else if (typeOfDataRequired == TypeOfDataRequired.RocksBasketed)
            text.text = "x " + gameplayManager.GetRocksBasketed();
        else
            text.text = "x " + gameplayManager.GetCoinsCollected();
    }
}
