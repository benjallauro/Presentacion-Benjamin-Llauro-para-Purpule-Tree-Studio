using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAmplifier : MonoBehaviour
{
    Difficulty difficulty;
    [SerializeField]
    [Range(0, 2)]
    float verticalAmplifierNumber;
    [SerializeField]
    [Range(0, 5)]
    float horizontalAmplifierNumber;
    [SerializeField]
    [Range(0, 2)]
    float verticalAmplifierNumberEasyMode;
    [SerializeField]
    [Range(0, 5)]
    float horizontalAmplifierNumberEasyMode;
    [TextArea]
    [Tooltip("Comentario")]
    public string Notes = "Se debe tener en cuenta que si los objetos tienen rango aleatorio de angulo y gravedad, esto afectara al comportamiento del amplificador";
    private void Start()
    {
        difficulty = Difficulty.Instance;
    }
    public float GetVerticalAmplifierNumber()
    {
        if (difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
            return verticalAmplifierNumberEasyMode;
        else
            return verticalAmplifierNumber;
    }
    public float GetHorizontalAmplifierNumber()
    {
        if (difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
            return horizontalAmplifierNumberEasyMode;
        else
            return horizontalAmplifierNumber;
    }

}