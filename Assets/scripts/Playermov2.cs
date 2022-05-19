using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermov2 : MonoBehaviour
{
    public GameObject itsBarnyard;
    public GameObject oponentBarnyard;
    public GameObject circle_chicken;
    private GameObject chicken;
    public GameObject circle_goldchicken;
    private GameObject goldchicken;
    public GameObject circle_fox;
    public GameObject circle_battle_fox;
    private GameObject fox;
    private bool chicken_caught;
    private bool goldchicken_caught;
    private bool fox_caught;

    private float itsBarnyard_min_x;
    private float itsBarnyard_max_x;
    private float itsBarnyard_min_z;
    private float itsBarnyard_max_z;
    private float oponentBarnyard_min_z;
    private float oponentBarnyard_max_z;
    private float oponentBarnyard_min_x;
    private float oponentBarnyard_max_x;
    // Start is called before the first frame update
    void Start()
    {
        circle_chicken.SetActive(false);
        chicken_caught = false;
        circle_fox.SetActive(false);
        circle_battle_fox.SetActive(false);
        fox_caught = false;
        circle_goldchicken.SetActive(false);
        goldchicken_caught = false;
        itsBarnyard_min_x = itsBarnyard.transform.position.x - itsBarnyard.GetComponent<Barnyard>().xDepth;
        itsBarnyard_max_x = itsBarnyard.transform.position.x + itsBarnyard.GetComponent<Barnyard>().xDepth;
        itsBarnyard_min_z = itsBarnyard.transform.position.z - itsBarnyard.GetComponent<Barnyard>().zDepth;
        itsBarnyard_max_z = itsBarnyard.transform.position.z + itsBarnyard.GetComponent<Barnyard>().zDepth;
        oponentBarnyard_min_x = oponentBarnyard.transform.position.x - oponentBarnyard.GetComponent<Barnyard>().xDepth;
        oponentBarnyard_max_x = oponentBarnyard.transform.position.x + oponentBarnyard.GetComponent<Barnyard>().xDepth;
        oponentBarnyard_min_z = oponentBarnyard.transform.position.z - oponentBarnyard.GetComponent<Barnyard>().zDepth;
        oponentBarnyard_max_z = oponentBarnyard.transform.position.z + oponentBarnyard.GetComponent<Barnyard>().zDepth;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > itsBarnyard_min_x)
        {
            //Release chicken
            if (chicken_caught == true)
            {
                chicken.SetActive(true);
                circle_chicken.SetActive(false);
                Vector3 randomPosition = new Vector3(Random.Range(itsBarnyard_min_x, itsBarnyard_max_x), 0.5f, Random.Range(itsBarnyard_min_z, itsBarnyard_max_z));
                chicken.transform.position = randomPosition;
                chicken_caught = false;
                GameStateManager.Instance.Add1PointP2();
            }

            //Release gold chicken
            if (goldchicken_caught == true)
            {
                goldchicken.SetActive(true);
                circle_goldchicken.SetActive(false);
                Vector3 randomPosition = new Vector3(Random.Range(itsBarnyard_min_x, itsBarnyard_max_x), 0.5f, Random.Range(itsBarnyard_min_z, itsBarnyard_max_z));
                goldchicken.transform.position = randomPosition;
                goldchicken_caught = false;
                GameStateManager.Instance.Add3PointP2();
            }

        }

        if ((fox_caught == true) && (transform.position.x < oponentBarnyard_max_x))
        {
            fox.SetActive(true);
            circle_fox.SetActive(false);
            Vector3 randomPosition = new Vector3(Random.Range(oponentBarnyard_min_x, oponentBarnyard_max_x), 0.5f, Random.Range(oponentBarnyard_min_z, oponentBarnyard_max_z));
            fox.transform.position = randomPosition;
            fox_caught = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((transform.position.x < itsBarnyard_min_x-5) && (transform.position.x > oponentBarnyard_max_x+5))
        {
            if ((other.gameObject.CompareTag("Chicken"))&& (chicken_caught == false) && (fox_caught == false))
            {
                Debug.Log("collisionnnn with chicken");
                chicken = other.gameObject;
                other.gameObject.SetActive(false);
                circle_chicken.SetActive(true);
                chicken_caught = true;
            }
            if ((other.gameObject.CompareTag("GoldenChicken")) && (goldchicken_caught == false) && (fox_caught == false))
            {
                Debug.Log("collisionnnn with goldenchicken");
                goldchicken = other.gameObject;
                other.gameObject.SetActive(false);
                circle_goldchicken.SetActive(true);
                goldchicken_caught = true;
            }
            if ((other.gameObject.CompareTag("Fox"))&& (chicken_caught == false) && (fox_caught == false))
            {
                Debug.Log("collisionnnn with fox");
                fox = other.gameObject;
                other.gameObject.SetActive(false);
                circle_fox.SetActive(true);
                fox_caught = true;
            }
        }
        else if (transform.position.x > itsBarnyard_min_x)
        {
            if (other.gameObject.CompareTag("Fox"))
            {
                Debug.Log("Battling fox");
                other.gameObject.SetActive(false);
                circle_battle_fox.SetActive(true);
            }
        }
    }

}
