using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    public GameObject fish1;
    public int fishThreshold = 50;
    private int population = 0;
    private int reproductionRate = 5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateFish", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GenerateFish() {

        if (population < fishThreshold)
        {  //then generate more fish 
            population += reproductionRate;
            GenerateObject(fish1, reproductionRate);
        }
        else {
            //create more garbage/sharks to kill fish
            Debug.Log("population reached threshold");
        }
            

    }

    //spawns a given gameobject as many times as you want inside game-area
    void GenerateObject(GameObject go, int amount)
    {

        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject tmp = Instantiate(go);
            tmp.gameObject.transform.position = new Vector3(Random.Range(0, 100), -1, Random.Range(0, 100));
            tmp.transform.parent = transform;
        }

    }
}
