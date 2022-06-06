using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool gameIsOver;
    public static bool gameIsPaused;
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject gameWonUI;
    public bool firstLevel;
    public bool secondLevel;
    public bool thirdLevel;
    public GameManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameIsOver = false; 
        gameIsPaused = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsOver){
            return;
        }

        if(Input.GetKeyDown("e") && gameIsPaused == false){
            EndGame();
        }
        
        /*
        if(Input.GetKeyDown("p") && gameIsPaused == false){
            WinGame();
        }
        */

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!gameIsPaused){
                PauseGame();
            }else{
                UnPauseGame();
            }
        }


        if(PlayerStats.lives <= 0){
            EndGame();
        }
    }

    public void WinGame(){
        gameIsOver = true;
        gameWonUI.SetActive(true);
    }

    void EndGame(){
        gameIsOver = true;
        gameOverUI.SetActive(true);

    }

    public void PauseGame(){
        if(gameIsOver == false){
        gameIsPaused = true;
        pauseUI.SetActive(true);
        }
    }

    public void UnPauseGame(){
        gameIsPaused = false;
        pauseUI.SetActive(false);
    }
}
