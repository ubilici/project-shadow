using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI Elements")]
    public GameObject blackHighlight;
    public GameObject redHighlight;
    public Text blackNumber;
    public Text redNumber;

    private GameObject[] redObstacles;
    private Node[] shadows;

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            currentPieceType = currentPieceType == PieceType.Red ? PieceType.Black : PieceType.Red;
            RefreshUI();
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            currentPieceType = currentPieceType == PieceType.Red ? PieceType.Black : PieceType.Red;
            RefreshUI();
        }
    }

    public void SetObstacleCount(int redObstacle, int blackObstacle)
    {
        numberOfBlackObstacles = blackObstacle;
        numberOfRedObstacles = redObstacle;

        redObstacles = new GameObject[numberOfRedObstacles];
        shadows = new Node[numberOfRedObstacles];

        RefreshUI();
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

        RefreshUI();
    }

    public void RefreshUI()
    {
        if(currentPieceType == PieceType.Black)
        {
            blackHighlight.SetActive(true);
            redHighlight.SetActive(false);
        }
        else if(currentPieceType == PieceType.Red)
        {
            blackHighlight.SetActive(false);
            redHighlight.SetActive(true);
        }

        blackNumber.text = "x" + numberOfBlackObstacles;
        redNumber.text = "x" + numberOfRedObstacles;
    }

}
