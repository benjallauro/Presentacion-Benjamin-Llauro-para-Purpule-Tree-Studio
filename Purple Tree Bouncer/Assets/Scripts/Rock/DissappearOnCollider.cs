using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissappearOnCollider : MonoBehaviour
{
    [SerializeField]
    float timeBeforeDissapear;
    GameTime timer;
    bool readyToDissappear;
    Transform myTransform;
    Vector3 startPosition;
    Quaternion startRotation;
    Bounce bounce;
    GameplayManager gameplayManger;
    void Start()
    {
        bounce = GetComponent<Bounce>();
        timer = new GameTime();
        timer.StopAndReset();
        timer.SetTimer(timeBeforeDissapear);
        readyToDissappear = false;
        myTransform = GetComponent<Transform>();
        startPosition = myTransform.transform.position;
        startRotation = transform.rotation;
        gameplayManger = GameplayManager.Instance;
    }
    public void CancelDissappear()
    {
        readyToDissappear = false;
        if(timer != null)
            timer.StopAndReset();
    }
    void Dissappear()
    {
        readyToDissappear = true;
        timer.Start();
        bounce.Restart();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DissappearArea" && !readyToDissappear)
        {
            Dissappear();
        }
        if (collider.gameObject.tag == "FinalTarget" && !readyToDissappear)
        {
            Debug.Log("Score!");
            Dissappear();
            gameplayManger.AddBasketedRock();
        }
    }
    void Update()
    {
        if(timer.Update(Time.deltaTime))
        {
            timer.StopAndReset();
            readyToDissappear = false;
            myTransform.position = startPosition;
            transform.rotation = startRotation;
            bounce.SetBouncing(false);
            this.gameObject.SetActive(false);
        }
    }
}
