using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public float timeBeforeFirstFox;
    public float timeBetweenSpawns;
    private float nextSpawnedFox;
    public GameObject foxPrefab;
    public GameObject goldenChickenPrefab;
    public GameObject timer;
    private float time;
    private bool firstFoxSpawned = false;
    private bool goldenChickenSpawned = false;
    public GameObject FoxEater;
    private int numChickensPlayArea;
    private GameObject[] chickens;
    private GameObject[] goldenChicken;
    private List<GameObject> chickensNotInBarnyard;
    public GameObject Barnyard1;
    public GameObject Barnyard2;
    private float Barnyard1_min_x;
    private float Barnyard1_max_x;
    private float Barnyard2_min_x;
    private float Barnyard2_max_x;
    // Start is called before the first frame update
    void Start()
    {
        time = timer.GetComponent<Timer>().timeRemaining;
        Barnyard1_min_x = Barnyard1.transform.position.x - Barnyard1.GetComponent<Barnyard>().xDepth;
        Barnyard1_max_x = Barnyard1.transform.position.x + Barnyard1.GetComponent<Barnyard>().xDepth;
        Barnyard2_min_x = Barnyard2.transform.position.x - Barnyard2.GetComponent<Barnyard>().xDepth;
        Barnyard2_max_x = Barnyard2.transform.position.x + Barnyard2.GetComponent<Barnyard>().xDepth;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.GetComponent<Timer>().timeRemaining < time - timeBeforeFirstFox)
        {
            if (firstFoxSpawned)
            {
                if ((timer.GetComponent<Timer>().timeRemaining < nextSpawnedFox) && (FoxEater.GetComponent<ChickenDeleater>().foxInGame == false))
                {
                    Debug.Log("Fox time");
                    SpawnFox();
                }
            }
            else
            {
                Debug.Log("First Fox");
                SpawnFox();
                firstFoxSpawned = true;
            }
        }

        NumChickensInPlayarea();
        if ((timer.GetComponent<Timer>().timeRemaining <= 0)|| (numChickensPlayArea == 0))
        {
            UIManager.Instance.GameOver();
        }
        if (((timer.GetComponent<Timer>().timeRemaining < 60) || (numChickensPlayArea == 4)) && (!goldenChickenSpawned))
        {
            TransformToGolden();
        }
    }

    private void SpawnFox()
    {
        Debug.Log("Spawning fox...");
        Vector3 pos = new Vector3(50, 0.5f, 100f);
        GameObject fox = Instantiate(foxPrefab, pos, foxPrefab.transform.rotation);
        nextSpawnedFox = timer.GetComponent<Timer>().timeRemaining - timeBetweenSpawns;
        FoxEater.GetComponent<ChickenDeleater>().foxInGame = true;
    }

    private void TransformToGolden()
    {
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
        chickensNotInBarnyard = new List<GameObject>();
        for (int i = 0; i < chickens.Length; i++)
        {
            if ((chickens[i].transform.position.x > Barnyard1_max_x) && (chickens[i].transform.position.x < Barnyard2_min_x))
            {
                chickensNotInBarnyard.Add(chickens[i]);
            }
        }
        int randomChicken = Random.Range(0, chickensNotInBarnyard.Count);
        Debug.Log("Spawning fox...");
        Vector3 pos = chickensNotInBarnyard[randomChicken].transform.position;
        GameObject goldenChicken = Instantiate(goldenChickenPrefab, pos, goldenChickenPrefab.transform.rotation);
        goldenChicken.transform.localScale = new Vector3(1, 1, 1);
        goldenChickenSpawned = true;
        Destroy(chickensNotInBarnyard[randomChicken]);
    }

    private void NumChickensInPlayarea()
    {
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
        chickensNotInBarnyard = new List<GameObject>();
        goldenChicken = GameObject.FindGameObjectsWithTag("GoldenChicken");
        for (int i = 0; i < chickens.Length; i++)
        {
            if ((chickens[i].transform.position.x > Barnyard1_max_x) && (chickens[i].transform.position.x < Barnyard2_min_x))
            {
                chickensNotInBarnyard.Add(chickens[i]);
            }
        }
        if ((goldenChicken.Length != 0) && ((goldenChicken[0].transform.position.x > Barnyard1_max_x) && (goldenChicken[0].transform.position.x < Barnyard2_min_x)))
        {
            chickensNotInBarnyard.Add(goldenChicken[0]);
        }
        numChickensPlayArea = chickensNotInBarnyard.Count;
    }
         



}
