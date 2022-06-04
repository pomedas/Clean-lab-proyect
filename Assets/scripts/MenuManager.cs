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
    public Animator transition;
    public AudioSource music;
    public float waitTime = 1f;
    public float fadeTime = 1f;
    
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
            StartCoroutine(LoadLevel("IntSysTemplate", music));
        }
    }

    IEnumerator LoadLevel(string level, AudioSource audioSource){

        transition.SetTrigger("Start");

        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(level);

    }

}
