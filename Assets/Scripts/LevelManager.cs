using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelPiece[] levelPieces;
    [Header("Start Point")]
    public int startX;
    public int startZ;
    [Header("Finish Point")]
    public int finishX;
    public int finishZ;
    [Header("Number of Obstacles")]
    public int blackObstacles;
    public int redObstacles;
    [Header("Reference Holder")]
    public Transform player;

    private GridManager gridManager;
    private ObstaclePlacement obstaclePlacement;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        obstaclePlacement = FindObjectOfType<ObstaclePlacement>();
    }

    public void GenerateLevel()
    {
        SetPieceNumbers();   

        player.position = gridManager.nodes[startX, startZ].transform.position + Vector3.up;
        gridManager.nodes[startX, startZ].SetNodeType(NodeType.Shadow);
        gridManager.nodes[finishX, finishZ].SetNodeType(NodeType.Finish);

        if (levelPieces.Length > 0)
        {
            foreach (var levelPiece in levelPieces)
            {
                switch (levelPiece.pieceType)
                {
                    case PieceType.Black:
                        gridManager.nodes[levelPiece.x, levelPiece.z].PlacePiece(PieceType.Black);
                        break;
                    case PieceType.Empty:
                        Destroy(gridManager.nodes[levelPiece.x, levelPiece.z].gameObject);
                        break;
                }
            }
        }
    }

    private void SetPieceNumbers()
    {
        obstaclePlacement.SetObstacleCount(redObstacles, blackObstacles);

        if(blackObstacles > 0)
        {
            obstaclePlacement.currentPieceType = PieceType.Black;
        }
        else if (redObstacles > 0)
        {
            obstaclePlacement.currentPieceType = PieceType.Red;
        }
        else
        {
            obstaclePlacement.currentPieceType = PieceType.None;
        }
    }
}

public enum PieceType
{
    Black,
    Red,
    Empty,
    None
}

[System.Serializable]
public class LevelPiece
{
    public int x, z;
    public PieceType pieceType;
}
