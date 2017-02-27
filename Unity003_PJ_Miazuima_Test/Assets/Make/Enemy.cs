using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    { // 포인팅 지정
        target = Waypoint.points[0];
    }
    void Update()
    { // 포인팅 이동
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);


        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

    }
    void GetNextWaypoint()
    {
        if (wavepointIndex == Waypoint.points.Length-2)
        {
            if (WaveSpawner.breaksign == false)
            {
                WaveSpawner.breaksign = true; // 원본 비 삭제
            }
            else if(WaveSpawner.breaksign == true){
                print("waring");
                Destroy(gameObject);

           }
        }
        if (wavepointIndex < Waypoint.points.Length - 1)
        {
            wavepointIndex++;
            target = Waypoint.points[wavepointIndex];
        }
    }

}
