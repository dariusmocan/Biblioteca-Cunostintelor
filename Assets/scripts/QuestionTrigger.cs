using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    public GameObject questionPanel;
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
                questionPanel.SetActive(true);
                interactionHint.SetActive(false);
                gameObject.SetActive(false); // dezactivezi obiectul
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