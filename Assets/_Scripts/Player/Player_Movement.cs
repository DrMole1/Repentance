using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveSpeedSave;
    public float changedSpeed;
    private Rigidbody2D rb;
    private Vector2 _movement;
    [HideInInspector] public bool canMove = true;
    public BoxCollider2D col;

    public float jumpForce = 6f;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 3f;
    public float MaxSpeed = 15f;

    public bool isGrounded = true;
    private bool _movingUp;
    private bool _receinvintInputs = true;


    public float collectedSoul;
    private float _CoyoteTimeJump = 0.1f;

    public Transform eyes;

    [Header("Concernant Game Over")]
    public GameObject lightLantern;
    public GameObject gameOverMenu;
    public GameObject panel;
    public AudioSource audio;
    public GameObject orbe;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collectedSoul = 0f;

        moveSpeedSave = moveSpeed;
        changedSpeed = moveSpeed;
    }

    float timerSinceGrounded = 0f;

    void Update()
    {

        if (!canMove)
        {
            _receinvintInputs = false;
            moveSpeed = 0f;
            return;
        }
        else
        {
            moveSpeed = changedSpeed;
            _receinvintInputs = true;
        }


        _movement.x = Input.GetAxis("Horizontal");

        //------------- Jump system -------------//
        if (isGrounded == false)
        {
            timerSinceGrounded += Time.deltaTime;

            if(timerSinceGrounded < _CoyoteTimeJump)
            {
                if(Input.GetButtonDown("Jump"))
                {
                    rb.AddForce(Vector2.up * jumpForce * 1.5f, ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            timerSinceGrounded = 0;
        }
        if (isGrounded && _receinvintInputs)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            }
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        //---------------------------------------//

        //---- Gère le flottement -----//

        if (col.offset.y <= -1f)
        {
            _movingUp = true;
        }

        if (col.offset.y >= 0f)
        {
            _movingUp = false;
        }

        if (_movingUp)
        {
            col.offset = new Vector2(0, col.offset.y + 0.030f);
        }
        else
        {
            col.offset = new Vector2(0, col.offset.y - 0.030f);
        }



        //-----------------------------//

        // En cas de défaite/Game Over
        if (lightLantern.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity <= 0f)
        {
            canMove = false;
            gameOverMenu.SetActive(true);
            LeanTween.alpha(panel.GetComponent<RectTransform>(), 0.6f, 0.4f);
            LeanTween.alpha(gameOverMenu.transform.GetChild(0).GetComponent<RectTransform>(), 1f, 0.3f);
            LeanTween.alpha(gameOverMenu.transform.GetChild(3).GetComponent<RectTransform>(), 1f, 0.3f);
            audio.volume = 0f;
            orbe.GetComponent<CircleCollider2D>().enabled = false; // On détruit l'orbe pour que le joueur ne puisse plus attaquer une fois mort
        }
    }

    void FixedUpdate()
    {
        transform.position = new Vector2((transform.position.x + _movement.x * moveSpeed * Time.fixedDeltaTime), transform.position.y);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);


        eyes.position = new Vector2(Mathf.Clamp(transform.position.x + _movement.x * moveSpeed * Time.fixedDeltaTime, transform.position.x - 0.05f, transform.position.x + 0.05f), transform.position.y);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Rondin"))
        {
            isGrounded = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Rondin"))
        {
            isGrounded = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Rondin"))
        {
            isGrounded = false;
        }
    }


    
}
