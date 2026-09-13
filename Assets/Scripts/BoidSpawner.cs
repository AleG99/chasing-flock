using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {

	public float radius = 10f;
	public int count = 50;
	public GameObject boid = null;

	void Start () {
		if (boid != null) {
			for (int i = 0; i < count; i++) {
                bool creating = true;
                Vector3 boidPos = Vector3.zero;
                while (creating) {
                    Vector3 tmpPos = Random.insideUnitCircle;
                    Vector3 testPos = transform.position + tmpPos * radius;
                    Collider2D collision = Physics2D.OverlapCircle(testPos, 1.0f);
                    if (!collision) {
                        creating = false;
                        boidPos += testPos;
                    }
                };

                GameObject go = Instantiate(boid, boidPos, transform.rotation);
				go.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 359.9f));
				go.name = boid.name + " " + i;
			}
		}
	}
}
