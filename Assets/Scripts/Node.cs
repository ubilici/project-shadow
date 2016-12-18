using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject transparentObstaclePrefab;
    public GameObject obstaclePrefab;
    public int numberOfPieces;

    private GameObject tObstacle;
    private GameObject obstacle;
    private GridManager grid;
    private ObstaclePlacement obstaclePlacement;

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
    }
}
