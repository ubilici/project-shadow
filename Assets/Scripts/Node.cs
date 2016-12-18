using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject transparentObstaclePrefab;
    public GameObject obstaclePrefab;
    public int numberOfPieces;

    private int gridSize, x, z;
    private bool isWalkable = false;
    private GameObject tObstacle;
    private GameObject obstacle;
    private GridManager gridManager;
    private ObstaclePlacement obstaclePlacement;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        obstaclePlacement = FindObjectOfType<ObstaclePlacement>();
    }

    public void SetNodeVariables(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public void SetShadow(int value)
    {
        isWalkable = value == 1 ? true : false;
        GetComponent<MeshRenderer>().material.color = obstaclePlacement.colors[value];
    }

    private void OnMouseEnter()
    {
        // Show transparent tObstacle.
        tObstacle = Instantiate(transparentObstaclePrefab, transform);
        tObstacle.transform.position = this.transform.position + Vector3.up * 0.8f + Vector3.up * 1.5f * numberOfPieces;
    }

    private void OnMouseExit()
    {
        // Destroy transparent tObstacle.
        Destroy(tObstacle);
    }

    private void OnMouseDown()
    {
        obstacle = Instantiate(obstaclePrefab, transform);
        obstacle.transform.position = this.transform.position + Vector3.up * 0.8f + Vector3.up * 1.5f * numberOfPieces;

        numberOfPieces++;
        FindNextNode();
    }

    private void FindNextNode()
    {
        if (z + numberOfPieces < gridManager.gridSize)
        {
            gridManager.nodes[x, z + numberOfPieces].SetShadow(1);
        }
    }
}
