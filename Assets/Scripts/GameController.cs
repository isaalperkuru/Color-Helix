using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject finishLine;

    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    private GameObject[] walls2;

    private int wallsSpawnNumber = 11;
    private float z = 7;

    private bool colorBump;
    void Awake()
    {
        instance = this;
        GenerateColors();
    }

    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        wallsSpawnNumber = 12;
        z = 7;

        DeleteWalls();

        colorBump = false;
        SpawnWalls();
    }

    void GenerateColors()
    {
        hitColor = colors[Random.Range(0, colors.Length)];
        failColor = colors[Random.Range(0, colors.Length)];
        while(failColor == hitColor)
            failColor = colors[Random.Range(0, colors.Length)];

        Ball.SetColor(hitColor);
    }

    void DeleteWalls()
    {
        walls2 = GameObject.FindGameObjectsWithTag("Fail");

        if(walls2.Length > 1)
        {
            for (int i = 0; i < walls2.Length; i++)
            {
                if(walls2[i].transform.parent.parent.parent.gameObject != null &&
                    walls2[i].transform.parent.parent.parent.gameObject.name != "Helix")
                    Destroy(walls2[i].transform.parent.parent.parent.gameObject);
                else
                    Destroy(walls2[i].transform.parent.parent.gameObject);
            }
        }

        Destroy(GameObject.FindGameObjectWithTag("Bump"));
    }

    void SpawnWalls()
    {
        for (int i = 0; i < wallsSpawnNumber; i++)
        {
            GameObject wall;

            if(Random.value <= 0.2 && !colorBump)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("Color Bump") as GameObject, transform.position, Quaternion.identity);
            }
            else if (Random.value <= 0.2)
            {
                wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
            }
            else if (i >= wallsSpawnNumber - 1 && !colorBump)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("Color Bump") as GameObject, transform.position, Quaternion.identity);
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }

            wall.transform.SetParent(GameObject.Find("Helix").transform);
            wall.transform.localPosition = new Vector3(0, 0, z);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
            z += 7;

            if (i <= wallsSpawnNumber)
                finishLine.transform.position = new Vector3(0, 0.03f, z*2);
        }
    }
}
