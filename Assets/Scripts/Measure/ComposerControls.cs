using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ComposerControls : MonoBehaviour
{
    public static bool handEmpty;
    private bool inNote;
    private GameObject selectedNote;
    private GameObject tieStartNote;
    private MeasureConstraints selectedMeasure;
    private char currMode = 'c';
    public char currArticulation = '_';
    public bool inArtZone = false;
    public bool placingTie = false;
    public bool cutSceneDeactivation = false;
    public AudioClip pickup;
    public AudioClip putdown;
    public AudioClip reset;
    public AudioClip restart;
    private AudioSource aud;
    public AudioClip wrong;
    private bool wrongCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        handEmpty = true;
        inNote = false;
        selectedNote = null;
        EventBus.Subscribe<ButtonEvent>(UpdateMode);
        EventBus.Subscribe<ArticulationSelect>(HandleArticulationSelect);
        aud = GetComponent<AudioSource>();
        EventBus.Subscribe<InvalidMeasureSound>(PlayWrong);
    }

    // Update is called once per frame
    void Update()
    {
        if (currMode == 'c')
        {
            GetInput();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("note") && handEmpty)
        {
            inNote = true;
            selectedNote = other.transform.parent.gameObject.transform.parent.gameObject;
            if (selectedNote.GetComponent<NoteStateController>().state == 0 && selectedNote.transform.childCount == 6)
            {
                selectedNote.transform.GetChild(5).gameObject.SetActive(true);
            }

        }
        else if (other.CompareTag("measure"))
        {
            selectedMeasure = other.GetComponent<MeasureConstraints>();
        }
        else if (other.CompareTag("artzone"))
        {
            inArtZone = true;
        }
        else if (other.CompareTag("time") && currMode == 'c' && handEmpty)
        {
            if (handEmpty && other.gameObject.transform.childCount > 0)
            {
                other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("note") && handEmpty)
        {
            inNote = false;
            if (selectedNote != null && selectedNote.transform.childCount == 6)
            {
                selectedNote.transform.GetChild(5).gameObject.SetActive(false);
            }
            selectedNote = null;
        }
        else if (other.CompareTag("artzone"))
        {
            inArtZone = false;
        }
        else if (other.CompareTag("time"))
        {
            if (other.gameObject.transform.childCount > 0)
            {
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0) && !cutSceneDeactivation)
        {
            if (placingTie)
            {
                if (selectedNote == null || selectedNote.GetComponent<NoteStateController>().state != 3 || selectedNote.GetComponent<NoteInfo>().line != tieStartNote.GetComponent<NoteInfo>().line)
                {
                    tieStartNote.GetComponent<NoteStateController>().RemoveArticulation();
                    tieStartNote.GetComponent<NoteStateController>().RemoveTie();
                    placingTie = false;
                    return;
                }
                GameObject[] notes = GameObject.FindGameObjectsWithTag("note");
                for (int i = 0; i < notes.Length; ++i)
                {
                    if (notes[i].GetComponent<NoteInfo>() == null)
                    {
                        continue;
                    }
                    if ((notes[i].GetComponent<NoteInfo>().x_pos > tieStartNote.GetComponent<NoteInfo>().x_pos && notes[i].GetComponent<NoteInfo>().x_pos < selectedNote.GetComponent<NoteInfo>().x_pos) || (notes[i].GetComponent<NoteInfo>().x_pos > selectedNote.GetComponent<NoteInfo>().x_pos && notes[i].GetComponent<NoteInfo>().x_pos < tieStartNote.GetComponent<NoteInfo>().x_pos))
                    {
                        tieStartNote.GetComponent<NoteStateController>().RemoveArticulation();
                        tieStartNote.GetComponent<NoteStateController>().RemoveTie();
                        placingTie = false;
                        return;
                    }
                }
                if (selectedNote.GetComponent<NoteInfo>().line == tieStartNote.GetComponent<NoteInfo>().line && selectedNote.GetComponent<NoteStateController>().state == 3 && tieStartNote.GetComponent<NoteStateController>().state == 3)
                {
                    selectedNote.GetComponent<NoteStateController>().RemoveArticulation();
                    selectedNote.GetComponent<NoteStateController>().AddArticulation('t');
                    selectedNote.GetComponent<NoteStateController>().connectedNote = tieStartNote;
                    tieStartNote.GetComponent<NoteStateController>().connectedNote = selectedNote;
                }
                placingTie = false;
            }
            //no note grabbed
            else if (handEmpty)
            {
                if (selectedNote != null)
                {
                    if (currArticulation == '_')
                    {
                        selectedNote.transform.parent = transform;
                        if (selectedNote.GetComponent<NoteStateController>().currArticulation == 't')
                        {
                            selectedNote.GetComponent<NoteStateController>().RemoveTie();
                            selectedNote.GetComponent<NoteStateController>().RemoveArticulation();
                        }
                        if (selectedNote.GetComponent<NoteStateController>().state == 0)
                        {
                            selectedNote.GetComponent<NoteStateController>().UpdateState(1);
                            Debug.Log("yessir");
                        }
                        else
                        {
                            selectedMeasure.RemoveNote(selectedNote.GetComponent<NoteInfo>());
                            selectedNote.GetComponent<NoteStateController>().UpdateState(2);
                            EventBus.Publish(new LineEvent(selectedNote.GetComponent<AttachToLine>().currLine, selectedNote.transform.position.y - 0.5f));
                        }
                        aud.clip = pickup;
                        aud.Play();
                        handEmpty = false;
                    }
                    else
                    {
                        if (selectedNote == null)
                        {
                            DiscardArticulation();
                        }
                        if (currArticulation == 'T')
                        {
                            selectedNote.GetComponent<NoteStateController>().AddArticulation('t');
                            DiscardArticulation();
                        }
                        else if (currArticulation == 't')
                        {
                            if (selectedNote.GetComponent<NoteStateController>().state != 3)
                            {
                                DiscardArticulation();
                            }
                            else
                            {
                                StartCoroutine(StartTie());
                                currArticulation = 'T';
                                aud.clip = putdown;
                                aud.Play();
                            }

                        }
                        else if (currArticulation != '_')
                        {
                            selectedNote.GetComponent<NoteStateController>().AddArticulation(currArticulation);
                            DiscardArticulation();
                        }
                    }
                }
                else
                {
                    if (currArticulation != '_')
                    {
                        DiscardArticulation();
                    }
                }
            }
            //note grabbed
            else if (!inArtZone)
            {
                if (selectedNote.GetComponent<NoteStateController>().canBePlaced)
                {
                    if (selectedNote.GetComponent<NoteStateController>().state == 1)
                    {
                        aud.clip = putdown;
                        aud.Play();
                        handEmpty = true;
                        selectedNote.GetComponent<AttachToLine>().currLine = -1;
                        selectedNote.GetComponent<NoteStateController>().UpdateState(0);
                    }
                    else if (selectedNote.GetComponent<NoteStateController>().state == 2)
                    {
                        if (selectedMeasure.CheckNewNote(selectedNote.GetComponent<NoteInfo>().length))
                        {
                            selectedMeasure.AddNote(selectedNote.GetComponent<NoteInfo>());
                            selectedNote.GetComponent<NoteStateController>().UpdateState(3);
                            aud.clip = putdown;
                            aud.Play();
                            handEmpty = true;
                        }
                        else
                        {
                            selectedMeasure.designatedTimeSigntaure.GetComponent<InvalidMeasure>().FlashRed();
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && !cutSceneDeactivation)
        {
            if (selectedNote != null)
            {
                if (selectedNote.GetComponent<NoteStateController>().currArticulation == 't')
                {
                    selectedNote.GetComponent<NoteStateController>().RemoveTie();
                }
                selectedNote.GetComponent<NoteStateController>().RemoveArticulation();


            }
            DiscardArticulation();
        }
    }
    private void UpdateMode(ButtonEvent e)
    {
        if (e.mode == 'c')
        {
            currMode = e.mode;
        }
        else if (e.mode == 'p')
        {
            currMode = e.mode;
            if (selectedNote != null)
            {
                selectedNote.GetComponent<NoteStateController>().ResetNote();
                selectedNote = null;
                inNote = false;
                handEmpty = true;
            }
        }
        else if (e.mode == 't')
        {
            handEmpty = true;
            inNote = false;
            selectedNote = null;
            aud.clip = restart;
            aud.Play();
        }
        else if (e.mode == 'r')
        {
            aud.clip = reset;
            aud.Play();
        }
    }

    private void HandleArticulationSelect(ArticulationSelect e)
    {
        if (e.articulation == 's')
        {
            if (handEmpty)
            {
                transform.GetChild(1).gameObject.SetActive(true);

                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                currArticulation = 's';
                aud.clip = pickup;
                aud.Play();
            }
            else
            {
                selectedNote.GetComponent<NoteStateController>().AddArticulation('s');
            }
        }
        else if (e.articulation == 'o')
        {
            if (handEmpty)
            {
                transform.GetChild(2).gameObject.SetActive(true);

                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                currArticulation = 'o';
                aud.clip = pickup;
                aud.Play();
            }
            else
            {
                selectedNote.GetComponent<NoteStateController>().AddArticulation('o');
            }
        }
        else if (e.articulation == 't')
        {
            if (handEmpty)
            {
                transform.GetChild(3).gameObject.SetActive(true);

                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                currArticulation = 't';
                aud.clip = pickup;
                aud.Play();
            }
            else
            {
                selectedNote.GetComponent<NoteStateController>().RemoveArticulation();
            }
        }
    }

    private void DiscardArticulation()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        aud.clip = putdown;
        aud.Play();
        currArticulation = '_';
    }

    public IEnumerator StartTie()
    {
        placingTie = true;
        StartCoroutine(selectedNote.GetComponent<NoteStateController>().adjustTie());
        tieStartNote = selectedNote;
        transform.GetChild(3).gameObject.SetActive(false);
        while (placingTie)
        {
            yield return new WaitForSeconds(0.01f);
        }
        tieStartNote = null;
        currArticulation = '_';
    }
    public void PlayWrong(InvalidMeasureSound e)
    {
        StartCoroutine(PlayWrong_());
    }
    private IEnumerator PlayWrong_()
    {
        if (wrongCooldown)
        {
            yield break;
        }
        wrongCooldown = true;
        aud.clip = wrong;
        aud.Play();
        yield return new WaitForSeconds(0.3f);
        wrongCooldown = false;
    }
}

public class ArticulationSelect
{
    //staccato = s
    //tenuto = o
    //tie = t
    public char articulation;

    public ArticulationSelect(char c)
    {
        this.articulation = c;
    }
}
