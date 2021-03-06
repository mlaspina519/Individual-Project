﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {
    //private Rigidbody2D rigid;
    private Transform target;   //target of enemy
    public Transform myTransform;   // current transform data
    float moveSpeed = 3.0f;     // speed
    float rotationSpeed = 3.0f;     // rotation

    public GameObject bulletBill;
    public int health = 3;
    public int Health { get { return health; } }

    // Cache data
    void Awake() {
        //rigid = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }
    
    // target the player
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("ShootBullet", 0.5f, 1.0f);
    }
    
    void Update() {
        Vector3 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 180;
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 180);
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

    // Look towards player
    //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
   
    // Move towards player
    //myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }

    public void Hit() {
        health--;
    }

    // Fires bullet
    void ShootBullet() {
            if (target.localScale.x < 0) {
                GameObject bullet = (GameObject)Instantiate(bulletBill, transform.position, Quaternion.identity);
                bullet.transform.localScale = new Vector2(-bullet.transform.localScale.x, bullet.transform.localScale.y);
                bullet.GetComponent<EnemyBullet>().ChangeDirection();
            } else {
                GameObject bullet = (GameObject)Instantiate(bulletBill, transform.position, Quaternion.identity);
                bullet.transform.localScale = new Vector2(bullet.transform.localScale.x, bullet.transform.localScale.y);
            }
    }

}