using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Polyglot;

public class Notification : MonoBehaviour
{
    public float timeToHide = 3.5f;
    public TMP_Text label;
    public Image image;

    void OnEnable()
    {
        Invoke("HideNotification", timeToHide);
    }

    public void InitNotification(string stringId)
    {
        label.text = Localization.Get(stringId);
        gameObject.SetActive(true);
    }

    public void InitNotification(Sprite sprite)
    {
        image.sprite = sprite;
        gameObject.SetActive(true);
    }

    void HideNotification()
    {
        gameObject.SetActive(false);
    }

}
