using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemSpriteMap
{
    public Goals type;
    public Sprite image;
}

public class ItemSprite : MonoBehaviour
{

    public List<ItemSpriteMap> itemMap;
    ItemSpriteMap currentItem;

    public ItemSpriteMap GetItemByType(Goals type) => itemMap.Where(item => item.type == type).FirstOrDefault();

    public void InitItem(Goals type, Transform panel)
    {
        currentItem = GetItemByType(type);
        GetComponent<Image>().sprite = currentItem.image;
        transform.SetParent(panel, false);
    }
}
