using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;

    private static Color currentColor;

    private MeshRenderer meshRenderer;
    private SpriteRenderer splash;

    private float height = 0.58f, speed = 6f, lerpAmount;

    private bool move, isRising, gameOver, displayed;

    public bool perfectStar;

    private AudioSource failSound, hitSound, LevelCompleteSound;

    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float value)
    {
        this.speed = value;
    }

    private void Awake()
    {
        failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        LevelCompleteSound = GameObject.Find("LevelCompleteSound").GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        splash = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        move = false;
        SetColor(GameController.instance.hitColor);
    }

    void Update()
    {
        if (Touch.IsPressing() && !gameOver) { 
            move = true;
            GetComponent<SphereCollider>().enabled = true;
        }
        if (move)
            Ball.z += speed * 0.025f;

        transform.position = new Vector3(0, height, Ball.z);

        displayed = false;
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
            if(perfectStar && !displayed)
            {
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay") as GameObject, transform.position, Quaternion.identity);
                pointDisplay.GetComponent<PointDisplay>().SetText("PERFECT +" + PlayerPrefs.GetInt("Level") * 2);
            }
            else if(!perfectStar && !displayed)
            {
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay") as GameObject, transform.position, Quaternion.identity);
                pointDisplay.GetComponent<PointDisplay>().SetText("+" + PlayerPrefs.GetInt("Level"));
            }
            hitSound.Play();
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
        if(other.tag == "Star")
        {
            perfectStar = true;
        }
    }
    IEnumerator PlayNewLevel()
    {
        LevelCompleteSound.Play();
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        move = false;
        Camera.main.GetComponent<CameraFollow>().Flash();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        Ball.z = 0;
        GameController.instance.GenerateLevel();
    }

    IEnumerator GameOver()
    {
        failSound.Play();
        gameOver = true;
        splash.color = currentColor;
        splash.transform.position = new Vector3(0, 0.7f, Ball.z - 0.05f);
        splash.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        splash.enabled = true;

        meshRenderer.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        move = false;
        yield return new WaitForSeconds(1.5f);
        Camera.main.GetComponent<CameraFollow>().Flash();
        gameOver = false;
        z = 0;
        GameController.instance.GenerateLevel();
        splash.enabled = false;
        meshRenderer.enabled = true;
    }
}
