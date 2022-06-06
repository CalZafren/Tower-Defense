using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    public GameObject gameManager;
    public GameObject wayPointParent;
    public Waypoints wayPoints;
    private GameManager g;

    // Start is called before the first frame update
    void Start()
    {
        //Grabs the Game Manager object instance
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        g = gameManager.GetComponent<GameManager>().instance;
        enemy = GetComponent<Enemy>();

        //Function to handle picking paths
        PickPath();
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.gameIsOver || GameManager.gameIsPaused){
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= .1f){
            GetNextWaypoint();
        }

    }

    void GetNextWaypoint(){

        if(wavepointIndex >= wayPoints.points.Length - 1){
            EndPath();
            
        }else{
        wavepointIndex++;
        target = wayPoints.points[wavepointIndex];
        }
    }


    //Handled functionality for if enemy makes it to the end
    void EndPath(){
        Destroy(gameObject);
        PlayerStats.lives--;
    }
    
    void PickPath(){
        if(g.firstLevel){
            //Picks path of the only waypoints in scene
            wayPointParent = GameObject.FindGameObjectWithTag("RedWayPoints");
        }else if(g.secondLevel){
            //Picks one of the two paths randomlly
            if(Random.value < .5){
                wayPointParent = GameObject.FindGameObjectWithTag("RedWayPoints");
            }else{
                wayPointParent = GameObject.FindGameObjectWithTag("BlueWayPoints");
            }
            //Picks one of the three paths randomlly
        }else if(g.thirdLevel){
            float temp = Random.value;
            if(temp < .33){
                wayPointParent = GameObject.FindGameObjectWithTag("RedWayPoints");
                Debug.Log("Test1");
            }else if (.33 < temp && temp < .66) {
                wayPointParent = GameObject.FindGameObjectWithTag("BlueWayPoints");
                Debug.Log("Test2");
            }else{
                wayPointParent = GameObject.FindGameObjectWithTag("GreenWayPoints");
                Debug.Log("Test3");
            }
            //Handles exceptions if something goes wrong
        }else{
            wayPointParent = GameObject.FindGameObjectWithTag("RedWayPoints");
        }

        wayPoints = wayPointParent.GetComponent<Waypoints>();
        target = wayPoints.points[0];
    }
}
