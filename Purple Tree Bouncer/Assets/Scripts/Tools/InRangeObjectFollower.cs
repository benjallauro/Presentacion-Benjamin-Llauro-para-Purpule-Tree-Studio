using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeObjectFollower : MonoBehaviour
{
    [SerializeField]
    Transform objectToFollow;
    [SerializeField]
    Transform minXPosition;
    [SerializeField]
    Transform maxXPosition;
    void Update()
    {
        if (objectToFollow.transform.position.x > minXPosition.position.x && objectToFollow.transform.position.x < maxXPosition.position.x)
            transform.position = new Vector3 (objectToFollow.transform.position.x, transform.position.y, transform.position.z);
    }
}
