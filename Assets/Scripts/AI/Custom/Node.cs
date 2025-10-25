using UnityEngine;

public class Node
{
    public bool isWalkable;
    public Vector3 worldPosition;

    public Node(bool walkable, Vector3 worldPos)
    {
        this.isWalkable = walkable;
        this.worldPosition = worldPos;
    }
}
