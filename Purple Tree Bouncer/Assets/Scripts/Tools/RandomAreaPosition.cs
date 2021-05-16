using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAreaPosition : MonoBehaviour
{
    [SerializeField]
    float defaultMinXDistance;
    [SerializeField]
    int defaultMaxXDistance;
    [SerializeField]
    float YPosition;

    Transform myTransform;
    Vector3 startPosition;
    Vector3 areaToCheckAvoid;
    void Start()
    {
        myTransform = GetComponent<Transform>();
        startPosition = myTransform.position;
    }
    public void ChangePositionInRange()
    {
        myTransform.position = new Vector3(startPosition.x + Random.Range(defaultMinXDistance, defaultMaxXDistance), YPosition, transform.position.z);
    }
    public void ChangePositionInDeterminedRange(float _minXDistance, float _maxXDistance, Transform _startPosition)
    {
        myTransform.position = new Vector3(Random.Range(_startPosition.transform.position.x + _minXDistance, _maxXDistance), YPosition, transform.position.z);
    }
    public void ChangePositionInRangeAvoidingArea(float minXAreaToAvoid, float maxXAreaToAvoid)
    {
        areaToCheckAvoid = new Vector3(startPosition.x + Random.Range(defaultMinXDistance, defaultMaxXDistance), YPosition, transform.position.z);
        if (areaToCheckAvoid.x > minXAreaToAvoid && areaToCheckAvoid.x < maxXAreaToAvoid)
        {
            bool left = (Random.value > 0.5f);
            if (left)
                myTransform.position = new Vector3(minXAreaToAvoid, YPosition, transform.position.z);
            else
                myTransform.position = new Vector3(maxXAreaToAvoid, YPosition, transform.position.z);
        }
        else
            myTransform.position = areaToCheckAvoid;
    }
}