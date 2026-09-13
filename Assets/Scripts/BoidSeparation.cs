using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSeparation : BoidComponent {
	override public Vector3 GetDirection(Collider2D[] neighbors, int size) {
		Vector3 separation = Vector3.zero;
		Vector3 obstacle = Vector3.zero;
		Vector3 wall = Vector3.zero;
		Vector3 tmp;
		for (int i = 0; i < size; i++) {
            Vector3 closestPoint = neighbors[i].ClosestPoint(transform.position);
			tmp = transform.position - closestPoint;

			if (neighbors[i].gameObject.layer == gameObject.layer) {
				separation += tmp.normalized / (tmp.magnitude + 0.0001f);
			} else if (neighbors[i].gameObject.layer == 7) {
				obstacle += tmp.normalized / (tmp.magnitude + 0.0001f);
			} else if (neighbors[i].gameObject.layer == 6) {
				wall += tmp.normalized / (tmp.magnitude + 0.0001f);
			}
		}
		return separation.normalized * BoidShared.SeparationComponent + 
			   obstacle.normalized * BoidShared.ObstacleComponent + 
			   wall.normalized * BoidShared.WallComponent;
	}
}
