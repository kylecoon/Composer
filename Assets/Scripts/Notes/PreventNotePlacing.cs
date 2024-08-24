using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventNotePlacing : MonoBehaviour
{
    private NoteStateController st;
    // Start is called before the first frame update
    void Start()
    {
        st = transform.parent.GetComponent<NoteStateController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("noteshield"))
        {
            AddNoteShield();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("noteshield"))
        {
            RemoveNoteShield();
        }
    }

    public void AddNoteShield()
    {
        st.inNoteShield += 1;
        if (st.state == 2)
        {
            st.canBePlaced = false;
            st.UpdateColor('r');
        }
    }
    public void RemoveNoteShield()
    {
        st.inNoteShield -= 1;
        if (st.canBePlaced == false && st.inNoteShield == 0)
        {
            st.canBePlaced = true;
            st.UpdateColor('g');
        }
    }
}
