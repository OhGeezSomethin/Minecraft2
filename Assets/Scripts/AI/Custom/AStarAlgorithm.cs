using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AStarAlgorithm : MonoBehaviour
{
    List<Node> open = new List<Node>(); // Stores all nodes with an already calculated f-cost
    List<Node> closed = new List<Node>(); // Nodes that have already been evaluated

    void PathFind()
    {
        open.Add(null); // Add the starting node to open

        while (open.Count > 0)
        {
            var currentNode = open[0]; // Current node is set to node with the lowest f-cost
            open.Remove(currentNode);
            closed.Add(currentNode);

            /* if (currentNode == targetNode) // Assumes path to target has been found
            {
                return;
            } else
            {
                foreach (var neighborNode in currentNode)
                {
                    if (closed.Contains(neighborNode))
                    {

                    } else if (!open.Contains(neighborNode))
                    {
                        var gCost = 0;
                        var hCost = 0;

                        var fCost = gCost + hCost;

                        if (!open.Contains(neighborNode))
                        {
                            open.Add(neighborNode);
                        }
                    }
                }
            } */
        }
    }
}
