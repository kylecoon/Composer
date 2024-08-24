using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCommands : MonoBehaviour
{
    public GameObject ResetButton;
    public GameObject PerformButton;
    public GameObject RestartButton;
    public GameObject ComposeButton;
    public GameObject ArticulationMenu;
    private bool coolingDown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset()
    {
        if (coolingDown)
        {
            return;
        }
        EventBus.Publish(new ButtonEvent('r'));
        StartCoroutine(Cooldown());
    }
    public void Perform()
    {
        if (coolingDown)
        {
            return;
        }

        if (GameObject.Find("Time Signature").GetComponent<ValidityChecker>().CheckValidity() == false)
        {
            Debug.Log("WRONG-O!!!");
            return;
        }

        EventBus.Publish(new ButtonEvent('p'));

        RestartButton.SetActive(true);
        ComposeButton.SetActive(true);

        ResetButton.SetActive(false);
        PerformButton.SetActive(false);
        if (ArticulationMenu != null)
        {
            ArticulationMenu.SetActive(false);
        }
        StartCoroutine(Cooldown());
    }
    public void Restart()
    {
        if (coolingDown)
        {
            return;
        }

        EventBus.Publish(new ButtonEvent('t'));

        StartCoroutine(Cooldown());
    }
    public void Compose()
    {
        if (coolingDown)
        {
            return;
        }

        Debug.Log("compose!");

        EventBus.Publish(new ButtonEvent('c'));

        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
        if (ArticulationMenu != null)
        {
            ArticulationMenu.SetActive(true);
        }


        RestartButton.SetActive(false);
        ComposeButton.SetActive(false);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(0.5f);
        coolingDown = false;
    }
}

public class ButtonEvent
{
    public char mode;

    public ButtonEvent(char c)
    {
        this.mode = c;
    }
}
