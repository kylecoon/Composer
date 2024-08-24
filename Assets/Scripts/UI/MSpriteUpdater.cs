using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSpriteUpdater : MonoBehaviour
{
    public Sprite sp1;
    public Sprite sp2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateSprite());
    }

    private IEnumerator UpdateSprite()
    {
        while (true)
        {
            GetComponent<Image>().sprite = sp1;
            yield return new WaitForSeconds(0.3f);
            GetComponent<Image>().sprite = sp2;
            yield return new WaitForSeconds(0.3f);

        }
    }
}
