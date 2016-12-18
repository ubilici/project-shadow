using System.Collections;
using UnityEngine;

public class ObstaclePlacement : MonoBehaviour
{
    public PieceType currentPieceType;
    public int numberOfBlackObstacles;
    public int numberOfRedObstacles;
    public int remainingObstacles
    {
        get
        {
            return numberOfBlackObstacles + numberOfRedObstacles;
        }
    }
    public Color[] colors;          // 0 - light / 1 - dark / 2 - finish

    private GameObject[] redObstacles;
    private Node[] shadows;

    public void SetObstacleCount(int redObstacle, int blackObstacle)
    {
        numberOfBlackObstacles = blackObstacle;
        numberOfRedObstacles = redObstacle;

        redObstacles = new GameObject[numberOfRedObstacles];
        shadows = new Node[numberOfRedObstacles];
    }

    public void AddRedObstacle(GameObject redObstacle, Node shadow, int id)
    {
        redObstacles[id] = redObstacle;
        shadows[id] = shadow;
    }

    public void AddRedObstacle(GameObject redObstacle, int id)
    {
        redObstacles[id] = redObstacle;
    }

    public void RemoveRedObstacle(int id)
    {
        Destroy(redObstacles[id]);
        if (shadows[id] != null)
        {
            shadows[id].SetNodeType(NodeType.Light);
        }
        numberOfRedObstacles++;
    }

}
