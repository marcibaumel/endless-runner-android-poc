using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float jumpForce = 8.0f;
    private bool canJump;
    private bool canDown = true;
    public float startTime = 0f;
    public float holdTime = 2.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("It Works Great!");
            startTime = Time.time;
            if (startTime + holdTime >= Time.time)
                Debug.Log(startTime);
                Debug.Log(Time.time);
                Debug.Log("It Works Great!");
                startTime = 0;
        }


        if (Input.GetMouseButtonDown(0) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right mouse click");
            if (canDown)
            {
                canDown = false;
                canJump = false;
                gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            }
            else
            {
                canDown = true;
                canJump = true;
                gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && canDown)
        {
            canJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("S_Game");
        }
    }
}
