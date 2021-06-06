using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Harbor : MonoBehaviour
{
    private bool redBoatInHarbor = false;
    private bool blueBoatInHarbor = false;

    public float timeThreshold = 2f;
    public UnityEvent OnBoatsInHarbor;


    // Start is called before the first frame update
    void Start()
    {
        if (OnBoatsInHarbor == null)
        {
            OnBoatsInHarbor = new UnityEvent();
        }    
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Boat")
        {
            redBoatInHarbor = true;
            if (blueBoatInHarbor)
            {
                StartCoroutine(EnterHarborCoroutine());
            }
        }
        if (other.gameObject.name == "Blue Boat")
        {
            blueBoatInHarbor = true;
            if (redBoatInHarbor)
            {
                StartCoroutine(EnterHarborCoroutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Red Boat")
        {
            redBoatInHarbor = false;
        }
        if (other.gameObject.name == "Blue Boat")
        {
            blueBoatInHarbor = false;
        }
    }

    IEnumerator EnterHarborCoroutine()
    {
        yield return new WaitForSeconds(timeThreshold);
        if (redBoatInHarbor && blueBoatInHarbor)
        {
            OnBoatsInHarbor.Invoke();
        }
    }
}
