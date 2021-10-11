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

    private int wallsSpawnNumber = 11;
    private float z = 7;

    void Awake()
    {
        instance = this;
        GenerateColors();
    }

    void Start()
    {
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

    void SpawnWalls()
    {
        for (int i = 0; i < wallsSpawnNumber; i++)
        {
            GameObject wall;
            wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);

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
