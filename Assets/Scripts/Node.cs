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
    public GameObject blackObstaclePrefab;
    public GameObject redObstaclePrefab;
    public int numberOfPieces;

    private int x, z;
    private NodeType nodeType = NodeType.Light;
    private GameObject tObstacle;
    private GridManager gridManager;
    private ObstaclePlacement obstaclePlacement;
    private GameManager gameManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        obstaclePlacement = FindObjectOfType<ObstaclePlacement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetNodeVariables(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public void SetNodeType(NodeType nodeType)
    {
        if (this.nodeType != NodeType.Finish)
        {
            this.nodeType = nodeType;
            SetColor(nodeType);
        }
    }

    public void SetColor(NodeType nodeType)
    {
        if (obstaclePlacement == null)
        {
            obstaclePlacement = FindObjectOfType<ObstaclePlacement>();
        }

        GetComponent<MeshRenderer>().material.color = obstaclePlacement.colors[(int)nodeType];
    }

    public void PlacePiece(PieceType pieceType)
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        switch (pieceType)
        {
            case PieceType.Black:
                GameObject blackObstacle = Instantiate(blackObstaclePrefab) as GameObject;
                blackObstacle.transform.localScale = Vector3.one * gridManager.nodeSize;
                blackObstacle.transform.rotation = this.transform.rotation;
                blackObstacle.transform.SetParent(transform);
                blackObstacle.transform.position = this.transform.position + Vector3.up * gridManager.nodeSize / 2 + Vector3.up * gridManager.nodeSize * numberOfPieces;

                numberOfPieces++;
                FindNextNode();
                break;

            case PieceType.Red:
                GameObject redObstacle = Instantiate(redObstaclePrefab) as GameObject;
                redObstacle.transform.localScale = Vector3.one * gridManager.nodeSize;
                redObstacle.transform.rotation = this.transform.rotation;
                redObstacle.transform.SetParent(transform);
                redObstacle.transform.position = this.transform.position + Vector3.up * gridManager.nodeSize / 2 + Vector3.up * gridManager.nodeSize * numberOfPieces;

                numberOfPieces++;
                FindNextNode();

                if (z + numberOfPieces < gridManager.gridSize)
                {
                    obstaclePlacement.AddRedObstacle(redObstacle, gridManager.nodes[x, z + numberOfPieces], obstaclePlacement.numberOfRedObstacles - 1);
                }
                else
                {
                    obstaclePlacement.AddRedObstacle(redObstacle, obstaclePlacement.numberOfRedObstacles - 1);
                }

                redObstacle.GetComponent<RedObstacle>().id = obstaclePlacement.numberOfRedObstacles - 1;
                redObstacle.GetComponent<RedObstacle>().node = this;

                break;
        }
    }

    private void OnMouseEnter()
    {
        if (obstaclePlacement.remainingObstacles > 0)
        {
            // Show transparent tObstacle.
            if (tObstacle != null)
            {
                Destroy(tObstacle);
            }

            tObstacle = Instantiate(transparentObstaclePrefab);
            tObstacle.transform.localScale = Vector3.one * gridManager.nodeSize;
            tObstacle.transform.rotation = this.transform.rotation;
            tObstacle.transform.SetParent(transform);
            tObstacle.transform.position = this.transform.position + Vector3.up * gridManager.nodeSize / 2 + Vector3.up * gridManager.nodeSize * numberOfPieces;
        }
    }

    private void OnMouseExit()
    {
        // Destroy transparent tObstacle.
        if (tObstacle != null)
        {
            Destroy(tObstacle);
        }
    }

    private void OnMouseDown()
    {
        if (obstaclePlacement.remainingObstacles > 0)
        {
            switch (obstaclePlacement.currentPieceType)
            {
                case PieceType.Black:
                    if (obstaclePlacement.numberOfBlackObstacles > 0)
                    {
                        PlacePiece(PieceType.Black);
                        obstaclePlacement.numberOfBlackObstacles--;
                    }
                    break;
                case PieceType.Red:
                    if (obstaclePlacement.numberOfRedObstacles > 0)
                    {
                        PlacePiece(PieceType.Red);
                        obstaclePlacement.numberOfRedObstacles--;
                    }
                    break;
            }

            Redraw();
            obstaclePlacement.RefreshUI();
        }
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
            gameManager.SetLastNode(nodeType);
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
