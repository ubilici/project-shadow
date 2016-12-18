using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ShadowEffect : MonoBehaviour
{
    public RenderTexture previousFrameBuffer;
    public Material shadowMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height, 0);

        shadowMaterial.SetTexture("_PrevFrame", previousFrameBuffer);
        Graphics.Blit(src, temp, shadowMaterial);
        Graphics.Blit(temp, previousFrameBuffer);
        Graphics.Blit(temp, dst);

        RenderTexture.ReleaseTemporary(temp);
    }
}