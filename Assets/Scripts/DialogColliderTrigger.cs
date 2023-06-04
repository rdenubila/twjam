using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogColliderTrigger : MonoBehaviour
{
    [SerializeField] DialogAsset dialogAsset;
    DialogController dialogController;

    void Start()
    {
        dialogController = FindObjectOfType<DialogController>();
    }


    public void InitDialog()
    {
        dialogController.InitDialog(dialogAsset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Colidiu");
            InitDialog();
            // Destroy(gameObject);
        }
    }
}
