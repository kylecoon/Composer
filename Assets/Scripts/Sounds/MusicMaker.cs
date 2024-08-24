using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicMaker : MonoBehaviour
{
    private Dictionary<int, char> lineToNote = new Dictionary<int, char>();
    private bool nextIsTie = false;
    void Start()
    {
        lineToNote.Add(11, 'D');
        lineToNote.Add(10, 'E');
        lineToNote.Add(9, 'F');
        lineToNote.Add(8, 'G');
        lineToNote.Add(7, 'A');
        lineToNote.Add(6, 'B');
        lineToNote.Add(5, 'C');
        lineToNote.Add(4, 'H');
        lineToNote.Add(3, 'I');
        lineToNote.Add(2, 'J');
        lineToNote.Add(1, 'K');

    }
    public string MakeMusic()
    {
        string music = "";
        GameObject[] measures = GameObject.FindGameObjectsWithTag("measure");
        measures = measures.OrderBy(measure => measure.GetComponent<MeasureConstraints>().measureNumber).ToArray();
        foreach (GameObject measure in measures)
        {

            List<NoteInfo> notes = measure.GetComponent<MeasureConstraints>().GetNotes();
            foreach (NoteInfo note in notes)
            {
                if (nextIsTie)
                {
                    music += char.ToLower(lineToNote[note.line]);
                }
                else
                {
                    music += lineToNote[note.line];
                }
                for (int i = 0; i < (note.length / 0.5f) - 1; ++i)
                {
                    if (note.articulation != 's')
                    {
                        music += Char.ToLower(lineToNote[note.line]);
                    }
                    else
                    {
                        music += '.';
                    }

                }
                if (note.articulation == 't')
                {
                    nextIsTie = !nextIsTie;
                }
            }
        }
        Debug.Log(music);
        return music;
    }
}
