using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateDirections
{
    Horizontal,
    Vertical
}

public class RotateArrow : MonoBehaviour
{
    public RotateDirections rotateDirection;
    public HorizontalDirections horizontalDirection;
    public VerticalDirections verticalDirection;

    private GridInput gridInput;

    private void Awake()
    {
        gridInput = FindObjectOfType<GridInput>();
    }

    private void OnMouseEnter()
    {
        switch (rotateDirection)
        {
            case RotateDirections.Horizontal:
                gridInput.SetDirection(horizontalDirection);
                break;
            case RotateDirections.Vertical:
                gridInput.SetDirection(verticalDirection);
                break;
        }
    }

    private void OnMouseExit()
    {
        switch (rotateDirection)
        {
            case RotateDirections.Horizontal:
                gridInput.SetDirection(HorizontalDirections.None);
                break;
            case RotateDirections.Vertical:
                gridInput.SetDirection(VerticalDirections.None);
                break;
        }
    }

}
