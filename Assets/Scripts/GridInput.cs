using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HorizontalDirections
{
    PositiveX,
    NegativeX,
    None
}

public enum VerticalDirections
{
    PositiveZ,
    NegativeZ,
    None
}

public class GridInput : MonoBehaviour
{
    public bool enableKeyboardControls;
    public Transform grid;
    public float rotationSpeed;
    public float leanSpeed;
    public float maximumLean;

    private bool fixRotation;
    private float lerpTime;
    private Quaternion startRotation;
    private HorizontalDirections horizontalDirection = HorizontalDirections.None;
    private VerticalDirections verticalDirection = VerticalDirections.None;

    public void SetDirection(HorizontalDirections rotateDirection)
    {
        horizontalDirection = rotateDirection;
    }

    public void SetDirection(VerticalDirections rotateDirection)
    {
        verticalDirection = rotateDirection;
    }

    private void Update()
    {
        if (enableKeyboardControls)
        {
            CheckInput();
        }

        RotateGrid();

        if (fixRotation)
        {
            lerpTime += Time.deltaTime * leanSpeed * 4;
            grid.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(Vector3.zero), lerpTime);
        }
    }

    private void RotateGrid()
    {
        switch (horizontalDirection)
        {
            case HorizontalDirections.PositiveX:
                fixRotation = false;
                LeanTowardsX(-1);
                break;
            case HorizontalDirections.NegativeX:
                fixRotation = false;
                LeanTowardsX(1);
                break;
            case HorizontalDirections.None:
                if (!fixRotation)
                {
                    FixRotation();
                }
                break;
        }

        switch (verticalDirection)
        {
            case VerticalDirections.PositiveZ:
                fixRotation = false;
                LeanTowardsZ(-1);
                break;
            case VerticalDirections.NegativeZ:
                fixRotation = false;
                LeanTowardsZ(1);
                break;
            case VerticalDirections.None:
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

    private void CheckInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            SetDirection(HorizontalDirections.NegativeX);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            SetDirection(HorizontalDirections.PositiveX);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            SetDirection(VerticalDirections.NegativeZ);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            SetDirection(VerticalDirections.PositiveZ);
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            SetDirection(HorizontalDirections.None);
            SetDirection(VerticalDirections.None);
        }
    }
}
