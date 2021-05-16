using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    bool bouncing;
    ThrowSimulation throwSimulation;
    Transform myPosition;
    [SerializeField]
    float minXDistance;
    [SerializeField]
    float maxXDistance;
    [SerializeField]
    float YTargetPosition;
    [SerializeField]
    float minAngle;
    [SerializeField]
    float maxAngle;
    [SerializeField]
    GameObject target;
    [SerializeField]
    Transform finalTarget;
    [SerializeField]
    float bounceGravity;
    [SerializeField]
    float bounceGravityEasyMode;
    float verticalAmplifier;
    float horizontalAmplifier;
    bool firstBounce;
    float numberChangingAngle;
    float numberChangingHorizontal;
    bool maxPassed;
    Difficulty difficulty;
    void Start()
    {
        throwSimulation = GetComponent<ThrowSimulation>();
        myPosition = GetComponent<Transform>();
        bouncing = false;
        firstBounce = true;
        verticalAmplifier = 1;
        horizontalAmplifier = 1;
        numberChangingAngle = 1;
        maxPassed = false;
        difficulty = Difficulty.Instance;
    }
    public bool GetBouncing() { return bouncing; }
    public void SetBouncing(bool _bouncing) { bouncing = _bouncing; }
    public void Restart()
    {
        verticalAmplifier = 1;
        horizontalAmplifier = 1;
        firstBounce = true;
        numberChangingAngle = 1;
        maxPassed = false;
        target.transform.position = new Vector3(-1000, target.transform.position.y, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HeadPlatform")
        {
            bouncing = true;
            if (firstBounce)
            {
                float amplifierCheck = other.gameObject.GetComponent<BounceAmplifier>().GetVerticalAmplifierNumber();
                    numberChangingAngle *= (other.gameObject.GetComponent<BounceAmplifier>().GetVerticalAmplifierNumber() * 1.5f);
                if (amplifierCheck < 1)
                    numberChangingAngle = -(numberChangingAngle) * (10 - amplifierCheck);
                else if (amplifierCheck == 1)
                    numberChangingAngle = 0;
                firstBounce = false;
                numberChangingHorizontal = other.gameObject.GetComponent<BounceAmplifier>().GetHorizontalAmplifierNumber();
            }
            else
            {
                verticalAmplifier += numberChangingAngle;
                if(numberChangingHorizontal != 1)
                    horizontalAmplifier *= numberChangingHorizontal;
            }
            target.transform.localPosition = new Vector3(transform.position.x + Random.Range(minXDistance, maxXDistance) * horizontalAmplifier, YTargetPosition, transform.position.z);
            if(difficulty != null && difficulty.GetSelectedDifficulty() == Difficulty.SelectedDifficulty.Easy)
                throwSimulation.SetGravity(bounceGravityEasyMode);
            else
                throwSimulation.SetGravity(bounceGravity);
            throwSimulation.StopProjectile();
            if (verticalAmplifier + maxAngle >= 90)
                maxPassed = true;
            if (target.transform.localPosition.x < finalTarget.transform.position.x && !maxPassed)
                throwSimulation.ThrowProjectile(myPosition, target.GetComponent<Transform>(), minAngle + verticalAmplifier, maxAngle + verticalAmplifier);
            else if(!maxPassed)
            {
                throwSimulation.ThrowProjectile(myPosition, finalTarget, minAngle, maxAngle);
                target.transform.position = new Vector3(500, target.transform.position.y, 0);
            }
            else
                throwSimulation.ThrowProjectile(myPosition, target.GetComponent<Transform>(), 85 , 85);

        }
    }
}