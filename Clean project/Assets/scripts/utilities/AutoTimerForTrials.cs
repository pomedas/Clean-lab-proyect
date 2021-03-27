using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTimerForTrials : MonoBehaviour
{
    Image sp;
    public float timeOnTrials;
    bool flag;

    public bool activeTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        if (activeTimer)
        {
            flag = false;
            sp = GetComponent<Image>();
            sp.color = Color.clear;
            transform.GetChild(0).GetComponent<Text>().color = Color.clear;
            StartCoroutine(waittoendtrial());
        }
        else {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            Color c = Color.Lerp(sp.color, Color.white, Time.deltaTime);
            sp.color = c;
            transform.GetChild(0).GetComponent<Text>().color = c;
        }
    }

    IEnumerator waittoendtrial() {
        yield return new WaitForSeconds(timeOnTrials);
        flag = true;
    }
}
