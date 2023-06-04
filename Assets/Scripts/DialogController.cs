using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Polyglot;

public class DialogController : MonoBehaviour
{
    public GameController gameController;
    public GameObject dialogPanel;
    public Image charSprite;
    public TMP_Text dialogText;
    DialogAsset currentAsset;
    int iDialog = 0;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        HideDialogPanel();
    }

    private void HideDialogPanel() => dialogPanel.SetActive(false);
    private void ShowDialogPanel() => dialogPanel.SetActive(true);
    public bool isDialogVisible() => dialogPanel.activeSelf;

    public void InitDialog(DialogAsset asset)
    {
        currentAsset = asset;
        ShowDialogPanel();
        iDialog = 0;
        ShowDialogText(iDialog);
    }

    void ShowDialogText(int i)
    {
        DialogLines currentLine = currentAsset.dialogOptions[0].lines[iDialog];
        dialogText.text = Localization.Get(currentLine.stringId);
        charSprite.sprite = gameController.GetCharacterByType(currentLine.character).sprite;
    }

}
