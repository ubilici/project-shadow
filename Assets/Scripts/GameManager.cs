using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NodeType lastNode;
    public float lifetimeOnLight;
    public Material shadowMaterial;

    private bool lightCountdownStarted;
    private float lightCountdown;
    private bool levelOver;
    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

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
                sceneLoader.ReloadLevel();
                levelOver = true;
            }

            if (lastNode == NodeType.Finish)
            {
                sceneLoader.LoadNextLevel();
                levelOver = true;
            }
        }

        SetShadowMag();
    }

    private void SetShadowMag()
    {
        shadowMaterial.SetFloat("_InkBleedMag", Mathf.Lerp(0.001f, 0.01f, lightCountdown / lifetimeOnLight));
    }
}
