using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArticulationTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject artTip;
    public void OnPointerEnter(PointerEventData eventData)
    {
        artTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        artTip.SetActive(false);
    }
}
