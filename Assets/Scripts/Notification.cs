using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Polyglot;

public class Notification : MonoBehaviour
{
    public float timeToHide = 3.5f;
    public TMP_Text label;

    void OnEnable()
    {
        Invoke("HideNotification", timeToHide);
    }

    public void InitNotification(string stringId)
    {
        label.text = Localization.Get(stringId);
        gameObject.SetActive(true);
    }

    void HideNotification()
    {
        gameObject.SetActive(false);
    }

}
