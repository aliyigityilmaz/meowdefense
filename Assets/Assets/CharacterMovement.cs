using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject player;
    private bool isEnergetic;

    public float speed = 5.0f;
    public CharacterController controller;

    private float gravity = -9.8f;
    Vector3 velocity;
    float jumpForce = 1.0f;


    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;
    [SerializeField]
    public int dashCount;
    private int dashCountMax = 3;

    [Header("Sesler")]
    public AudioSource enerj;
    public AudioSource dashSound;
    // Start is called before the first frame update
    void Start()
    {
        isEnergetic = true;
        dashCount = dashCountMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<CharacterEnergy>().currentEnergy < player.GetComponent<CharacterEnergy>().minEnergy)
        {
            isEnergetic = false;
        }
        else
        {
            isEnergetic = true;
        }
        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (controller.isGrounded && velocity.y < 0 )
        {
            velocity.y = -3f;
        }

        Vector3 move = transform.right * horizontal * speed * Time.deltaTime + transform.forward * vertical * speed * Time.deltaTime;

        controller.Move(move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * (-2.0f * gravity));
        }

        if (Input.GetKey(KeyCode.LeftShift) && controller.isGrounded && isEnergetic)
        {
            speed = 6f;
        }
        else if (GetComponent<CharacterEnergy>().currentEnergy <= 10)
        {
            speed = 1f;
        }
        else
        {
            speed = 3f;
        }


        if (Input.GetKeyDown(KeyCode.Q) && dashCount > 0 && isEnergetic)
        {
            Dash();
            dashCount--;
            if (dashCount < dashCountMax)
            {
                StartCoroutine(DashCooldown());
            }
        }
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(3.0f);
        dashCount++;
    }

    public void Dash()
    {
        StartCoroutine(DashMove());
    }

    IEnumerator DashMove()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            dashSound.Play();
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mama")
        {
           enerj.Play();
        }
    }
}
