using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public Vector3 composePosition = new(0.5f, 1.98000002f, -10f);
    public float composeSize = 8.0f;
    [SerializeField] private GameObject player;
    private float performHeight = 0.0f;
    private float performSize = 4.5f;
    private char currMode = 'c';
    public char startMode;
    public float leftBound;
    public float rightBound;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ButtonEvent>(ChangeMode);
        currMode = startMode;
        if (startMode == 'c')
        {
            Camera.main.orthographicSize = composeSize;
        }
        else if (startMode == 'p')
        {
            Camera.main.orthographicSize = performSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currMode == 'p')
        {
            if (player.transform.position.x > leftBound && player.transform.position.x < rightBound)
            {
                transform.position = new Vector3(player.transform.position.x + 4.0f, performHeight, -10);
            }
        }
    }

    private void ChangeMode(ButtonEvent e)
    {
        if (e.mode == 'c')
        {
            currMode = 'c';
            StartCoroutine(goToCompose());
        }
        else if (e.mode == 'p')
        {
            StartCoroutine(goToPerform());
        }
    }

    private IEnumerator goToCompose()
    {
        Vector2 initialPosition = transform.position;
        for (int i = 0; i < 25; ++i)
        {
            transform.position = Vector3.Lerp(new Vector3(initialPosition.x, initialPosition.y, -10), new Vector3(composePosition.x, composePosition.y, -10), 0.04f * (i + 1));
            Camera.main.orthographicSize = performSize + (((composeSize - performSize) / 25) * (i + 1));
            yield return new WaitForSeconds(0.012f);
        }
    }


    private IEnumerator goToPerform()
    {
        Vector2 initialPosition = transform.position;
        for (int i = 0; i < 25; ++i)
        {
            transform.position = Vector3.Lerp(new Vector3(initialPosition.x, initialPosition.y, -10), new Vector3(player.transform.position.x + 4.0f, performHeight, -10), 0.04f * (i + 1));
            Camera.main.orthographicSize = composeSize + (((performSize - composeSize) / 25) * (i + 1));
            yield return new WaitForSeconds(0.012f);
        }
        currMode = 'p';
    }

    public IEnumerator fromTheTop()
    {
        currMode = '_';
        yield return new WaitForEndOfFrame();
        Vector3 initialPosition = transform.position;
        float player_x = player.transform.position.x;
        for (int i = 0; i < 25; ++i)
        {
            transform.position = Vector3.Lerp(new Vector3(initialPosition.x, performHeight, -10), new Vector3(player_x + 4.0f, performHeight, -10), 0.04f * (i + 1));
            yield return new WaitForSeconds(0.012f);
        }
        currMode = 'p';
    }
}
