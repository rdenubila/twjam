using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<CharacterInfo> characterList;
    public GameObject[] disableOnStart;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);
    }

    public CharacterInfo GetCharacterByType(Character type) =>
        characterList.Where<CharacterInfo>(character => character.type == type).FirstOrDefault();
}
