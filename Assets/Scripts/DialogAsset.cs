using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogFields
{
    public string label;
    public string stringId;
    [Tooltip("This dialog line will be available only if...")]
    public Goals availableIf = Goals.None;
    [Tooltip("Make this Goal available after this dialog")]
    public Goals availableAfter = Goals.None;
    public DialogLines[] lines;
}

[Serializable]
public class DialogLines
{
    public string label;
    public string stringId;
    public Character character;
}

[CreateAssetMenu(fileName = "new Dialog", menuName = "Game Assets/Dialog")]
public class DialogAsset : ScriptableObject
{
    public DialogFields[] dialogOptions;
}
