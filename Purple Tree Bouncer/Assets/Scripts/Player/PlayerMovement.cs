using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D heroRigidBody;
    [SerializeField]
    Animator animator;
    Vector2 force;
    [SerializeField]
    float moveSpeed;
    bool lookingLeft;
    [SerializeField]
    ParticleSystem particleEmmitor;
    float horizontalInput;
    public void Awake()
    {
        heroRigidBody = GetComponent<Rigidbody2D>();
        if (animator == null)
            Debug.Log("There is no Animator attached to this object's children");
        force = new Vector2(0, 0);
        lookingLeft = false;
    }
    private void MovementInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        horizontalInput = Input.GetAxis("Horizontal");
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
                horizontalInput = 1;
            else if (touch.position.x < Screen.width / 2)
                horizontalInput = -1;
        }
        else
            horizontalInput = 0;
        #endif
        float horizontalForce = horizontalInput * moveSpeed * Time.deltaTime;
        force = new Vector2(horizontalForce, 0);
        heroRigidBody.AddForce(force);

        if (horizontalInput > 0)
        {
            lookingLeft = false;
            animator.SetBool("walking", true);
            if (particleEmmitor != null)
                particleEmmitor.Play();

        }
        else if (horizontalInput < 0)
        {
            lookingLeft = true;
            animator.SetBool("walking", true);
            if (particleEmmitor != null)
                particleEmmitor.Play();
            Debug.Log(horizontalInput);
        }
        else
        {
            animator.SetBool("walking", false);
            if (particleEmmitor != null)
                particleEmmitor.Stop();
        }
    }
    public bool GetLookingLeft() { return lookingLeft; }
    private void FixedUpdate()
    {
        if (heroRigidBody != null)
        {
            MovementInput();
        }
        else
            Debug.Log("Rigidbody is null");
    }
}
