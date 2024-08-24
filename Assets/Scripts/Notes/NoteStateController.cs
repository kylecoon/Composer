using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteStateController : MonoBehaviour
{
    public int state; //0 = on page, 1 = in hand off staff, 2 = in hand on staff, 3 = on staff
    public bool canBePlaced;
    public int inNoteShield = 0;
    private BoxCollider2D box;
    private GameObject ns;
    private Vector3 initialPosition;
    private NoteInfo note;
    public char currArticulation = '_';
    public GameObject connectedNote;
    public GameObject TiePrefab;
    private GameObject Tie;
    private ComposerControls comp;
    private GameObject mouse;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        canBePlaced = true;
        box = transform.GetChild(1).GetChild(0).GetComponent<BoxCollider2D>();
        ns = transform.GetChild(2).gameObject;
        initialPosition = transform.position;
        EventBus.Subscribe<ButtonEvent>(HandleButtonEvent);
        note = GetComponent<NoteInfo>();
        mouse = GameObject.Find("Mouse");
        if (mouse != null)
        {
            comp = mouse.GetComponent<ComposerControls>();
        }
    }
    public void UpdateState(int s)
    {
        state = s;
        if (transform.childCount == 6)
        {
            transform.GetChild(5).gameObject.SetActive(false);
        }
        //on page
        if (state == 0)
        {
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = 0;
            ns.SetActive(false);
            transform.parent = null;
            UpdateColor('g');
            box.isTrigger = true;
            note.line = -1;
        }
        //in hand off staff
        else if (state == 1)
        {
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = 0;
            note.line = -1;
            ns.SetActive(true);
            canBePlaced = true;
            transform.localPosition = new Vector2(transform.localPosition.x, 0.5f);
            UpdateColor('g');
            box.isTrigger = true;
        }
        //in hand on staff
        else if (state == 2)
        {
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = 0;
            ns.SetActive(true);
            box.isTrigger = true;
            note.line = -1;
            if (inNoteShield != 0)
            {
                UpdateColor('r');
                canBePlaced = false;
            }
            else
            {
                UpdateColor('g');
                canBePlaced = true;
            }
        }
        //on staff
        else if (state == 3)
        {
            ns.SetActive(true);
            transform.parent = null;
            UpdateColor('b');
            box.isTrigger = false;
            note.line = GetComponent<AttachToLine>().currLine;
            note.UpdateX_Pos();
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = 7;
        }
    }
    public void UpdateColor(char c)
    {
        if (c == 'r')
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.7830189f, 0.1662069f, 0.1662069f, 0.8f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.7830189f, 0.1662069f, 0.1662069f, 0.8f);
            transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(0.7830189f, 0.1662069f, 0.1662069f, 0.8f);
            transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.7830189f, 0.1662069f, 0.1662069f, 0.8f);
        }
        else if (c == 'g')
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 0.8f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 0.8f);
            transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 0.8f);
            transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 0.8f);
        }
        else if (c == 'b')
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 1.0f);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 1.0f);
            transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 1.0f);
            transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.1254902f, 0.1254902f, 1.0f);
        }
    }

    private void HandleButtonEvent(ButtonEvent e)
    {
        if (e.mode == 'r')
        {
            ResetNote();
        }
    }

    public void ResetNote()
    {
        state = 0;
        ns.SetActive(false);
        transform.parent = null;
        transform.localPosition = new(0, 0);
        box.isTrigger = true;
        inNoteShield = 0;
        GetComponent<AttachToLine>().currLine = -1;
        GetComponent<AttachToLine>().locked = false;
        transform.position = initialPosition;
        transform.GetChild(0).localPosition = new Vector3(1.42999995f, 1.71000004f, 0);
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
        currArticulation = '_';
        note.articulation = '_';
        if (connectedNote != null)
        {
            //connectedNote.GetComponent<NoteStateController>().RemoveArticulation();
            connectedNote = null;
        }

        canBePlaced = false;
        UpdateColor('b');

        if (Tie != null)
        {
            Destroy(Tie);
        }
    }

    public void AddArticulation(char c)
    {
        currArticulation = c;
        note.articulation = c;
        RemoveTie();
        if (c == 's')
        {
            transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (c == 'o')
        {
            transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (c == 't')
        {
            transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void RemoveArticulation()
    {
        if (Tie != null)
        {
            Destroy(Tie);
        }
        currArticulation = '_';
        note.articulation = '_';
        if (connectedNote != null)
        {
            connectedNote = null;
        }
        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
    }

    public void RemoveTie()
    {
        if (connectedNote != null)
        {
            connectedNote.GetComponent<NoteStateController>().RemoveArticulation();
            connectedNote.GetComponent<NoteStateController>().connectedNote = null;
            connectedNote = null;
        }
        if (Tie != null)
        {
            Destroy(Tie);
        }
    }

    public IEnumerator adjustTie()
    {
        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetChild(0).gameObject.GetComponent<AudioSource>().clip = transform.GetChild(1).GetChild(0).GetComponent<DoArticulation>().stretch;
        transform.GetChild(1).GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        if (connectedNote != null)
        {
            connectedNote.GetComponent<NoteStateController>().RemoveArticulation();
            connectedNote = null;
            Destroy(Tie);
        }

        currArticulation = 't';
        note.articulation = 't';
        Tie = Instantiate(TiePrefab, new Vector3(transform.position.x - 1.0f, transform.position.y - 1.5f, transform.position.z), Quaternion.identity);
        float starting_x = transform.position.x - 1.0f;
        while (comp.placingTie)
        {
            Tie.transform.localScale = new Vector3((mouse.transform.position.x - starting_x) / 16.38f, transform.localScale.y, transform.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        if (Tie != null)
        {
            Tie.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
