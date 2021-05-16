using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpriteRotator : MonoBehaviour
{
    PlayerMovement playerMovement;
    bool spriteLookingLeft;
    void Start()
    {
        spriteLookingLeft = false;
        playerMovement = GetComponentInParent<PlayerMovement>();
        if (playerMovement == null)
            Debug.Log("There is no PlayerMovement script attached to this object's parent");
    }

    void Update()
    {
        if ((playerMovement.GetLookingLeft() && !spriteLookingLeft) || (!playerMovement.GetLookingLeft() && spriteLookingLeft))
        {
            transform.Rotate(0, 180, 0);
            if (spriteLookingLeft)
                spriteLookingLeft = false;
            else
                spriteLookingLeft = true;
        }
    }
}
