using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class PongBall : MonoBehaviour
{

    public Text countText;
    public Text countText2;
    public int xDirection;

    private TrailRenderer myTrail;
    private Material myColor;
    private Rigidbody rb;
    private int count;
    private int count2;
    private float speedInXDirection;

    public float speedB;
    public float speedM;
    public float speedR;
    public AudioClip ton;

    void Start()
    {
        StartCoroutine(Pause());

        myTrail = GetComponent<TrailRenderer>();
        count = 0;
        SetCountText();
        count2 = 0;
        SetCountText2();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, 0f);
        speedB = 5f;
        speedM = 0f;
        myTrail.enabled = false;
        transform.GetComponent<Renderer>().material.color = Color.white;
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = ton;
    }

    void SetCountText()
    {
        countText.text = count.ToString();
    }
    void SetCountText2()
    {
        countText2.text = count2.ToString();
    }

    void Update()
    {
        Winner();

        if (transform.position.x < -59.4f)
        {
            StartCoroutine(Pause());
        }
        if (transform.position.x > 59.5f)
        {
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {

        transform.GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(1f);
        LaunchBall();
    }

    void LaunchBall()
    {
        speedM = 0f;
        myTrail.enabled = false;

        transform.position = Vector3.zero;

        Vector3 launchDirection = new Vector3();

        rb.velocity = new Vector3(0f, 0f, 0f);

        xDirection = Random.Range(0, 2);

        speedB = 5f;

        if (xDirection == 0)
        {
            launchDirection.x = -5f;
        }
        if (xDirection == 1)
        {
            launchDirection.x = 5f;
        }
        int zDirection = Random.Range(0, 3);

        if (zDirection == 0)
        {
            launchDirection.z = -5f;
        }
        if (zDirection == 1)
        {
            launchDirection.z = 5f;
        }
        if (zDirection == 2)
        {
            launchDirection.z = 0f;
        }

        rb.velocity = launchDirection;
    }


    void OnCollisionEnter(Collision hit)
    {
        GetComponent<AudioSource>().Play();

        if (hit.gameObject.name == "TopWall" && speedM<149f)
        {
            speedInXDirection = 0f;

            if (rb.velocity.x > 0f)
                {
                    speedInXDirection = speedB;
                }
                if (rb.velocity.x < 0f)
                {
                    speedInXDirection = -speedB;
                }

                rb.velocity = new Vector3(speedInXDirection, 0f, -speedB);
         
        }
        if (hit.gameObject.name == "TopWall" && speedM > 149f)
        {
            speedInXDirection = 0f;

            if (rb.velocity.x > 0f)
            {
                speedInXDirection = 150f;
            }
            if (rb.velocity.x < 0f)
            {
                speedInXDirection = -150f;
            }

            rb.velocity = new Vector3(speedInXDirection, 0f, -150);

        }
        if (hit.gameObject.name == "BottomWall" && speedM < 149f)
        {
            speedInXDirection = 0f;

            if (rb.velocity.x > 0f)
                speedInXDirection = speedB;

            if (rb.velocity.x < 0f)
                speedInXDirection = -speedB;

            rb.velocity = new Vector3(speedInXDirection, 0f, speedB);

        }
        if (hit.gameObject.name == "BottomWall" && speedM > 149f)
        {
            speedInXDirection = 0f;

            if (rb.velocity.x > 0f)
                speedInXDirection = 150f;

            if (rb.velocity.x < 0f)
                speedInXDirection = -150f;

            rb.velocity = new Vector3(speedInXDirection, 0f, 150f);

        }
        if (hit.gameObject.name == "Player1")
         {
               myTrail.enabled = false;
               transform.GetComponent<Renderer>().material.color = Color.white;

            if (speedB < 150f)
                {
                    speedB = speedB + 5f;
                }

            speedM = 0f;

            rb.velocity = new Vector3(speedB, 0f, 0f);

                if (transform.position.z - hit.gameObject.transform.position.z < -2)

                    rb.velocity = new Vector3(speedB, 0f, -speedB);

                if (transform.position.z - hit.gameObject.transform.position.z > 2)
                {
                    rb.velocity = new Vector3(speedB, 0f, speedB);

                }

            }

        if (hit.gameObject.name == "Player2")
        {
            myTrail.enabled = false;
            transform.GetComponent<Renderer>().material.color = Color.white;

            if (speedB < 150f)
            {
                speedB = speedB + 5f;
            }
            speedM = 0f;

            rb.velocity = new Vector3(-speedB, 0f, 0f);

            if (transform.position.z - hit.gameObject.transform.position.z < -2)
            {
                rb.velocity = new Vector3(-speedB, 0f, -speedB);
            }
            if (transform.position.z - hit.gameObject.transform.position.z > 2)
            {
                rb.velocity = new Vector3(-speedB, 0f, speedB);
            }
        }
        
    }

     void OnTriggerEnter(Collider other)
    {
        MineSpawner mineSpawner = GetComponent<MineSpawner>();

        if (other.gameObject.CompareTag("RightGoal"))
        {
          
            rb.velocity = new Vector3(0f, 0f, 0f);
            count = count + 1;
            SetCountText();
            xDirection = 1;
            myTrail.enabled = false;
        }

        if (other.gameObject.CompareTag("LeftGoal"))
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            count2 = count2 + 1;
            SetCountText2();
            xDirection = 0;
            myTrail.enabled = false;
        }

        if (other.gameObject.CompareTag("MineTag"))
        {
            Renderer rend = GetComponent<Renderer>();
            other.gameObject.SetActive(false);
            CountDown(mineSpawner);
            StartCoroutine(MineHit());
            speedM = 150f;
            myTrail.enabled = !myTrail.enabled;
            transform.GetComponent<Renderer>().material.color = Color.red;

        }
    }
    public void Winner()
    {
        if (count >= 3)
        {
            SceneManager.LoadScene(1);
        }
        if (count2 >= 3)
        {
            SceneManager.LoadScene(2);
        }

    }
    private void CountDown(MineSpawner mineSpawner)
    {
        MineSpawner.instance.MineCounter = MineSpawner.instance.MineCounter - 1;

    }

    IEnumerator MineHit()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        MineBall();
    }
    void MineBall()
    {


        xDirection = Random.Range(0, 2);

        Vector3 mineDirection = new Vector3();

        rb.velocity = new Vector3(0f, 0f, 0f);

        xDirection = Random.Range(0, 2);

        if (xDirection == 0)
        {
            mineDirection.x = -150f;
        }
        if (xDirection == 1)
        {
            mineDirection.x = 150f;
        }
        int zDirection = Random.Range(0, 2);

        if (zDirection == 0)
        {
            mineDirection.z = -150f;
        }
        if (zDirection == 1)
        {
            mineDirection.z = 150f;
        }


        rb.velocity = mineDirection;
    }
}