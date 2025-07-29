using UnityEngine;

public class DASH : MonoBehaviour
{
    private float Speed = 5f;
    private float JumpSpeed = 10f;
    private Rigidbody2D RB;
    private bool isGround = false;
    private float move;
    private float Dashforce = 20f;
    private float Dashtime = 0.2f;
    private float Dashcooldown = 0.5f;
    private float Dashtimeleft;
    private float LastDashtime;
    private bool isDashing;
    private int Jump;
    private int MaxJump = 2 ;
   
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Jump = MaxJump;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        if (!isDashing)
        {
            RB.linearVelocity = new Vector2(move * Speed, RB.linearVelocity.y);

        }

        if (isGround == true || Jump > 0)
            if (Input.GetButtonDown("Jump") && !isDashing)
            {
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, JumpSpeed);
                Jump--;

        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= LastDashtime + Dashcooldown)
            {
                StartDash(move);
            }

        if (isDashing)
        {
            if (Dashtimeleft > 0)
            {
                Dashtimeleft -= Time.deltaTime;
            }

            else
            {
                EndDash();
            }
        }                   

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
            Jump = MaxJump;



        }
        
         if (collision.gameObject.CompareTag("Enemy"))
            {
                Camera.main.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.2f);
            }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") )
        {
            isGround = false;
        }
    }

    void StartDash(float direction)
    {
        if (direction == 0)
        {
            direction = transform.localScale.x;
        }
        isDashing = true;
        Dashtimeleft = Dashtime;
        LastDashtime = Time.time;
        RB.linearVelocity = new Vector2(Mathf.Sign(direction) * Dashforce, 0f);
    }

    void EndDash()
    {
        isDashing = false;
    }
}
