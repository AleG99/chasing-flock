using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Range(10,30)] public int obstacleNumber = 10;
    public GameObject obstacle = null;

    // Start is called before the first frame update
    void Start() {
        if (obstacle) {
            for (int i = 0; i < obstacleNumber; i++) {
                int x = Random.Range(1, 100);
                int y = Random.Range(1, 100);

                GameObject ob = Instantiate(obstacle, new Vector3(x, y, 0), Quaternion.identity);
                ob.name = "Obstacle " + i;
            }
        }
    }
}
