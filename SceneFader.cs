using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{

    public Image image;
    

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeTo(string scene){
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn(){
        float t = 1f;
        while(t > 0){
            t -= Time.deltaTime;
            image.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene){
        float t = 0f;
        while(t < 1f){
            t += Time.deltaTime;
            image.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }
}
