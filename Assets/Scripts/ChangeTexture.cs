using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public Material[] materials;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SetRandomMaterial();
    }

    void SetRandomMaterial()
    {
       int randomIndex = Random.Range(0, materials.Length);
       renderer.material = materials[randomIndex];      
        
    }
}
