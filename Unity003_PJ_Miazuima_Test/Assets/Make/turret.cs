using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {
    private Transform target;

    [Header("Attributes")]

    public float range = 15f; //인식 거리
    public float fireRate = 1f;
    private float fireCountdown = 0f; // <-?
    
    [Header("Setup Fields")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float trackingspeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
   

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdataTarget", 0f, 0.5f);
	}
	void UpdataTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy< shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }

        if (nearstEnemy != null && shortestDistance <= range)
        {
            target = nearstEnemy.transform;
          
        }

    }

	// Update is called once per frame
	void Update () {
		if (target == null)
        {
            return;
        }
        //target lockon
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = lookRotation.eulerAngles;
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime* trackingspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y-14f,0f); // 좌표 보정 <- 제대로 만들것;

        // shoot
        if (fireCountdown <= 0f)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

	}
    void shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet bullet = bulletGo.GetComponent<bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }        
        // Debug.Log("shoot");
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
