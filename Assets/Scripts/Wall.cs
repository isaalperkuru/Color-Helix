using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject wallFragment;
    private GameObject wall1, wall2;

    private float rotationZ;
    private float rotationZMax = 180;
    void Awake()
    {
        wallFragment = Resources.Load("WallFragment") as GameObject;
    }
    private void Start()
    {
        SpawnWallFragments();
    }
    void SpawnWallFragments()
    {
        wall1 = new GameObject();
        wall2 = new GameObject();

        wall1.name = "Wall1";
        wall2.name = "Wall2";

        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);

        for (int i = 0; i < 100; i++)
        {
            GameObject wallF = Instantiate(wallFragment, transform.position, Quaternion.Euler(0,0,rotationZ));
            rotationZ += 3.6f;

            if (rotationZ <= rotationZMax)
            {
                wallF.transform.SetParent(wall1.transform);
                wallF.gameObject.tag = "Hit";
            }
            else
            {
                wallF.transform.SetParent(wall2.transform);
                wallF.gameObject.tag = "Fail";
            }
        }

        wall1.transform.localPosition = Vector3.zero;
        wall2.transform.localPosition = Vector3.zero;

        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
