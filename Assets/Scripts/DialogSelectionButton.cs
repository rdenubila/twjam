using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Polyglot;

public class DialogSelectionButton : MonoBehaviour
{

    DialogFields _dialog;
    [SerializeField] TMP_Text label;


    DialogController GetDialogController() => FindObjectOfType<DialogController>();

    public void Init(DialogFields dialog, Transform dialogSelectionPanel)
    {
        transform.SetParent(dialogSelectionPanel, false);

        _dialog = dialog;
        label.text = Localization.Get(_dialog.stringId);
    }

    public void Init(Transform dialogSelectionPanel)
    {
        transform.SetParent(dialogSelectionPanel, false);
    }

    public void OnClick()
    {
        GetDialogController().InitDialogLine(_dialog);
    }

    public void ExitDialogSelection()
    {
        GetDialogController().HideDialogSelectionPanel();
    }

}
