using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float scrollX = 0.001f;

    void Update()
    {
        float OffsetX = Time.time * scrollX;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, 0);
    }
}
