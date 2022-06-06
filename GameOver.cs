using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Text waveText;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    private string nextLevel;
    private string currentLevel;


    // Start is called before the first frame update
    void OnEnable()
    {
        waveText.text = WaveSpawner.waveNum.ToString();
        switch(SceneManager.GetActiveScene().buildIndex){
            case 2:
                nextLevel = "Level02";
                Debug.Log("Level02");
                break;
            case 3:
                nextLevel = "Level03";
                Debug.Log("Level03");
                break;
            default:
                nextLevel = "MainMenu";
                Debug.Log("Mainmenu");
                break;
       }
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry(){
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu(){
        sceneFader.FadeTo(menuSceneName);
    }

    public void NextLevel(){
        sceneFader.FadeTo(nextLevel);
    }
}
