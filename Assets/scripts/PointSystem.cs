using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{

    public static PointSystem Instance; // 1

    [HideInInspector]
    public int pointsP1=0;
    [HideInInspector]
    public int pointsP2=0;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add1PointP1()
    {
        pointsP1++;
        UIManager.Instance.UpdatePointsP1();

    }

    public void Add3PointP1()
    {
        pointsP1+=3;
        UIManager.Instance.UpdatePointsP1();

    }

    public void Add1PointP2()
    {
        pointsP2++;
        UIManager.Instance.UpdatePointsP2();

    }

    public void Add3PointP2()
    {
        pointsP2+=3;
        UIManager.Instance.UpdatePointsP2();

    }

    public void ErasePointP1()
    {
        pointsP1--;
        UIManager.Instance.UpdatePointsP1();
    }

    public void ErasePointP2()
    {
        pointsP2--;
        UIManager.Instance.UpdatePointsP2();
    }

    public void Erase3PointsP1()
    {
        pointsP1-=3;
        UIManager.Instance.UpdatePointsP1();
    }

    public void Erase3PointsP2()
    {
        pointsP2-=3;
        UIManager.Instance.UpdatePointsP2();
    }
}
