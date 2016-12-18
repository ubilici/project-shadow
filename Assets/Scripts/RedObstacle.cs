using System.Collections;
using UnityEngine;

public class RedObstacle : MonoBehaviour
{
    public int id;
    public Node node;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            node.numberOfPieces--;
            FindObjectOfType<ObstaclePlacement>().RemoveRedObstacle(id);
        }
    }
}
