
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TileGraph {
	protected float tileSize;
	protected Node[,] matrix;
	protected Dictionary<Node, float[]> map;
	
	public TileGraph(float x, float y, float ts) {
		tileSize = ts;

		matrix = new Node[
          (int) Math.Floor (x / tileSize),
          (int) Math.Floor (y / tileSize)
        ];

		map = new Dictionary<Node, float[]> ();
		for (int i = 0; i < matrix.GetLength(0); i++) {
			for (int j = 0; j < matrix.GetLength(1); j++) {
				Node n = new Node ("" + i + "," + j);
				matrix[i, j] = n;
				map[n] = new float[] { (i + .5f) * tileSize, (j + .5f) * tileSize };
			}
		}	
	}

	// quantization
	public Node this[float x, float y] {
		get {
			return matrix[
				(int) Math.Floor (x / tileSize), 
				(int) Math.Floor (y / tileSize) 
			];
		}
	}
	
	// localization
	public float[] this[Node n] {
		get {
			if (!map.ContainsKey(n)) return null;
			return map[n];
		}
	}

	public Node[] Nodes {
		get {
			return map.Keys.ToArray(); 
		}
	}

	public Node[,] Matrix {
		get {
			return matrix;
		}
	}
}