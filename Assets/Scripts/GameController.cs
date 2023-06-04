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
    public Notification notification;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);
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
        }
    }

    private void AddItem(Goals item)
    {
        Instantiate(itemPrefab).GetComponent<ItemSprite>().InitItem(item, itemPanel);
    }

    public void ShowNotification(string stringId)
    {
        notification.InitNotification(stringId);
    }
}
