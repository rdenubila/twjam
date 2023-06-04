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
    public TMP_Text nameText;
    DialogAsset currentAsset;
    int iDialogLine = 0;
    int iDialogOption = 0;

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
        iDialogLine = 0;
        ShowDialogText(iDialogLine);
    }

    void ShowDialogText(int i)
    {
        DialogLines currentLine = currentAsset.dialogOptions[iDialogOption].lines[iDialogLine];
        CharacterInfo currentChar = gameController.GetCharacterByType(currentLine.character);

        dialogText.text = Localization.Get(currentLine.stringId);
        nameText.text = currentChar.name + "\n<size=80%>" + currentChar.role;
        charSprite.sprite = currentChar.sprite;
    }

    public void NextDialogLine()
    {
        iDialogLine++;
        if (iDialogLine < currentAsset.dialogOptions[iDialogOption].lines.Length)
        {
            ShowDialogText(iDialogLine);
        }
        else
        {
            FinishDialog();
        }
    }

    void FinishDialog()
    {
        DialogFields currentDialog = currentAsset.dialogOptions[iDialogOption];
        if (currentDialog.availableAfter != Goals.None)
            gameController.AddGoal(currentDialog.availableAfter);

        HideDialogPanel();
    }

}
