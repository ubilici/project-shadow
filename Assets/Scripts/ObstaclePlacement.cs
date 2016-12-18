using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacement : MonoBehaviour
{
    private GridManager grid;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }
}
