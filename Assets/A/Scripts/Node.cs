using UnityEngine;
using System.Collections;

public class Node {

    //le state walkable et sa position dans le grid (x,y)
    public bool walkable;
	public Vector3 worldPosition;

    //poisition du node
    public int gridX;
	public int gridY;

    //distance from starting node
    public int gCost;
    //distance from end node
    public int hCost;
    //keeps track of the current node
    public Node parent;

    //assigne des valeurs à la création d'un node (keeps track of its position)
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

    //gcost+hcost
    public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
