using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeleater : MonoBehaviour
{
    private GameObject[] chickens;
    private GameObject[] goldenChicken;
    private List<GameObject> chickensInBarnyard1 = new List<GameObject>();
    private List<GameObject> chickensInBarnyard2 = new List<GameObject>();
    private List<GameObject> chickensNotInBarnyard = new List<GameObject>();
    public GameObject Barnyard1;
    public GameObject Barnyard2;
    private float Barnyard1_min_x;
    private float Barnyard1_max_x;
    private float Barnyard2_min_x;
    private float Barnyard2_max_x;
    public bool foxInBarnyard1;
    public bool foxInBarnyard2;
    public bool foxInGame;
    public GameObject timer;
    private float timeLastEaten;
    // Start is called before the first frame update
    void Start()
    {
        timeLastEaten = 240;
        foxInBarnyard1 = false;
        foxInBarnyard2 = false;
        foxInGame = true;
        
        Barnyard1_min_x = Barnyard1.transform.position.x - Barnyard1.GetComponent<Barnyard>().xDepth;
        Barnyard1_max_x = Barnyard1.transform.position.x + Barnyard1.GetComponent<Barnyard>().xDepth;
        Barnyard2_min_x = Barnyard2.transform.position.x - Barnyard2.GetComponent<Barnyard>().xDepth;
        Barnyard2_max_x = Barnyard2.transform.position.x + Barnyard2.GetComponent<Barnyard>().xDepth;
    }

    // Update is called once per frame
    void Update()
    {
        if ((foxInGame)&&(timeLastEaten - 10 > timer.GetComponent<Timer>().timeRemaining))
        {
            chickens = GameObject.FindGameObjectsWithTag("Chicken");
            goldenChicken = GameObject.FindGameObjectsWithTag("GoldenChicken");
            if (foxInBarnyard1)
            {
                Debug.Log("Fox in Barnyard1");
                for (int i = 0; i < chickens.Length; i++)
                {
                    if (chickens[i].transform.position.x < Barnyard1_max_x)
                    {
                        chickensInBarnyard1.Add(chickens[i]);
                    }
                }
                if (goldenChicken != null)
                {
                    chickensInBarnyard1.Add(goldenChicken[0]);
                }
                if (chickensInBarnyard1.Count > 0)
                {
                    int randomChicken = Random.Range(0, chickensInBarnyard1.Count);
                    Destroy(chickensInBarnyard1[randomChicken]);
                }
                else
                {
                    Debug.Log("No chickens in Barnyard1");
                }
            }
            else if (foxInBarnyard2)
            {
                Debug.Log("Fox in Barnyard2");
                for (int i = 0; i < chickens.Length; i++)
                {
                    if (chickens[i].transform.position.x > Barnyard2_min_x)
                    {
                        chickensInBarnyard2.Add(chickens[i]);
                    }
                }
                if (goldenChicken != null)
                {
                    chickensInBarnyard1.Add(goldenChicken[0]);
                }
                if (chickensInBarnyard2.Count > 0)
                {
                    int randomChicken = Random.Range(0, chickensInBarnyard2.Count);
                    Destroy(chickensInBarnyard2[randomChicken]);
                }
                else
                {
                    Debug.Log("No chickens in Barnyard2");
                }

            }
            else if(GameObject.FindGameObjectsWithTag("Fox")!=null)
            {
                Debug.Log("Fox in area");
                for (int i = 0; i < chickens.Length; i++)
                {
                    if ((chickens[i].transform.position.x > Barnyard1_max_x) && (chickens[i].transform.position.x < Barnyard2_min_x))
                    {
                        chickensNotInBarnyard.Add(chickens[i]);
                    }
                }
                if (goldenChicken != null)
                {
                    chickensInBarnyard1.Add(goldenChicken[0]);
                }
                int randomChicken = Random.Range(0, chickensNotInBarnyard.Count);
                Destroy(chickensNotInBarnyard[randomChicken]);
            }
            timeLastEaten = timer.GetComponent<Timer>().timeRemaining;
        }
    }
}
