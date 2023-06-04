using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrab : MonoBehaviour
{
    public Goals type;
    public GameObject interactIcon;
    private Transform player;
    public float minDistance = 3.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    float distanceToPlayer() => Vector3.Distance(player.position, transform.position);
    bool isNearToPlayer() => distanceToPlayer() < minDistance;

    void Update()
    {
        interactIcon.SetActive(isNearToPlayer());
    }

    public void GrabItem()
    {
        FindObjectOfType<GameController>().AddGoal(type);
        Destroy(gameObject);
    }
}
