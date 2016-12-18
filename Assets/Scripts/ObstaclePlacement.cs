using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacement : MonoBehaviour
{
    public Color[] colors;          // 0 - light / 1 - dark

    private GridManager grid;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }
}
