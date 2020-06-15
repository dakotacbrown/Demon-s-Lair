﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int hitPoints;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Bullet") {
            hitPoints--;
            if(hitPoints <= 0) {
                GameController.instance.KillEnemy();
                Destroy(gameObject);
            }
        }
    }
}
