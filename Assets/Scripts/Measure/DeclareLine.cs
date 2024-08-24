using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeclareLine : MonoBehaviour
{
    public int line;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("mouse"))
        {
            EventBus.Publish(new LineEvent(line, transform.position.y));
        }
    }
}
