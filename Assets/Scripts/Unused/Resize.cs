using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    private float startPoint = 0.209999993f;
    public GameObject mouse;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3((mouse.transform.position.x - startPoint) / 15.38f, transform.localScale.y, transform.localScale.z);
    }
}
