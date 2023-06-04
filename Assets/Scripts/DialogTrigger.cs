using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{

    [SerializeField] DialogAsset dialogAsset;
    DialogController dialogController;
    public GameObject interactIcon;
    private Transform player;
    public float minDistance = 1f;

    void Start()
    {
        dialogController = FindObjectOfType<DialogController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void showIcon() => interactIcon.SetActive(true);
    void hideIcon() => interactIcon.SetActive(false);
    float distanceToPlayer() => Vector3.Distance(player.position, transform.position);
    bool isNearToPlayer() => distanceToPlayer() < minDistance;

    void Update()
    {
        interactIcon.SetActive(isNearToPlayer());
    }

    public void InitDialog()
    {
        if (isNearToPlayer())
            dialogController.InitDialog(dialogAsset);
    }

}
