using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button answer1;
    public Button answer2;
    public Button answer3;
    public TextMeshProUGUI verdictText;

    void Start()
    {
        questionText.text = "Care este capitala Franței?";
        answer1.GetComponentInChildren<TextMeshProUGUI>().text = "Paris";
        answer2.GetComponentInChildren<TextMeshProUGUI>().text = "Berlin";
        answer3.GetComponentInChildren<TextMeshProUGUI>().text = "Madrid";

        answer1.onClick.AddListener(CorrectAnswer);
        answer2.onClick.AddListener(WrongAnswer);
        answer3.onClick.AddListener(WrongAnswer);

        verdictText.text = "";
    }

    void CorrectAnswer()
    {
        StartCoroutine(ShowVerdictAndHide("Corect!", Color.green));
    }

    void WrongAnswer()
    {
        StartCoroutine(ShowVerdictAndHide("Greșit!", Color.red));
    }

    IEnumerator ShowVerdictAndHide(string message, Color color)
    {
        verdictText.text = message;
        verdictText.color = color;

        yield return new WaitForSeconds(5f);

        verdictText.text = "";
        questionText.gameObject.SetActive(false);
        answer1.gameObject.SetActive(false);
        answer2.gameObject.SetActive(false);
        answer3.gameObject.SetActive(false);
    }
}