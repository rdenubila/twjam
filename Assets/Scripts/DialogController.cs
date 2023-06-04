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
    DialogFields currentDialog;
    int iDialogLine = 0;


    public GameObject dialogSelectionPanel;
    public GameObject dialogSelectionButtonPrefab;
    public GameObject dialogSelectionExitButtonPrefab;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        HideDialogSelectionPanel();
        HideDialogPanel();
    }

    public void HideDialogSelectionPanel() => dialogSelectionPanel.SetActive(false);
    private void ShowDialogSelectionPanel() => dialogSelectionPanel.SetActive(true);
    private void HideDialogPanel() => dialogPanel.SetActive(false);
    private void ShowDialogPanel() => dialogPanel.SetActive(true);
    public bool isDialogVisible() => dialogPanel.activeSelf || dialogSelectionPanel.activeSelf;

    public void InitDialog(DialogAsset asset)
    {
        currentAsset = asset;
        PopulateDialogOptions();
        ShowDialogSelectionPanel();
    }

    void PopulateDialogOptions()
    {
        while (dialogSelectionPanel.transform.childCount > 0)
            DestroyImmediate(dialogSelectionPanel.transform.GetChild(0).gameObject);

        foreach (DialogFields option in currentAsset.dialogOptions)
        {
            Instantiate(dialogSelectionButtonPrefab)
                .GetComponent<DialogSelectionButton>()
                .Init(option, dialogSelectionPanel.transform);
        }

        Instantiate(dialogSelectionExitButtonPrefab)
                .GetComponent<DialogSelectionButton>()
                .Init(dialogSelectionPanel.transform);
    }

    public void InitDialogLine(DialogFields fields)
    {
        HideDialogSelectionPanel();
        currentDialog = fields;
        ShowDialogPanel();
        iDialogLine = 0;
        ShowDialogText(iDialogLine);
    }

    void ShowDialogText(int i)
    {
        DialogLines currentLine = currentDialog.lines[iDialogLine];
        CharacterInfo currentChar = gameController.GetCharacterByType(currentLine.character);

        dialogText.text = Localization.Get(currentLine.stringId);
        nameText.text = currentChar.name + "\n<size=80%>" + currentChar.role;
        charSprite.sprite = currentChar.sprite;
    }

    public void NextDialogLine()
    {
        iDialogLine++;
        if (iDialogLine < currentDialog.lines.Length)
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
        if (currentDialog.availableAfter != Goals.None)
            gameController.AddGoal(currentDialog.availableAfter);

        HideDialogPanel();
        ShowDialogSelectionPanel();
    }

}
