using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiopan : MonoBehaviour
{
    AudioSource audioplayer;

    void Start() {
        audioplayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioplayer.panStereo = -transform.localPosition.z / 50;
    }
}
