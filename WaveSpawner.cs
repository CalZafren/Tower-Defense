using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform enemyHeavyPrefab;
    public Transform enemyLightPrefab;
    public Transform enemyBombPrefab;
    public float TimeBetweenWaves;
    private float countdown = 5f;
    public static int waveNum;
    public Transform spawnPoint;
    public Text CountdownText;
    public Text WaveText;
    public Text completedText;
    private Wave[] Waves = new Wave[25];
    public GameManager gameManager;




    // Start is called before the first frame update
    void Start()
    {
        waveNum = 0;
        UpdateWaveText(); 
        FillWaveArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameIsOver || GameManager.gameIsPaused){
            return;
        }

        //On the last wave, set the wave timer to an amount that will allow that wave to finish
        if(waveNum == 25){
            CountdownText.enabled = false;
            completedText.enabled = true;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length == 0){
                gameManager.WinGame();
            }
        }

        if(countdown <= 0){
            StartCoroutine(spawnWave());
            StartCoroutine(spawnLightWave());
            StartCoroutine(spawnHeavyWave());
            StartCoroutine(spawnBombWave());
            countdown = TimeBetweenWaves;
            UpdateWaveText();
        }

        if(waveNum != 25){
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            CountdownText.text = string.Format("{0:00.00}", countdown);
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator spawnWave(){
        waveNum++;

        //Starts a for loop spawning in all of the normal enemies
        for(int i = 0; i < Waves[waveNum-1].normal; i++){   
            spawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(.4f);
        }
    }

    IEnumerator spawnLightWave(){
        yield return new WaitForSeconds(6f);
        for(int j = 0; j < Waves[waveNum-1].light; j++){
            spawnEnemy(enemyLightPrefab);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator spawnHeavyWave(){
        yield return new WaitForSeconds(4f);
        for(int k = 0; k < Waves[waveNum-1].heavy; k++){
            spawnEnemy(enemyHeavyPrefab);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator spawnBombWave(){
        yield return new WaitForSeconds(10f);
        for(int l = 0; l < Waves[waveNum - 1].bomb; l++){
            spawnEnemy(enemyBombPrefab);
            yield return new WaitForSeconds(5f);
        }
    }

    void spawnEnemy(Transform enemy){
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public void UpdateWaveText(){
        WaveText.text = "Wave:" + waveNum.ToString();
    }

    void FillWaveArray(){

        if(gameManager.firstLevel){
            Waves[0] =  new Wave(0, 15, 0, 0);
            Waves[1] =  new Wave(0, 20, 0, 0);
            Waves[2] =  new Wave(2, 20, 0, 0);
            Waves[3] =  new Wave(4, 25, 1, 0);
            Waves[4] =  new Wave(5, 30, 1, 0);
            Waves[5] =  new Wave(7, 35, 2, 0);
            Waves[6] =  new Wave(10, 45, 3, 0);
            Waves[7] =  new Wave(15, 55, 5, 0);
            Waves[8] =  new Wave(20, 65, 7, 0);
            Waves[9] =  new Wave(30, 75, 10, 1);
            Waves[10] = new Wave(40, 75, 15, 1);
            Waves[11] = new Wave(40, 80, 15, 1);
            Waves[12] = new Wave(41, 85, 17, 2);
            Waves[13] = new Wave(42, 90, 19, 2);
            Waves[14] = new Wave(43, 100, 23, 2);
            Waves[15] = new Wave(44, 115, 25, 3);
            Waves[16] = new Wave(50, 125, 27, 3);
            Waves[17] = new Wave(55, 150, 30, 3);
            Waves[18] = new Wave(60, 175, 35, 3);
            Waves[19] = new Wave(65, 190, 37, 4);
            Waves[20] = new Wave(70, 200, 40, 4);
            Waves[21] = new Wave(80, 210, 42, 4);
            Waves[22] = new Wave(90, 225, 45, 4);
            Waves[23] = new Wave(95, 245, 47, 5);
            Waves[24] = new Wave(100, 250, 50, 5);
        }else if(gameManager.secondLevel){
            Waves[0] =  new Wave(0, 15, 0, 0);
            Waves[1] =  new Wave(0, 20, 0, 0);
            Waves[2] =  new Wave(2, 20, 0, 0);
            Waves[3] =  new Wave(4, 25, 1, 0);
            Waves[4] =  new Wave(5, 30, 1, 0);
            Waves[5] =  new Wave(7, 35, 2, 0);
            Waves[6] =  new Wave(10, 45, 3, 0);
            Waves[7] =  new Wave(15, 55, 5, 0);
            Waves[8] =  new Wave(20, 65, 7, 0);
            Waves[9] =  new Wave(30, 75, 10, 1);
            Waves[10] = new Wave(40, 75, 15, 1);
            Waves[11] = new Wave(40, 80, 15, 1);
            Waves[12] = new Wave(41, 85, 17, 2);
            Waves[13] = new Wave(42, 90, 19, 2);
            Waves[14] = new Wave(43, 100, 23, 2);
            Waves[15] = new Wave(44, 115, 25, 3);
            Waves[16] = new Wave(50, 125, 27, 3);
            Waves[17] = new Wave(55, 150, 30, 3);
            Waves[18] = new Wave(60, 175, 35, 3);
            Waves[19] = new Wave(65, 190, 37, 4);
            Waves[20] = new Wave(70, 200, 40, 4);
            Waves[21] = new Wave(80, 210, 42, 4);
            Waves[22] = new Wave(90, 225, 45, 4);
            Waves[23] = new Wave(95, 245, 47, 5);
            Waves[24] = new Wave(100, 250, 50, 5);
        }else{
            Waves[0] =  new Wave(0, 15, 0, 0);
            Waves[1] =  new Wave(0, 20, 0, 0);
            Waves[2] =  new Wave(2, 20, 0, 0);
            Waves[3] =  new Wave(4, 25, 1, 0);
            Waves[4] =  new Wave(5, 30, 1, 0);
            Waves[5] =  new Wave(7, 35, 2, 0);
            Waves[6] =  new Wave(10, 45, 3, 0);
            Waves[7] =  new Wave(15, 55, 5, 0);
            Waves[8] =  new Wave(20, 65, 7, 0);
            Waves[9] =  new Wave(30, 75, 10, 1);
            Waves[10] = new Wave(40, 75, 15, 1);
            Waves[11] = new Wave(40, 80, 15, 1);
            Waves[12] = new Wave(41, 85, 17, 2);
            Waves[13] = new Wave(42, 90, 19, 2);
            Waves[14] = new Wave(43, 100, 23, 2);
            Waves[15] = new Wave(44, 115, 25, 3);
            Waves[16] = new Wave(50, 125, 27, 3);
            Waves[17] = new Wave(55, 150, 30, 3);
            Waves[18] = new Wave(60, 175, 35, 3);
            Waves[19] = new Wave(65, 190, 37, 4);
            Waves[20] = new Wave(70, 200, 40, 4);
            Waves[21] = new Wave(80, 210, 42, 4);
            Waves[22] = new Wave(90, 225, 45, 4);
            Waves[23] = new Wave(95, 245, 47, 5);
            Waves[24] = new Wave(100, 250, 50, 5);
        }
    }

    public void SendNextWave(){
        //Sets countdown to zero and gives the player money
        PlayerStats.Money += (int)countdown*2;
        countdown = 0;
    }
}
