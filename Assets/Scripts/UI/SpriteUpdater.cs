using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdater : MonoBehaviour
{
    public LayerMask mask;
    public Sprite ground1;
    public Sprite ground2;
    public Sprite air;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, mask))
        {
            if (transform.position.x % 1.0f < 0.5f)
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
