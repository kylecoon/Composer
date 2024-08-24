using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureConstraints : MonoBehaviour
{
    public float requiredLength;
    public float currLength = 0;
    public int measureNumber;
    public Dictionary<int, NoteInfo> notes = new Dictionary<int, NoteInfo>();
    public GameObject designatedTimeSigntaure;

    void Start()
    {
        EventBus.Subscribe<ButtonEvent>(ResetMeasure);
    }

    public bool CheckNewNote(float f)
    {
        if (GameObject.Find("Time Signature").GetComponent<ValidityChecker>().noTimeSignature)
        {
            return true;
        }
        if (currLength + f > requiredLength)
        {
            designatedTimeSigntaure.GetComponent<InvalidMeasure>().FlashRed();
            return false;
        }
        return true;
    }

    public void AddNote(NoteInfo n)
    {
        notes.Add(n.id, n);
        n.measure = measureNumber;
        currLength += n.length;
    }

    public void RemoveNote(NoteInfo n)
    {
        notes.Remove(n.id);
        n.measure = -1;
        currLength -= n.length;
    }

    public List<NoteInfo> GetNotes()
    {
        List<NoteInfo> orderedNotes = new List<NoteInfo>();
        foreach (KeyValuePair<int, NoteInfo> entry in notes)
        {
            orderedNotes.Add(entry.Value);
        }
        orderedNotes.Sort((note1, note2) => note1.x_pos.CompareTo(note2.x_pos));
        return orderedNotes;
    }

    public void FlashRed()
    {

    }
    private void ResetMeasure(ButtonEvent e)
    {
        if (e.mode == 'r')
        {
            currLength = 0;
            notes.Clear();
        }
        if (e.mode == 'p')
        {
            List<NoteInfo> orderedNotes = GetNotes();
            foreach (NoteInfo n in orderedNotes)
            {
                Debug.Log(n.length);
            }
        }
    }
}

