using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{   
    private int speed = 0;
    private Vector3 dir;

    void Start() {
        speed = Random.Range(5, 21);
        dir = new Vector3(Random.Range(0,2) * 2 - 1, 0, 0);
    }

    void FixedUpdate() {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.layer == 6) {
            dir = Vector3.Reflect(dir, Vector3.right);
        }
    }
}
