using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D body;
    public Animator animator;
    private Vector2 facingLeft;
    private Vector2 facingRight;
    BoxCollider2D bcol;
    SpriteRenderer spriterender;

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
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if(IsGrounded() && Input.GetKey(KeyCode.Space)){
            body.velocity = new Vector2(body.velocity.x, speed);
        }

          if(Input.GetKey(KeyCode.A)){
            animator.SetBool("running", true);
            transform.localScale = facingLeft;
            Debug.Log("The A key was pressed.");
        }

          if(Input.GetKey(KeyCode.D)){
            animator.SetBool("running", true);
            transform.localScale = facingRight;
            Debug.Log("The D key was pressed.");
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            animator.SetBool("running", false);
        }

        if(IsGrounded()) {
            animator.SetBool("onGround", true);
        } else {
            animator.SetBool("onGround", false);
            animator.SetBool("running", false);
        }
    }

    bool IsGrounded() {
    RaycastHit2D raycastHit = Physics2D.Raycast(bcol.bounds.center, Vector2.down, bcol.bounds.extents.y + 1f, platformLayerMask);
    Debug.DrawRay(bcol.bounds.center, Vector2.down * (bcol.bounds.extents.y + 1f));
    Color rayColor;
    Debug.Log("IsGrounded is running");

        if (raycastHit.collider != null) {
        rayColor = Color.green;
        Debug.Log("IsGrounded green");
        }
        else {
        rayColor = Color.red;
        Debug.Log("IsGrounded red");
        }
        return raycastHit.collider != null;
        }
}
