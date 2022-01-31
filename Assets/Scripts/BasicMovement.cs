using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D body;
    public Animator animator;
    public Transform feetPos;

    public float jumpFloat;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private Vector2 facingLeft;
    private Vector2 facingRight;
    BoxCollider2D bcol;
    SpriteRenderer spriterender;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool isFalling;

    private bool singleFloatJump;

    private void Awake()
    {
       body = GetComponent<Rigidbody2D>(); 
       facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
       facingRight = new Vector2(transform.localScale.x, transform.localScale.y);
       spriterender = GetComponent<SpriteRenderer>();
       bcol = GetComponent<BoxCollider2D>(); 
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);


        //TESTS

        if(body.velocity.y < 0) {
            isFalling = true;
            isJumping = false;
        }

         if(body.velocity.y > 0) {
            isFalling = false;
        }
        
        if(Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Debug.Log("GROUNDED JUMP");
            isJumping = true;
            singleFloatJump = true;
            jumpTimeCounter = jumpTime;
            body.velocity = new Vector2(body.velocity.x, speed) * jumpForce;
        }

        //GetKey to check if it's continually held
        if(isJumping && !isGrounded) {
            if(jumpTimeCounter > 0) {
                 body.velocity = new Vector2(body.velocity.x, speed) * jumpForce;
                 jumpTimeCounter -= Time.deltaTime;
            } else {
                //this never runs
                Debug.Log("JUMP RAN OUT");
                isJumping = false;
            }
        }

        if (isFalling && singleFloatJump) {

            if (Input.GetKeyDown(KeyCode.Space) && singleFloatJump) {
                Debug.Log("FLOAT");
                GetComponent<Rigidbody2D>().drag = jumpFloat;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                Debug.Log("FALL");
                GetComponent<Rigidbody2D>().drag = 0;
                singleFloatJump = false;
            }
        }

        //Transforms for directional animation

          if(Input.GetKey(KeyCode.A)){
            animator.SetBool("running", true);
            transform.localScale = facingLeft;
        }

          if(Input.GetKey(KeyCode.D)){
            animator.SetBool("running", true);
            transform.localScale = facingRight;
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            animator.SetBool("running", false);
        }

        if(isGrounded) {
            animator.SetBool("onGround", true);
            GetComponent<Rigidbody2D>().drag = 0;
        } else {
            animator.SetBool("onGround", false);
            animator.SetBool("running", false);
        }
    }
}
