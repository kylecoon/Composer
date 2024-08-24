using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoArticulation : MonoBehaviour
{
    private NoteStateController st;
    private GameObject player;
    public LayerMask mask;
    private bool reseting;
    public float initial_x;
    private bool sliding = false;
    public AudioClip bounce;
    public AudioClip stretch;
    public AudioClip slide;
    void Start()
    {
        st = transform.parent.gameObject.transform.parent.GetComponent<NoteStateController>();
        player = GameObject.Find("Player");
        EventBus.Subscribe<ButtonEvent>(HandleButton);
        initial_x = transform.root.position.x;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (sliding)
            {
                player.transform.parent = transform;
                return;
            }
            if (st.currArticulation == 's' && player.transform.position.y > transform.position.y + 0.6f)
            {
                GetComponent<AudioSource>().clip = bounce;
                GetComponent<AudioSource>().Play();
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000f));
            }
            else if (st.currArticulation == 'o' && player.transform.position.y > transform.position.y + 0.6f)
            {
                StartCoroutine(LegatoSlide());
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }

    private IEnumerator LegatoSlide()
    {
        GetComponent<AudioSource>().clip = slide;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
        sliding = true;
        player.transform.parent = transform;
        while (!Physics2D.Raycast(new(transform.position.x + 2.0f, transform.position.y + 0.5f), transform.forward, 0.5f, mask) && !Physics2D.Raycast(new(transform.position.x + 2.0f, transform.position.y), transform.forward, 0.5f, mask) && !Physics2D.Raycast(new(transform.position.x + 2.0f, transform.position.y - 0.5f), transform.forward, 0.5f, mask) && !reseting)
        {
            transform.root.position = new(transform.root.position.x + 0.03f, transform.root.position.y);
            yield return new WaitForSecondsRealtime(0.005f);
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = false;
        sliding = false;
    }

    private void HandleButton(ButtonEvent e)
    {
        if (e.mode == 't' || e.mode == 'c')
        {
            StartCoroutine(ResetLegato());
        }
    }

    private IEnumerator ResetLegato()
    {
        reseting = true;
        transform.root.position = new(initial_x, transform.root.position.y);
        yield return new WaitForSeconds(0.25f);
        reseting = false;
    }
}
