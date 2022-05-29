using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private GameObject[] chickens;

    public float speed_fox;
    float minx = 30;
    float maxx = 70;
    float minz = 5;
    float maxz = 95;

    // Params

    private void Start()
    {
        speed_fox = 15;
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
    }

    bool im_free()
    {
        return (transform.position.x > minx) && (transform.position.x < maxx);
    }

    bool is_free(GameObject go)
    {
        return (go.transform.position.x > minx) && (go.transform.position.x < maxx);
    }

    private void Update()
    {
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
        if (im_free())
        {
            float x = transform.position.x;
            float z = transform.position.z;

            float vx = 0;
            float vz = 0;

            GameObject best_chicken = null;
            float best_distance = 1000;
            for (int i = 0; i < chickens.Length; i++)
            {
                GameObject myChicken = chickens[i];
                if (is_free(myChicken))
                {
                    float chickenx = myChicken.transform.position.x;
                    float chickenz = myChicken.transform.position.z;
                    float d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                    if (d < best_distance)
                    {
                        best_chicken = myChicken;
                        best_distance = d;
                    }
                }
            }
            float dx = best_chicken.transform.position.x - transform.position.x;
            float dz = best_chicken.transform.position.z - transform.position.z;

            Vector3 v = new Vector3(dx, 0, dz);
            v *= speed_fox / Mathf.Sqrt(dx * dx + dz * dz);
            Debug.Log(v);
            transform.position += v * Time.deltaTime;
        }
    }
}
