using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBehaviour : MonoBehaviour
{
    [SerializeField]
    ThrowSimulation throwSimulation;
    [SerializeField]
    float minTimeBetweenThrows;
    [SerializeField]
    int maxTimeBetweenThrows;
    [SerializeField]
    float minTimeBetweenThrowsEasyMode;
    [SerializeField]
    int maxTimeBetweenThrowsEasyMode;
    [SerializeField]
    float minXDistance;
    [SerializeField]
    float maxXDistance;
    GameTime timer;
    [SerializeField]
    GameObject[] projectiles;
    int nextProjectile;
    Animator anim;
    [SerializeField]
    RandomAreaPosition targetPosition;
    [SerializeField]
    Transform target;
    public float gravity = 9.8f;
    public float gravityEasyMode = 9.8f;
    bool avalibleProjectileFound;
    int counter;
    Transform myTransform;
    Difficulty difficulty;
    void Start()
    {
        timer = new GameTime();
        timer.StopAndReset();
        timer.SetTimer(Random.Range(minTimeBetweenThrows, maxTimeBetweenThrows));
        timer.Start();
        nextProjectile = 0;
        anim = GetComponentInChildren<Animator>();
        avalibleProjectileFound = false;
        counter = 0;
        myTransform = GetComponent<Transform>();
        difficulty = Difficulty.Instance;
    }

    void Update()
    {
        avalibleProjectileFound = false;
        if (timer.Update(Time.deltaTime))
        {
            if(nextProjectile < projectiles.Length)
            {
                counter = 0;
                foreach (GameObject pro in projectiles)
                {
                    if (!pro.GetComponent<Bounce>().GetBouncing() && !pro.GetComponent<ThrowSimulation>().GetMoving())
                    {
                        avalibleProjectileFound = true;
                        nextProjectile = counter;
                    }
                    counter++;
                }
            }
            if(avalibleProjectileFound)
            {
                targetPosition.ChangePositionInDeterminedRange(minXDistance, maxXDistance, myTransform);
                projectiles[nextProjectile].gameObject.SetActive(true);
                projectiles[nextProjectile].GetComponent<DissappearOnCollider>().CancelDissappear();
                if(difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
                    projectiles[nextProjectile].GetComponent<ThrowSimulation>().SetGravity(gravityEasyMode);
                else
                    projectiles[nextProjectile].GetComponent<ThrowSimulation>().SetGravity(gravity);
                projectiles[nextProjectile].GetComponent<ThrowSimulation>().StopProjectile();
                projectiles[nextProjectile].GetComponent<ThrowSimulation>().ThrowProjectile(myTransform, target);
                anim.SetTrigger("throw");
            }
            timer.StopAndReset();
            if(difficulty != null)
            {
                if (difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Normal)
                    timer.SetTimer(Random.Range(minTimeBetweenThrows, maxTimeBetweenThrows));
                else
                    timer.SetTimer(Random.Range(minTimeBetweenThrowsEasyMode, maxTimeBetweenThrowsEasyMode));
            }
            else
                timer.SetTimer(Random.Range(minTimeBetweenThrows, maxTimeBetweenThrows));
            timer.Start();
        }
    }
}