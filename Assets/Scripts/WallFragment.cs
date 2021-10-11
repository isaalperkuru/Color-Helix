using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        if(this.gameObject.tag == "Hit")
        {
            meshRenderer.material.color = GameController.instance.hitColor;
        }
        else
        {
            meshRenderer.material.color = GameController.instance.failColor;
        }
    }

    void Update()
    {
        
    }
}
