using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour

{
    [Header("General")]
    public Transform target;
    public float range = 15f;
    private Enemy e;

    [Header("Use Bullets (default")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    [HideInInspector]
    public float fireCountDown = 0f;

    [Header("Use Laser")]
    public bool useLaser;
    public LineRenderer LineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float laserDPS;
    private bool alreadySlowed = false;
    private float laserFireRate = 1f;
    private float laserCountDown = 0f;
    public float slowPercent;

    [Header("Missile Launcher")]
    public bool isLauncher;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    [Header("Break System")]
    public Image breakImage;
    public Image healthBar;
    public float maxShots;
    //[HideInInspector]
    public float currentShots; 
    [HideInInspector]
    public bool isBroken = false;   

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        currentShots = maxShots;
        breakImage.enabled = false;
        if(useLaser == true){
            impactEffect.Stop();
            alreadySlowed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.gameIsOver || GameManager.gameIsPaused){
            return;
        }

        //Handles updating the healthbar
        healthBar.fillAmount = currentShots/maxShots;

        if(currentShots <= 0){
            BreakTurret();
        }

        //Only run turret behavior if it is not broken
        if(isBroken == false){
            //Handles Behavior for Launchers
            if(isLauncher){
                UpdateTarget();
            }
            if(target == null || Vector3.Distance(transform.position, target.position) > range){
                //Removes line renderer
                if(useLaser){
                    if(LineRenderer.enabled){
                        LineRenderer.enabled = false;
                        impactEffect.Stop();
                        impactLight.enabled = false;
                    }
                    alreadySlowed = false;
                }
                UpdateTarget();
            }else{
                LockOnTarget();
                //Handle shooting
                if(useLaser){
                    Laser();
                }else{
                    //Shooting countdown
                    if(fireCountDown <= 0f){
                        Shoot();
                        fireCountDown = 1/fireRate;
                    }
                    fireCountDown -= Time.deltaTime;
                }
            }
        }else{
            if(useLaser){
                    if(LineRenderer.enabled){
                        LineRenderer.enabled = false;
                        impactEffect.Stop();
                        impactLight.enabled = false;
                    }
                    alreadySlowed = false;
                }
        }
    }

    //Handles Updating Target for Turrets
    void UpdateTarget(){
            //Creates the array of all objects in scene with enemy tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            //makes the shortest distance infinity
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach(GameObject enemy in enemies){
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                //Checks the distance to each enemy, and finds the one with the shortest distance from turret
                if(distanceToEnemy < shortestDistance){
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            if(nearestEnemy != null && shortestDistance <= range){
                target = nearestEnemy.transform;
                e = target.GetComponent<Enemy>();
            }else{
                target = null;
            }
    }

    void LockOnTarget(){
        //Rotates the turret
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    //Handles the Range Sphere for the Turrets
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //Handles Shooting
    void Shoot(){
        currentShots--;
        //Instantiates the bullet
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Grabs the bullet and stores it in a variable
        bullet bullet = bulletGo.GetComponent<bullet>();
        //Uses the bullets seek method to pass the target variable to the bullet
        if(bullet != null){
            bullet.Seek(target);
            bullet.GetParent(this);
        }
    }

    //Handles shooting Laser
    void Laser(){
        if(!LineRenderer.enabled){
            LineRenderer.enabled = true;
            impactLight.enabled = true;
            impactEffect.Play();
        }
        if(target != null){
            //Setting the Laser
            LineRenderer.SetPosition(0, firePoint.position);
            LineRenderer.SetPosition(1, target.position);
            //Setting the impact effect
            Vector3 dir = firePoint.position - target.position;
            impactEffect.transform.rotation = Quaternion.LookRotation(dir);
            impactEffect.transform.position = target.position + dir.normalized;
            //Handles Damaging and Slowing Enemies
            LaserHitEnemy();
        }
    }

    void LaserHitEnemy(){
        float maxSpeed = e.speed;
        float newSpeed;

        //Handles slowing enemies
        if(alreadySlowed == false){
            newSpeed = maxSpeed * (1-slowPercent);
            e.speed = newSpeed;
            alreadySlowed = true;
            Debug.Log(newSpeed);
        }

        //Handles Speeding enemies up once they get out of range
        if(Vector3.Distance(transform.position, target.transform.position) >= (range - .1)){
           e.speed = e.maxSpeed;
        }

        //Handles Damaging Enemy
        if(laserCountDown <= 0){
            e.TakeDamage(laserDPS);
            laserCountDown = 1/laserFireRate;
            currentShots--;
        }
        laserCountDown -= Time.deltaTime;
    }

    void BreakTurret(){
        currentShots = 0;
        isBroken = true;
       // Make turret head rotate down
       PartToRotate.rotation = Quaternion.Euler(25f, 0f, 0f);

       //Enable break image
       breakImage.enabled = true;
    }

    public void FixTurret(){
        
       // Make turret head rotate down
       if(isBroken == true){
            PartToRotate.rotation = Quaternion.Euler(-25f, 0f, 0f);
       }
       

       //Enable break image
       breakImage.enabled = false;

       //Reloads shots
       currentShots = maxShots;

       isBroken = false;
    }
}
