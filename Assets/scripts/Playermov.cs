using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermov : MonoBehaviour
{
    public GameObject circle_chicken;
    private GameObject chicken;
    public GameObject circle_fox;
    public GameObject circle_battle_fox;
    private GameObject fox;
    private bool chicken_caught;
    private bool fox_caught;
    // Start is called before the first frame update
    void Start()
    {
        circle_chicken.SetActive(false);
        chicken_caught = false;
        circle_fox.SetActive(false);
        circle_battle_fox.SetActive(false);
        fox_caught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 20)
        {
            if (chicken_caught == true)
            { 
                chicken.SetActive(true);
                circle_chicken.SetActive(false);
                chicken.transform.position = transform.position;
                chicken_caught = false;
            }
            
        }

        if ((fox_caught == true)&&(transform.position.x>80))
        {
            fox.SetActive(true);
            circle_fox.SetActive(false);
            fox.transform.position = transform.position;
            fox_caught = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((transform.position.x > 25)&& (transform.position.x < 75))
        {
            if (other.gameObject.CompareTag("Chicken")){
                Debug.Log("collisionnnn with chicken");
                chicken = other.gameObject;
                other.gameObject.SetActive(false);
                circle_chicken.SetActive(true);
                chicken_caught = true;
            }
            if (other.gameObject.CompareTag("Fox")){
                Debug.Log("collisionnnn with fox");
                fox = other.gameObject;
                other.gameObject.SetActive(false);
                circle_fox.SetActive(true);
                fox_caught = true;
            }
        }
        else if (transform.position.x < 20)
        {
            if (other.gameObject.CompareTag("Fox")){
                Debug.Log("collisionnnn with fox");
                other.gameObject.SetActive(false);
                circle_battle_fox.SetActive(true);
            }
        }
    }

}
