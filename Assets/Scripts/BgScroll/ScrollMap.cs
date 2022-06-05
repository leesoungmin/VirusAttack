using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMap : MonoBehaviour
{
    public float scrollSpeed;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!L.ingameManager.isPlaying)
            meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, scrollSpeed * Time.time));
    }
}
