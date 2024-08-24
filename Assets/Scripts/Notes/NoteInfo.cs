using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInfo : MonoBehaviour
{
    public int line = -1;
    public float length;
    public int measure = -1;
    public char articulation = '_';
    public float x_pos;
    public int id;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateX_Pos()
    {
        x_pos = transform.position.x;
        transform.GetChild(1).transform.GetChild(0).GetComponent<DoArticulation>().initial_x = transform.position.x;
    }

}
