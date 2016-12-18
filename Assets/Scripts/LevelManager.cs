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
    [Header("Reference Holder")]
    public Transform player;

    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void GenerateLevel()
    {
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
                        gridManager.nodes[levelPiece.x, levelPiece.z].PlacePiece();
                        break;
                    case PieceType.Empty:
                        Destroy(gridManager.nodes[levelPiece.x, levelPiece.z].gameObject);
                        break;
                }
            }
        }
    }
}

public enum PieceType
{
    Black,
    Red,
    Empty
}

[System.Serializable]
public class LevelPiece
{
    public int x, z;
    public PieceType pieceType;
}
