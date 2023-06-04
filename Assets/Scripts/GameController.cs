using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<Goals> goalsAchieved = new List<Goals>();
    public List<CharacterInfo> characterList;
    public GameObject[] disableOnStart;

    public Transform itemPanel;
    public GameObject itemPrefab;
    public GameObject portalObj;
    public Notification notification;
    public Notification itemNotification;

    public DialogAsset initialDialog;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);

        Invoke("StartInitialDialog", 1.5f);
    }

    void StartInitialDialog()
    {
        FindObjectOfType<DialogController>().InitDialog(initialDialog);
    }

    public CharacterInfo GetCharacterByType(Character type) =>
        characterList.Where<CharacterInfo>(character => character.type == type).FirstOrDefault();

    public bool HasGoal(Goals goalToCheck) => goalsAchieved.Contains(goalToCheck);

    public void AddGoal(Goals newGoal)
    {
        goalsAchieved.Add(newGoal);

        switch (newGoal)
        {
            case Goals.ItemsKey:
            case Goals.ItemsDonuts:
            case Goals.ItemsHat:
                AddItem(newGoal);
                break;
            case Goals.ActionOpenPortal:
                portalObj.SetActive(true);
                break;
        }
    }

    private void AddItem(Goals item)
    {
        ItemSprite newItem = Instantiate(itemPrefab).GetComponent<ItemSprite>();
        newItem.InitItem(item, itemPanel);
        itemNotification.InitNotification(newItem.GetItemByType(item).image);
    }

    public void ShowNotification(string stringId)
    {
        notification.InitNotification(stringId);
    }
}
