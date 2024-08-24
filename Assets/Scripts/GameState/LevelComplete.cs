using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public int level;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventBus.Publish(new LevelCompleteEvent(level));
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

public class LevelCompleteEvent
{
    public int level;

    public LevelCompleteEvent(int level)
    {
        this.level = level;
    }
}
