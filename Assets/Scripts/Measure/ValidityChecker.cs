using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValidityChecker : MonoBehaviour
{
    public bool noTimeSignature = false;
    public bool CheckValidity()
    {
        if (noTimeSignature == true)
        {
            return true;
        }
        GameObject[] measures = GameObject.FindGameObjectsWithTag("measure");
        bool valid = true;
        foreach (GameObject measure in measures)
        {
            if (measure.GetComponent<MeasureConstraints>().currLength != measure.GetComponent<MeasureConstraints>().requiredLength)
            {
                measure.GetComponent<MeasureConstraints>().designatedTimeSigntaure.GetComponent<InvalidMeasure>().FlashRed();
                valid = false;
            }
        }
        return valid;
    }
}
