using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvalidMeasure : MonoBehaviour
{
    private bool cooldown = false;
    public void FlashRed()
    {
        if (!cooldown)
        {
            StartCoroutine(DoFlashRed());
        }
    }
    private IEnumerator DoFlashRed()
    {
        cooldown = true;
        EventBus.Publish(new InvalidMeasureSound());
        Color initial_color = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().color;
        for (int i = 0; i < 25; ++i)
        {
            transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().color = Color.Lerp(initial_color, Color.red, (i + 1) * 0.04f);
            transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = Color.Lerp(initial_color, Color.red, (i + 1) * 0.04f);
            if (transform.childCount > 2 && transform.GetChild(2).gameObject.GetComponent<TextMeshPro>() != null)
            {
                transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().color = Color.Lerp(initial_color, Color.red, (i + 1) * 0.04f);
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 25; ++i)
        {
            transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().color = Color.Lerp(Color.red, initial_color, (i + 1) * 0.04f);
            transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = Color.Lerp(Color.red, initial_color, (i + 1) * 0.04f);
            if (transform.childCount > 2 && transform.GetChild(2).gameObject.GetComponent<TextMeshPro>() != null)
            {
                transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().color = Color.Lerp(Color.red, initial_color, (i + 1) * 0.04f);
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForEndOfFrame();
        cooldown = false;
    }
}

public class InvalidMeasureSound
{
    public InvalidMeasureSound()
    {

    }
}
