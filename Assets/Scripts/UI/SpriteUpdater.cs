using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdater : MonoBehaviour
{
    public LayerMask mask;
    public Sprite ground1;
    public Sprite ground2;
    public Sprite air;
    public LayerMask JumpMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PerformControls.currMode == 'c' || GameObject.Find("Player").GetComponent<PerformControls>().cutSceneDeactivation == true)
        {
            return;
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x - 0.2f, transform.position.y), Vector2.down, 0.55f, JumpMask) || Physics2D.Raycast(new Vector2(transform.position.x + 0.2f, transform.position.y), Vector2.down, 0.55f, JumpMask))
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                GetComponent<SpriteRenderer>().sprite = ground1;
                return;
            }
            if (Mathf.Abs(transform.position.x) % 1.0f < 0.5f)
            {
                GetComponent<SpriteRenderer>().sprite = ground1;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = ground2;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = air;
        }
    }
}
