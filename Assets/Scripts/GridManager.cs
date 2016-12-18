using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Node nodePrefab;
    public float areaSize;
    public int gridSize;
    public Node[,] nodes;

    [HideInInspector]
    public float nodeSize;
    private float distanceBetweenNodes;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        CreateGrid();
        levelManager.GenerateLevel();
    }

    private void CreateGrid()
    {
        nodes = new Node[gridSize, gridSize];

        distanceBetweenNodes = areaSize / gridSize;
        nodeSize = distanceBetweenNodes - 0.1f;

        float start = (1 - gridSize) * distanceBetweenNodes / 2;
        Vector3 currentPoint = new Vector3(start, 0, start);

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Node node = Instantiate(nodePrefab, transform) as Node;
                node.transform.localScale = new Vector3(nodeSize, 0.2f, nodeSize);
                node.transform.position = currentPoint;
                node.transform.name = x + "_" + z;
                node.SetNodeVariables(x, z);

                nodes[x, z] = node;

                currentPoint.z += distanceBetweenNodes;
            }
            currentPoint.z = start;
            currentPoint.x += distanceBetweenNodes;
        }
    }
}
