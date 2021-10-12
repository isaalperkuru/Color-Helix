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
            GameObject colorBumb = GameObject.FindGameObjectWithTag("Bump");
            if(transform.position.z > colorBumb.transform.position.z)
            {
                GameController.instance.hitColor = colorBumb.GetComponent<ColorBump>().GetColor();
            }
            meshRenderer.material.color = GameController.instance.hitColor;
        }
        else
        {
            if (GameController.instance.failColor == GameController.instance.hitColor)
                GameController.instance.failColor = GameController.instance.colors[Random.Range(0, GameController.instance.colors.Length)];
            meshRenderer.material.color = GameController.instance.failColor;
        }
    }

    void Update()
    {
        
    }
}
