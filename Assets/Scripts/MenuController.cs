using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Polyglot;

public class MenuController : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangeLanguage(string language)
    {
        switch (language)
        {
            case "br":
                Localization.Instance.SelectedLanguage = Language.Portuguese_Brazil;
                break;
            default:
                Localization.Instance.SelectedLanguage = Language.English;
                break;

        }
    }
}
