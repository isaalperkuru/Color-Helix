using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;

    private static Color currentColor;

    private MeshRenderer meshRenderer;

    private float height = 0.58f, speed = 6f;

    private bool move;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        move = false;
    }

    void Update()
    {
        if (Touch.IsPressing())
            move = true;

        if (move)
            Ball.z += speed * 0.025f;

        transform.position = new Vector3(0, height, Ball.z);

        UpdateColor();
    }

    void UpdateColor()
    {
        meshRenderer.sharedMaterial.color = currentColor;
    }

    public static float GetZ()
    {
        return Ball.z;
    }

    public static Color SetColor(Color color)
    {
        return currentColor = color;
    }

    public static Color GetColor()
    {
        return currentColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hit")
        {
            print("We hit wall");
        }
        if(other.tag == "Fail")
        {
            print("GameOver");
        }
        if(other.tag == "Finish")
        {
            print("Finished");
        }
    }
}
