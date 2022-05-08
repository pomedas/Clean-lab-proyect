using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermov1 : MonoBehaviour
{
    public GameObject circle_chicken;
    public GameObject chicken;
    private bool chicken_caught;
    // Start is called before the first frame update
    void Start()
    {
        circle_chicken.SetActive(false);
        chicken_caught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x > 80)&&(chicken_caught))
        {
            chicken.SetActive(true);
            circle_chicken.SetActive(false);
            if (chicken_caught == true) {
                chicken.transform.position = transform.position;

            }
            chicken_caught = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chicken") && (transform.position.x <= 80))
        {
            Debug.Log("collisionnnn");
            other.gameObject.SetActive(false);
            circle_chicken.SetActive(true);
            chicken_caught = true;
        }
    }

}
