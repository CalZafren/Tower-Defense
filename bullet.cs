using UnityEngine;

public class bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 50f;
    public float explodeRadius = 0f;
    public GameObject impactEffect;
    public GameObject missileImpactEffect;
    private Turret parentTurret;
    public int damage = 1;
    private Transform tempEnemyLocation;
    public bool missileHadTarget = false;

    public void Seek(Transform _target){
        target = _target;
    }

    public void GetParent(Turret turret){
        parentTurret = turret;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //Functionality for if target dies before bullet reaches it
        if(target == null){
            Destroy(gameObject);
            parentTurret.currentShots++;
            parentTurret.fireCountDown = 0;
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget(){
        //Destroy bullet
        Destroy(gameObject);
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 1f);
        if(explodeRadius > 0f){
            Explode();
        }else{
            Damage(target);
        }
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider collider in colliders){
            if(collider.tag == "Enemy"){
                Damage(collider.transform);
            }
        }
        GameObject missileEffect = (GameObject)Instantiate(missileImpactEffect, transform.position, transform.rotation);
        Destroy(missileEffect, 3f);
    }

    //Function to damage enemy
    void Damage(Transform enemy){
        Enemy e = enemy.GetComponent<Enemy>();
        if(e != null){
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
