using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // 1

    [SerializeField] public MenuP1 P1;
    [SerializeField] public MenuP2 P2;
    float a;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        P1 = P1.GetComponent<MenuP1>();
        P2 = P2.GetComponent<MenuP2>();
        if (P1.is1Inside == true && P2.is2Inside == true){
            SceneManager.LoadScene("IntSysTemplate");
        }
    }

}
