using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateDirections
{
    PositiveX,
    PositiveZ,
    NegativeX,
    NegativeZ,
    None
}

public class GridInput : MonoBehaviour
{
    public Transform grid;
    public float rotationSpeed;
    public float leanSpeed;
    public float maximumLean;

    private bool fixRotation;
    private float lerpTime;
    private Quaternion startRotation;
    private RotateDirections currentDirection = RotateDirections.None;

    public void SetDirection(RotateDirections rotateDirection)
    {
        currentDirection = rotateDirection;
    }

    private void Update()
    {
        RotateGrid();

        if (fixRotation)
        {
            lerpTime += Time.deltaTime * leanSpeed * 4;
            grid.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(Vector3.zero), lerpTime);
        }
    }

    private void RotateGrid()
    {
        switch (currentDirection)
        {
            case RotateDirections.PositiveX:
                fixRotation = false;
                LeanTowardsX(-1);
                break;
            case RotateDirections.NegativeX:
                fixRotation = false;
                LeanTowardsX(1);
                break;
            case RotateDirections.PositiveZ:
                fixRotation = false;
                LeanTowardsZ(-1);
                break;
            case RotateDirections.NegativeZ:
                fixRotation = false;
                LeanTowardsZ(1);
                break;
            case RotateDirections.None:
                if (!fixRotation)
                {
                    FixRotation();
                }
                break;
        }
    }

    private void LeanTowardsZ(float value)
    {
        if (value < 0)
        {
            grid.Rotate(Vector3.back, leanSpeed * value, Space.Self);
        }
        else if (value > 0)
        {
            grid.Rotate(Vector3.back, leanSpeed * value, Space.Self);
        }

        if (Quaternion.Angle(grid.localRotation, Quaternion.Euler(Vector3.zero)) > maximumLean)
        {
            if (grid.localEulerAngles.z > 360 - maximumLean * 2)
            {
                grid.localEulerAngles = new Vector3(grid.localEulerAngles.x, 0, maximumLean * -1);
            }
            else if (grid.localEulerAngles.z < maximumLean * 2)
            {
                grid.localEulerAngles = new Vector3(grid.localEulerAngles.x, 0, maximumLean); 
            }
        }
    }

    private void LeanTowardsX(float value)
    {
        if (value < 0)
        {
            grid.Rotate(Vector3.left, leanSpeed * value, Space.Self);
        }
        else if (value > 0)
        {
            grid.Rotate(Vector3.left, leanSpeed * value, Space.Self);
        }

        if (Quaternion.Angle(grid.localRotation, Quaternion.Euler(Vector3.zero)) > maximumLean)
        {
            if (grid.localEulerAngles.x > 360 - maximumLean * 2)
            {
                grid.localEulerAngles = new Vector3(maximumLean * -1, 0, grid.localEulerAngles.z);
            }
            else if (grid.localEulerAngles.x < maximumLean * 2)
            {
                grid.localEulerAngles = new Vector3(maximumLean, 0, grid.localEulerAngles.z);
            }
        }
    }

    private void FixRotation()
    {
        startRotation = grid.rotation;
        fixRotation = true;
        lerpTime = 0;
    }
}
