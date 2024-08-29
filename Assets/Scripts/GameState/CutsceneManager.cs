using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public int level;
    public GameObject MaestroText;
    [SerializeField] private int currStep = 0;
    public GameObject player;
    public GameObject mouse;
    public GameObject wasd;
    public GameObject ResetButton;
    public GameObject PerformButton;
    public GameObject artMenu;
    public GameObject legatoButton;
    public GameObject tieButton;
    public AudioClip artic;
    // Start is called before the first frame update
    void Start()
    {
        if (level == 0)
        {
            StartCoroutine(Level0Cutscene());
        }
        else if (level == 1)
        {
            StartCoroutine(Level1Cutscene());
        }
        else if (level == 3)
        {
            StartCoroutine(Level3Cutscene());
        }
        else if (level == 4)
        {
            StartCoroutine(Level4Cutscene());
        }
        else if (level == 9)
        {
            StartCoroutine(Level9Cutscene());
        }
        else if (level == 10)
        {
            StartCoroutine(Level10Cutscene());
        }
        else if (level == 11)
        {
            StartCoroutine(Level11Cutscene());
        }
        else if (level == 13)
        {
            StartCoroutine(Level13Cutscene());
        }
        else if (level == 15)
        {
            StartCoroutine(Level15Cutscene());
        }
        else if (level == 17)
        {
            StartCoroutine(Level17Cutscene());
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private IEnumerator Level0Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Ah, my dear apprentice! It's me, your Maestro!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "It would seem I lost some of my piano keys amongst this sheet music...";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "I've got a performance tonight so be a dear and find them in due haste!";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        wasd.SetActive(true);
        ReactivatePlayerControls();
    }

    private IEnumerator Level1Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Oh dear, it seems as if these notes fell right off the staff...";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Say, how about we put your composition skills to action?";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Use your mouse to place the quarter notes back onto the staff!";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Once you're finished, we can hear your new masterpiece!";
        while (currStep == 3)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        mouse.SetActive(true);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level3Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Oh looky there, a new note type! So far, we've only seen quarter notes!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "This new note is called a half note. When played, it lasts twice as long as a quarter note.";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Place them on the staff and let's see how they all sound together!";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level4Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "AH!!! NUMBERS!!!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "The ever-tricky Time Signature has finally turned up...";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Time signatures indicate how many beats should be in a Measure.";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "\"Measures\" are the space on the staff between two vertical lines.";
        while (currStep == 3)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Hover over the Time Signature to learn what each number indicates.";
        while (currStep == 4)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level9Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Well, would you look at that! A Time Change!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Time Signatures can change at the start of any measures.";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Each measure will adhere to the most recent Time Signature.";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Take a crack at filling this staff out!.";
        while (currStep == 3)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level10Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Uh oh! I hardly believe one note will be enough to get you up to those keys...";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "We'll need to use something a bit more sophistiated!";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        artMenu.SetActive(true);
        GameObject.Find("Mouse").GetComponent<AudioSource>().clip = artic;
        GameObject.Find("Mouse").GetComponent<AudioSource>().Play();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Select an articulation from the menu on the right to give this note some pizazz!";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level11Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "If you ever wish to remove an articulation, simply right-click the note it is on!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }
    private IEnumerator Level13Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "I'm not sure even our springiest of staccatos can get us across this measure...";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Let's try something new! Introducing...";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("Mouse").GetComponent<AudioSource>().clip = artic;
        GameObject.Find("Mouse").GetComponent<AudioSource>().Play();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "The Tenuto!";
        legatoButton.SetActive(true);
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Tenutos will help carry you through gaps between notes. Give it a whirl!";
        while (currStep == 3)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level15Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Oh my, it looks like I've spilled some ink on the scores... Do your best to work around it!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private IEnumerator Level17Cutscene()
    {
        yield return new WaitForEndOfFrame();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "WHA-?!?! Zero notes per measure??? Why, I've never seen anything so preposterous!!!";
        MaestroText.SetActive(true);
        DeactivatePlayerControls();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        while (currStep == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "We'll need to pull out all the stops for this one...";
        while (currStep == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Introducing...";
        while (currStep == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("Mouse").GetComponent<AudioSource>().clip = artic;
        GameObject.Find("Mouse").GetComponent<AudioSource>().Play();
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "The Tie!";
        tieButton.SetActive(true);
        while (currStep == 3)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "The Tie has the ability to bridge between two adjacent, same pitched notes!";
        while (currStep == 4)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "More importantly, for our purposes, the tie can extend into other measures!";
        while (currStep == 5)
        {
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).GetComponent<MaestroAudio>().PlayTalk();
        MaestroText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Go on, what are you waiting for?!";
        while (currStep == 6)
        {
            yield return new WaitForEndOfFrame();
        }
        MaestroText.SetActive(false);
        ReactivatePlayerControls();
        ResetButton.SetActive(true);
        PerformButton.SetActive(true);
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currStep += 1;
        }
    }

    private void DeactivatePlayerControls()
    {
        if (player != null)
        {
            player.GetComponent<PerformControls>().cutSceneDeactivation = true;
        }
        if (mouse != null)
        {
            mouse.GetComponent<ComposerControls>().cutSceneDeactivation = true;
        }
    }

    private void ReactivatePlayerControls()
    {
        if (player != null)
        {
            player.GetComponent<PerformControls>().cutSceneDeactivation = false;
        }
        if (mouse != null)
        {
            mouse.GetComponent<ComposerControls>().cutSceneDeactivation = false;
        }
    }
}
