using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_V2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 _movement;
    [HideInInspector] public bool canMove = true;

    public float jumpForce = 6f;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 3f;

    public bool isGrounded = true;


    private bool _receinvintInputs = true;


    private SpriteRenderer sr;

    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //StartCoroutine(Floating());

        

    }

    private bool _movingUp;
    
    // Update is called once per frame
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
            moveSpeed = 5f;
            _receinvintInputs = true;
        }


        //---- Gère le flottement -----//

        if(col.offset.y <= -0.3f)
        {
            _movingUp = true;
        }

        if(col.offset.y >= 0f)
        {
            _movingUp = false;
        }

        if(_movingUp)
        {
            col.offset = new Vector2(0, col.offset.y + 0.005f);
        }
        else
        {
            col.offset = new Vector2(0, col.offset.y - 0.005f);
        }
        


        //-----------------------------//












        _movement.x = Input.GetAxis("Horizontal");

        if(_movement.x < -0.1)
        {
            sr.flipX = true;
        }

        if (_movement.x > +0.1)
        {
           sr.flipX = false;
        }

        if (isGrounded == true && _receinvintInputs == true)
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
    }

    void FixedUpdate()
    {
        transform.position = new Vector2((transform.position.x + _movement.x * moveSpeed * Time.fixedDeltaTime), transform.position.y);

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
