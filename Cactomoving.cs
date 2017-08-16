using System.Collections;

using UnityEngine;

public class Cactomoving : MonoBehaviour
{
    public float maxSpeed = 10f;
    private Rigidbody2D rb2D;
    // private Animator anim;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    private bool facingRight = true;
    private bool grounded = false;
    public float jumpForce = 700f;
    private float groundRadius = 0.2f;

    void Start()
    {
        // anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        // anim.SetBool("Ground",grounded);
        // anim.SetFloat("vSpeed",rb2D.velocity.y);
        float move = Input.GetAxis("Horizontal");
        // anim.SetFloat("Speed",Mathf.Abs(move));
        rb2D.velocity = new Vector2(move, rb2D.velocity.y);
        /* No need to smooth with Time.deltatime in fixedupdate - only in Update. */
        if(move>0&&!facingRight)
        {
            Flip();
        }
        else if(move<0&&facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Update()
    {
        if(grounded&&Input.GetKeyDown(KeyCode.Space))
        {
            // anim.SetBool("Ground",false);
            rb2D.AddForce(new Vector2(0, jumpForce));
        }
    }
}