using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PerformControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private float speed = 6.5f;
    public LayerMask JumpMask;
    public LayerMask MoveMask;
    [SerializeField] private Vector3 startingPosition;
    public static char currMode = 'c';
    public char startMode;
    public bool cutSceneDeactivation = false;
    [SerializeField] public static bool onGround = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool triedJump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        EventBus.Subscribe<ButtonEvent>(UpdateMode);
        currMode = startMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (currMode == 'p')
        {
            GetInput();
            CheckGround();
        }
    }

    private void GetInput()
    {
        if (cutSceneDeactivation)
        {
            return;
        }
        Vector2 direction = new(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if ((direction.x > 0 && AgainstWall(direction.x)) || (direction.x < 0 && AgainstWall(direction.x)))
        {
            direction.x = 0;
        }
        rb.velocity = direction;
        if (Input.GetKeyDown(KeyCode.Space) && triedJump == false)
        {
            StartCoroutine(TriedJump());
        }
    }

    private bool AgainstWall(float x_dir)
    {
        if (x_dir > 0)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, 0.3f, MoveMask) ||
                Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.3f, MoveMask) ||
                Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Vector2.right, 0.3f, MoveMask))
            {
                return true;
            }
        }
        else if (x_dir < 0)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.left, 0.3f, MoveMask) ||
                Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 0.3f, MoveMask) ||
                Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Vector2.left, 0.3f, MoveMask))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateMode(ButtonEvent e)
    {
        if (e.mode == 'p')
        {
            currMode = 'p';
        }
        else if (e.mode == 'c')
        {
            currMode = 'c';
            transform.position = startingPosition;
            rb.velocity = Vector3.zero;
        }
        else if (e.mode == 't')
        {
            transform.parent = null;
            transform.position = startingPosition;
            rb.velocity = Vector3.zero;
            StartCoroutine(FreezePlayer(0.4f));
            StartCoroutine(Camera.main.GetComponent<CameraOperator>().fromTheTop());
        }
    }
    private IEnumerator FreezePlayer(float f)
    {
        char previousMode = currMode;
        currMode = '_';
        yield return new WaitForSeconds(f);
        currMode = previousMode;
    }
    private IEnumerator TriedJump()
    {
        triedJump = true;
        float initialTime = Time.time;
        while (Time.time - initialTime < 0.19f)
        {
            if (onGround && canJump)
            {
                canJump = false;
                StartCoroutine(DoJump());

            }
            yield return new WaitForEndOfFrame();
        }
        triedJump = false;
    }
    private IEnumerator DoJump()
    {
        Debug.Log("yippee!");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 675);
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }

    private void CheckGround()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - 0.2f, transform.position.y), Vector2.down, 0.55f, JumpMask) || Physics2D.Raycast(new Vector2(transform.position.x + 0.2f, transform.position.y), Vector2.down, 0.55f, JumpMask))
        {
            onGround = true;
        }
        else
        {
            if (onGround)
            {
                StartCoroutine(CoyoteTime());
            }
        }
    }

    private IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(0.12f);
        onGround = false;
    }
}
