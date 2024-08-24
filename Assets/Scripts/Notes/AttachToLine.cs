using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToLine : MonoBehaviour
{
    private float y;
    public bool locked = false;
    private NoteStateController st;
    public int currLine = -1;
    public GameObject mouse;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<LineEvent>(HandleLineEvent);
        st = GetComponent<NoteStateController>();
    }

    void Update()
    {
        if (st.state == 0 || st.state == 3)
        {
            return;
        }
        if (st.state == 2)
        {
            if (mouse.transform.position.y > 3.0f || mouse.transform.position.y < -3.0f)
            {
                currLine = -1;
                locked = false;
                st.UpdateState(1);
                return;
            }
        }
        if (locked)
        {
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    void HandleLineEvent(LineEvent e)
    {
        if (st.state == 0 || st.state == 3)
        {
            return;
        }
        if (st.state == 2)
        {
            if (mouse.transform.position.y > 3.0f || mouse.transform.position.y < -3.0f)
            {
                currLine = -1;
                locked = false;
                st.UpdateState(1);
                return;
            }
            else
            {
                currLine = e.line;
            }
        }
        if (e.line == 0 || e.line == 12)
        {
            locked = false;
            st.UpdateState(1);
        }
        else if (e.line != -1)
        {
            locked = true;
            y = e.height + 0.5f;
            currLine = e.line;
            st.UpdateState(2);
        }
        //stem should go up
        if (e.line >= 7)
        {
            transform.GetChild(0).localPosition = new Vector3(1.42999995f, 1.71000004f, 0);
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;

            transform.GetChild(3).localPosition = new Vector3(-1.55f, -2.12f, 0);
            transform.GetChild(4).localPosition = new Vector3(-1.55f, -2.12f, 0);
        }
        //stem should go down
        else if (e.line >= 0)
        {
            transform.GetChild(0).localPosition = new Vector3(-1.59000003f, -3.70000005f, 0);
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;

            transform.GetChild(3).localPosition = new Vector3(-1.55f, 0.43f, 0);
            transform.GetChild(4).localPosition = new Vector3(-1.55f, 0.43f, 0);
        }

    }

}
public class LineEvent
{
    public int line;
    public float height;

    public LineEvent(int line, float height)
    {
        this.line = line;
        this.height = height;
    }
}
