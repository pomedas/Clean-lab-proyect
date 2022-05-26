using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // 1

    public Text PointsP1;
    public Text PointsP2;
    public Text timer_1;
    public Text timer_2;
    public GameObject timer;
    private float timeRemaining;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        float timeRemaining = timer.GetComponent<Timer>().timeRemaining;
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timer_1.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer_2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    // Update is called once per frame
    public void UpdatePointsP1() // 1
    {
        PointsP1.text = GameStateManager.Instance.pointsP1.ToString();
    }
    public void UpdatePointsP2() // 1
    {
        PointsP2.text = GameStateManager.Instance.pointsP2.ToString();
    }

}
