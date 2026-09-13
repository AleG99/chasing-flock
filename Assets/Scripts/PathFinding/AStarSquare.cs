using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AStarSquare : MonoBehaviour {
	public int x = 100;
	public int y = 100;
	public GameObject o;

	protected Node[,] matrix;
	protected Graph g;
	protected TileGraph tg;

	public bool stopAtFirstHit = false;

	public enum Heuristics { Euclidean, Manhattan, Bisector, FullBisector, Zero };
	public HeuristicFunction [] myHeuristics = { EuclideanEstimator, ManhattanEstimator, BisectorEstimator,
												 FullBisectorEstimator, ZeroEstimator };
	public Heuristics heuristicToUse = Heuristics.Euclidean;

	void Start () {
		tg = new TileGraph(x, y, 10);
		matrix = tg.Matrix;

		g = new Graph();
		CreateLabyrinth(g, matrix);
	}

	protected static float EuclideanEstimator(Node from, Node to) {
		return (from.sceneObject.transform.position - to.sceneObject.transform.position).magnitude;
	}

	protected static float ManhattanEstimator(Node from, Node to) {
		return (
				Mathf.Abs(from.sceneObject.transform.position.x - to.sceneObject.transform.position.x) +
				Mathf.Abs(from.sceneObject.transform.position.z - to.sceneObject.transform.position.z)
			);
	}

	protected static float BisectorEstimator(Node from, Node to) {
		Ray r = new Ray (Vector3.zero, to.sceneObject.transform.position);
		return Vector3.Cross(r.direction, from.sceneObject.transform.position - r.origin).magnitude;
	}

	protected static float FullBisectorEstimator(Node from, Node to) {
		/*
		Ray r = new Ray (Vector3.zero, to.sceneObject.transform.position);
		Vector3 toBisector = Vector3.Cross (r.direction, from.sceneObject.transform.position - r.origin);
		return toBisector.magnitude + (to.sceneObject.transform.position - ( from.sceneObject.transform.position + toBisector ) ).magnitude ;
		*/
		Ray r = new Ray (Vector3.zero, to.sceneObject.transform.position);
		float toBisector = Vector3.Cross(r.direction, from.sceneObject.transform.position - r.origin).magnitude;
		return toBisector + Mathf.Abs(Vector3.Dot(to.sceneObject.transform.position - from.sceneObject.transform.position, r.direction));
	}

	protected static float ZeroEstimator (Node from, Node to) { return 0f; }
	
	protected void CreateLabyrinth(Graph g, Node[,] crossings) {
		for (int i = 0; i < crossings.GetLength(0); i += 1) {
			for (int j = 0; j < crossings.GetLength(1); j += 1) {
				crossings[i, j].sceneObject = Instantiate(o);

				float[] xy = tg[crossings[i,j]];
				crossings[i, j].sceneObject.transform.position = new Vector3(xy[0], xy[1], 0);

				g.AddNode(crossings[i, j]);
				foreach (Edge e in Edges(crossings, i, j)) {
					g.AddEdge(e);
				}
			}
		}
	}

    protected Edge[] Edges(Node[,] matrix, int x, int y) {
        List<Edge> result = new List<Edge>();
        if (x != 0)
            result.Add(new Edge(matrix[x, y], matrix[x - 1, y], Distance(matrix[x, y], matrix[x - 1, y])));

        if (y != 0)
            result.Add(new Edge(matrix[x, y], matrix[x, y - 1], Distance(matrix[x, y], matrix[x, y - 1])));

        if (x != (matrix.GetLength(0) - 1))
            result.Add(new Edge(matrix[x, y], matrix[x + 1, y], Distance(matrix[x, y], matrix[x + 1, y])));

        if (y != (matrix.GetLength(1) - 1))
            result.Add(new Edge(matrix[x, y], matrix[x, y + 1], Distance(matrix[x, y], matrix[x, y + 1])));

        return result.ToArray();
    }

    protected virtual float Distance(Node from, Node to) {
        return 1f;
    }

	public Edge[] solve(Vector2 start, Vector2 end) {
		Node s = tg[start.x, start.y];
		Node e = tg[end.x, end.y];

		AStarSolver.immediateStop = stopAtFirstHit;
		Edge[] path = AStarSolver.Solve(g, s, e, myHeuristics [(int) heuristicToUse]);

		return path;
	}

	public float[] getPositionFromNode(Node n) {
		return tg[n];
	}

	public Node getNodeFromPosition(float[] pos) {
		return tg[pos[0], pos[1]];
	}
}
