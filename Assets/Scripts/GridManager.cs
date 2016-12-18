using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Node nodePrefab;
    public int gridSize;
    public float nodeSize;
    public Node[,] nodes;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        nodes = new Node[gridSize, gridSize];

        float start = (1 - gridSize) * nodeSize / 2;
        Vector3 currentPoint = new Vector3(start, 0, start);

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Node node = Instantiate(nodePrefab, transform) as Node;
                node.transform.position = currentPoint;
                node.transform.name = x + "_" + z;

                nodes[x, z] = node;

                currentPoint.z += nodeSize;
            }
            currentPoint.z = start;
            currentPoint.x += nodeSize;
        }
    }
}
