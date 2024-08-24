using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidInk : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ink"))
        {
            transform.root.GetChild(4).GetChild(2).GetComponent<PreventNotePlacing>().AddNoteShield();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ink"))
        {
            transform.root.GetChild(4).GetChild(2).GetComponent<PreventNotePlacing>().RemoveNoteShield();
        }
    }
}
