using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    public RotateDirections rotateDirection;

    private GridInput gridInput;

    private void Awake()
    {
        gridInput = FindObjectOfType<GridInput>();
    }

    private void OnMouseEnter()
    {
        gridInput.SetDirection(rotateDirection);
    }

    private void OnMouseExit()
    {
        gridInput.SetDirection(RotateDirections.None);
    }

}
