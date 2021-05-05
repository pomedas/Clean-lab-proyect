using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVRotatonScript : MonoBehaviour
{
    /// <summary>
    /// reference to the mesh renderer component of the object
    /// </summary>
    private MeshRenderer r;

    /// <summary>
    /// speed with which uv coordinates will move
    /// </summary>
    public float speed;

    /// <summary>
    /// get reference to the mesh renderer
    /// </summary>
    void Start()
    {
        r = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// update the position in the uv coordinates of the texture to make the animation on the object
    /// </summary>
    void Update()
    {
        r.material.mainTextureOffset += new Vector2(Random.Range(0,1.0f), Random.Range(0,1.0f)) * speed;
    }
}
