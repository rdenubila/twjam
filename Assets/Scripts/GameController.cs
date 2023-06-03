using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject[] disableOnStart;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
