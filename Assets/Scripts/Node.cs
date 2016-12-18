using System.Collections;
using UnityEngine;

public enum NodeType
{
    Light,
    Shadow,
    Finish
}

public class Node : MonoBehaviour
{
    public GameObject transparentObstaclePrefab;
    public GameObject obstaclePrefab;
    public int numberOfPieces;

    private int x, z;
    private NodeType nodeType = NodeType.Light;
    private GameObject tObstacle;
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

    public void SetNodeType(NodeType nodeType)
    {
        this.nodeType = nodeType;
        SetColor(nodeType);
    }

    public void SetColor(NodeType nodeType)
    {
        if (obstaclePlacement == null)
        {
            obstaclePlacement = FindObjectOfType<ObstaclePlacement>();
        }

        if (GetComponent<MeshRenderer>().material.color != obstaclePlacement.colors[2])
        {
            GetComponent<MeshRenderer>().material.color = obstaclePlacement.colors[(int)nodeType];
        }
    }

    public void PlacePiece()
    {
        if(gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        GameObject obstacle = Instantiate(obstaclePrefab) as GameObject;
        obstacle.transform.localScale = Vector3.one * gridManager.nodeSize;
        obstacle.transform.rotation = this.transform.rotation;
        obstacle.transform.SetParent(transform);
        obstacle.transform.position = this.transform.position + Vector3.up * gridManager.nodeSize / 2 + Vector3.up * gridManager.nodeSize * numberOfPieces;

        numberOfPieces++;
        FindNextNode();
    }

    private void OnMouseEnter()
    {
        // Show transparent tObstacle.
        if(tObstacle != null)
        {
            Destroy(tObstacle);
        }

        tObstacle = Instantiate(transparentObstaclePrefab);
        tObstacle.transform.localScale = Vector3.one * gridManager.nodeSize;
        tObstacle.transform.rotation = this.transform.rotation;
        tObstacle.transform.SetParent(transform);
        tObstacle.transform.position = this.transform.position + Vector3.up * gridManager.nodeSize / 2 + Vector3.up * gridManager.nodeSize * numberOfPieces;
    }

    private void OnMouseExit()
    {
        // Destroy transparent tObstacle.
        Destroy(tObstacle);
    }

    private void OnMouseDown()
    {
        PlacePiece();
        Redraw();
    }

    private void FindNextNode()
    {
        if (z + numberOfPieces < gridManager.gridSize)
        {
            gridManager.nodes[x, z + numberOfPieces].SetNodeType(NodeType.Shadow);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log(nodeType);
        }
    }

    private void Redraw()
    {
        Destroy(tObstacle);
        tObstacle = Instantiate(transparentObstaclePrefab);
        tObstacle.transform.localScale = Vector3.one * gridManager.nodeSize;
        tObstacle.transform.SetParent(transform);
        tObstacle.transform.position = this.transform.position + Vector3.up * gridManager.nodeSize / 2 + Vector3.up * gridManager.nodeSize * numberOfPieces;
    }
}
