using UnityEngine;

public abstract class BoidComponent : MonoBehaviour {
	public abstract Vector3 GetDirection(Collider2D[] neighbors, int size);
}

public class BoidBlending : MonoBehaviour {
    public GameObject target = null;
	public AStarSquare pathFinding = null;

	private Edge[] path = null;
	private int currentEdge = 0;
	private Collider2D[] neighbors = new Collider2D[100];
	

    void Start() {
        target = GameObject.Find("Target");
		pathFinding = GameObject.Find("PathFinding").GetComponent<AStarSquare>();
    }

	void FixedUpdate() {
		Vector3 globalDirection = Vector3.zero;

        if (target != null && path == null)
			calculatePath();

		if (path != null) {
			float[] currentPos = { transform.position.x, transform.position.y };
			Node current = pathFinding.getNodeFromPosition(currentPos);

			if (currentEdge != path.Length) {
				float[] targetNodePos = pathFinding.getPositionFromNode(path[currentEdge].to);
				globalDirection += ((new Vector3(targetNodePos[0], targetNodePos[1])) - transform.position).normalized * BoidShared.TargetComponent;
				
				if (current.description == path[currentEdge].to.description)
					currentEdge++;
			} else {
				globalDirection += (target.transform.position - transform.position).normalized * BoidShared.TargetComponent;
			}
		}

        int count = Physics2D.OverlapCircleNonAlloc(transform.position, BoidShared.BoidFOW, neighbors);
        
		foreach (BoidComponent bc in GetComponents<BoidComponent>()) {
			globalDirection += bc.GetDirection(neighbors, count);
		}

		if (globalDirection != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(Vector3.forward, (globalDirection.normalized + transform.up) / 2f);
		}

		transform.position += transform.up * BoidShared.BoidSpeed * Time.deltaTime;
	}

	private void calculatePath() {
		Vector2 start = transform.position;
		Vector2 end = target.transform.position;

		path = pathFinding.solve(start, end);
		currentEdge = 0;
	}
}