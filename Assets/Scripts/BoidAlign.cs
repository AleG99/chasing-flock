using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAlign : BoidComponent {
	override public Vector3 GetDirection(Collider2D[] neighbors, int size) {
		Vector3 alignment = Vector3.zero;
		for (int i = 0; i < size; i++) {
			if (neighbors[i].gameObject.layer == gameObject.layer) {
				alignment += neighbors[i].gameObject.transform.up;
			}
		}
		return alignment.normalized * BoidShared.AlignComponent;
	}
}
