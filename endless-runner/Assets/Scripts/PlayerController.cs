using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    [SerializeField] private float jumpForce = 8.0f;
    private bool canJump;
    private bool canDown = true;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float timer = 0f;
    private bool hasExecuted = false;
    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.y < startTouchPosition.y)
            {
                audioSource.Play();
                hasExecuted = true;
                timer = 0f;
            }
            else
            {
                if (canJump)
                {
                    audioSource.Play();
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }

        if (hasExecuted)
        {
            downSizing();
        }
    }

    void downSizing()
    {
        timer += Time.deltaTime;
        gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
        canJump = false;

        if (timer >= 0.8f)
        {
            timer = 0f;
            canJump = true;
            hasExecuted = false;
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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