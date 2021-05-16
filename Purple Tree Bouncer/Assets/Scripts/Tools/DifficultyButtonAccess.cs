using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButtonAccess : MonoBehaviour
{
    Difficulty difficulty;
    void Start()
    {
        difficulty = Difficulty.Instance;
    }
    public void SetEasyDifficulty() { difficulty.SetEasyDifficulty(); }
    public void SetNormalDifficulty() { difficulty.SetNormalDifficulty(); }

}
