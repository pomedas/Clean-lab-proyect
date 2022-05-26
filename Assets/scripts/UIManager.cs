using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // 1

    public Text PointsP1;
    public Text PointsP2;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

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
