using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    private GameObject fox;
    private GameObject player1;
    private GameObject player2;
    private GameObject[] chickens;

    public float speed_chicken;
    float minx = 25;
    float maxx = 75;
    float minz = 0;
    float maxz = 100;

    // Params
    float k_border = 10 * 10;
    float k_fox = 40 * 40;
    float k_player = 40 * 40;
    float k_chicken = 10 * 10;

    private void Start()
    {
        speed_chicken = 10;
        fox = GameObject.FindGameObjectWithTag("Fox");
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
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
            float d;
            float vx = 0;
            float vz = 0;

            // Border repulsion
            float dx2 = (x - minx) * (x - minx);
            float dX2 = (x - maxx) * (x - maxx);
            float dz2 = (z - minz) * (z - minz);
            float dZ2 = (z - maxz) * (z - maxz);
            if (dx2 != 0 && dX2 != 0 && dz2 != 0 && dZ2 != 0)
            {
                vx += k_border / dx2 - 1 / dX2;
                vz += k_border / dz2 - 1 / dZ2;
            }

            // Fox repulsion
            if (fox != null){
                float foxx = fox.transform.position.x;
                float foxz = fox.transform.position.z;
                d = Mathf.Sqrt((x - foxx) * (x - foxx) + (z - foxz) * (z - foxz));
                if (d != 0f)
                {
                    vx += k_fox * (x - foxx) / (d * d * d);
                    vz += k_fox * (z - foxz) / (d * d * d);
                }
            }
            
            // Player1 repulsion
            float player1x = player1.transform.position.x;
            float player1z = player1.transform.position.z;
            d = Mathf.Sqrt((x - player1x) * (x - player1x) + (z - player1z) * (z - player1z));
            if (d != 0)
            {
                vx += k_player * (x - player1x) / (d * d * d);
                vz += k_player * (z - player1z) / (d * d * d);
            }

            // Player2 repulsion
            float player2x = player2.transform.position.x;
            float player2z = player2.transform.position.z;
            d = Mathf.Sqrt((x - player2x) * (x - player2x) + (z - player2z) * (z - player2z));
            if (d != 0)
            {
                vx += k_player * (x - player2x) / (d * d * d);
                vz += k_player * (z - player2z) / (d * d * d);
            }

            // Chickens repulsion
            for (int i = 0; i < chickens.Length; i++)
            {
                GameObject myChicken = chickens[i];
                if (myChicken != this && is_free(myChicken))
                {
                    float chickenx = myChicken.transform.position.x;
                    float chickenz = myChicken.transform.position.z;
                    d = Mathf.Sqrt((x - chickenx) * (x - chickenx) + (z - chickenz) * (z - chickenz));
                    if (d != 0)
                    {
                        vx += k_chicken * (x - chickenx) / (d * d * d);
                        vz += k_chicken * (z - chickenz) / (d * d * d);
                    }
                }
            }

            // Update v, to clamp it (max velocity), amd update the position.
            Vector3 v = new Vector3(vx, 0, vz);
            if (Mathf.Sqrt(vx * vx + vz * vz) > speed_chicken)
            {
                v *= speed_chicken / Mathf.Sqrt(vx * vx + vz * vz);
            }
            transform.position += v * Time.deltaTime;
        }
    }
}
