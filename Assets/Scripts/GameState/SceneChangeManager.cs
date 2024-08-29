using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftCurtain;
    public GameObject rightCurtain;
    public GameObject text1;
    public GameObject text2;
    private int level = 0;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        Screen.SetResolution(1600, 1000, true);
        EventBus.Subscribe<LevelCompleteEvent>(TransitionLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void TransitionLevel(LevelCompleteEvent e)
    {
        StartCoroutine(TransitionLevel(e.level));
    }
    private IEnumerator TransitionLevel(int l)
    {
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<PerformControls>().cutSceneDeactivation = true;
        }
        GetComponent<AudioSource>().Play();
        if (level != 0)
        {
            StartCoroutine(FadeInProgress());
        }
        Vector3 left_initial = leftCurtain.GetComponent<RectTransform>().anchoredPosition;
        Vector3 right_initial = rightCurtain.GetComponent<RectTransform>().anchoredPosition;
        for (int i = 0; i < 50; ++i)
        {
            leftCurtain.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(left_initial, new(-48.1f + 15, -15.8f, 0), (i + 1) * 0.02f);
            rightCurtain.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(right_initial, new(48.1f + 15, -15.8f, 0), (i + 1) * 0.02f);
            yield return new WaitForSeconds(0.015f);
        }
        SceneManager.LoadScene((l + 1).ToString(), LoadSceneMode.Single);
        yield return new WaitForSeconds(0.3f);
        if (level != 0)
        {
            StartCoroutine(FadeOutProgress());
        }
        else
        {
            ++level;
        }
        for (int i = 0; i < 50; ++i)
        {
            leftCurtain.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(new(-48.1f + 15, -15.8f, 0), left_initial, (i + 1) * 0.02f);
            rightCurtain.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(new(48.1f + 15, -15.8f, 0), right_initial, (i + 1) * 0.02f);
            yield return new WaitForSeconds(0.015f);
        }
    }
    private IEnumerator FadeInProgress()
    {
        text1.GetComponent<TextMeshProUGUI>().text = level.ToString() + "/20";
        text1.SetActive(true);
        text2.SetActive(true);
        for (int i = 0; i < 25; ++i)
        {
            text1.GetComponent<TextMeshProUGUI>().alpha = 0.04f * (i + 1);
            text2.GetComponent<TextMeshProUGUI>().alpha = 0.04f * (i + 1);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private IEnumerator FadeOutProgress()
    {
        for (int i = 0; i < 25; ++i)
        {
            text1.GetComponent<TextMeshProUGUI>().alpha = 1.0f - (0.04f * (i + 1));
            text2.GetComponent<TextMeshProUGUI>().alpha = 1.0f - (0.04f * (i + 1));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        text1.SetActive(false);
        text2.SetActive(false);
        level++;
    }
}
