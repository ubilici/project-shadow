using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NodeType lastNode;
    public float lifetimeOnLight;

    private bool lightCountdownStarted;
    private float lightCountdown;
    private bool levelOver;

    public void SetLastNode(NodeType nodeType)
    {
        lastNode = nodeType;
    }

    private void Update()
    {
        if (!lightCountdownStarted)
        {
            if (lastNode == NodeType.Light)
            {
                lightCountdown = 0;
                lightCountdownStarted = true;
            }
        }

        if (lightCountdownStarted)
        {
            lightCountdown += Time.deltaTime;

            if(lastNode == NodeType.Shadow)
            {
                lightCountdown = 0;
                lightCountdownStarted = false;
            }
        }

        if (!levelOver)
        {
            if (lightCountdown >= lifetimeOnLight)
            {
                Debug.Log("Game Over");
                levelOver = true;
            }

            if (lastNode == NodeType.Finish)
            {
                Debug.Log("You Win");
                levelOver = true;
            }
        }

    }



}
