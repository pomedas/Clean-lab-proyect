using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public Transform red_boat;
    public Transform blue_boat;

    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, red_boat.position);
        line.SetPosition(1, blue_boat.position);
    }
}
