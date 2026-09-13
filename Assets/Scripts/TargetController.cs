using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {
    private float radius = 10.0f;
    private int corner = 0;

    void Start() {
        ResetPosition();
    }

    void ResetPosition() {
        int newCorner = Random.Range(1, 5);
        while (newCorner == corner) {
            newCorner = Random.Range(1, 5);
        }

        corner = newCorner;
        Vector3 cornerPos = Vector3.zero;
        Vector2 tmpPos = Random.insideUnitCircle;
        Vector3 targetPos = Vector3.zero;

        switch (corner) {
            case 1:
                cornerPos += new Vector3(0, 100);
                targetPos += new Vector3(Mathf.Abs(tmpPos.x), -Mathf.Abs(tmpPos.y));
                break;
            case 2:
                cornerPos += new Vector3(100, 100);
                targetPos += new Vector3(-Mathf.Abs(tmpPos.x), -Mathf.Abs(tmpPos.y));
                break;
            case 3:
                cornerPos += new Vector3(100, 0);
                targetPos += new Vector3(-Mathf.Abs(tmpPos.x), Mathf.Abs(tmpPos.y));
                break;
            default:
                cornerPos += new Vector3(0, 0);
                targetPos += new Vector3(Mathf.Abs(tmpPos.x), Mathf.Abs(tmpPos.y));
                break;
        }

        transform.position = cornerPos + targetPos * radius;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 8) {
            ResetPosition();
        }
    }
}
