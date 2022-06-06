using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    public float startHealth;
    private float health;
    private float maxHealth;
    public float speed;
    public float maxSpeed;
    public int deathValue;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;
    public bool isBomb;
    public float explodeRadius;
    public GameObject explodeEffect;


    void Start(){
        maxSpeed = speed;
        //Handles setting and increasing health for the enemy
        IncreaseHealth();

    }
    
    //Handles functionality for enemy taking damage
    public void TakeDamage(float amount){
        health -= amount;
        healthBar.fillAmount = health/maxHealth;
        if(health <= 0){
            Die();
        }
    }

    //function to kill the enemy
    void Die(){
        if(isBomb == true){
            Bomb();
            GameObject bombEffect = (GameObject)Instantiate(explodeEffect, transform.position, Quaternion.identity);
        }
        PlayerStats.Money += deathValue;
        Destroy(gameObject);
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    void IncreaseHealth(){
        //Functionality for light enemies
        if(startHealth == 1){
            maxHealth = startHealth + (WaveSpawner.waveNum - 1)/4;
        //Functionality for normal enemies
        }else if(startHealth == 2){
            maxHealth = startHealth + (WaveSpawner.waveNum - 1)/2;
        //Functionality for heavy enemies
        }else{
            maxHealth = startHealth + (WaveSpawner.waveNum - 1) * (float)1.5;
        }
        //Sets the enemies health
        health = maxHealth;
        //Sets the death value
        deathValue = Mathf.FloorToInt(maxHealth);
    }

    //Handles bomb functionality for bomb enemies
    void Bomb(){
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach(GameObject turret in turrets){
            if(Vector3.Distance(transform.position, turret.transform.position) <= explodeRadius){
                Damage(turret);
            }
        }
    }

    void Damage(GameObject turret){
        Turret t = turret.GetComponent<Turret>();
        if(t != null){
            t.currentShots -= 50;
            Debug.Log("test");
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
