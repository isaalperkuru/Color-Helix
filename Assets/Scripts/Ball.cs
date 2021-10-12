using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;

    private static Color currentColor;

    private MeshRenderer meshRenderer;

    private float height = 0.58f, speed = 6f, lerpAmount;

    private bool move, isRising;

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
        if (isRising)
        {
            currentColor = Color.Lerp(meshRenderer.material.color, GameObject.FindGameObjectWithTag("Bump").GetComponent<ColorBump>().GetColor(), 
                lerpAmount);
            lerpAmount += Time.deltaTime;
        }
        if(lerpAmount >= 1)
        {
            isRising = false;
        }
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
            Destroy(other.transform.parent.gameObject);
        }
        if(other.tag == "Bump")
        {
            lerpAmount = 0;
            isRising = true;
        }
        if(other.tag == "Fail")
        {
            StartCoroutine(GameOver());
        }
        if(other.tag == "Finish")
        {
            StartCoroutine(PlayNewLevel());
        }
    }
    IEnumerator PlayNewLevel()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        move = false;
        //Flash
        //level++
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        Ball.z = 0;
        GameController.instance.GenerateLevel();
    }

    IEnumerator GameOver()
    {
        GameController.instance.GenerateLevel();
        Ball.z = 0;
        move = false;
        yield break;
    }
}
