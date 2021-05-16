using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    private static Difficulty _instance;
    public static Difficulty Instance { get { return _instance; } }
    public enum SelectedDifficulty
    {
        Easy,
        Normal
    }
    SelectedDifficulty selectedDifficulty;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }

    public SelectedDifficulty GetSelectedDifficulty() { return selectedDifficulty; }
    public void SetEasyDifficulty() { selectedDifficulty = SelectedDifficulty.Easy; }
    public void SetNormalDifficulty() { selectedDifficulty = SelectedDifficulty.Normal; }

}
