using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger2 : MonoBehaviour
{
    public GameObject questionPanel2;
    public GameObject interactionHint;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear)
        {
            interactionHint.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                questionPanel2.SetActive(true); // aici setezi panelul cu NextSceneByLevel1
                interactionHint.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionHint.SetActive(false);
        }
    }
}
