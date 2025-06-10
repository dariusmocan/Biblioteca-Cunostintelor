using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuestionManager2 : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button answer1;
    public Button answer2;
    public Button answer3;
    public TextMeshProUGUI verdictText;
    private int correctAnswer = 0;

    private List<QuestionItem> allQuestions;
    private List<QuestionItem> quizQuestions;
    private int currentQuestionIndex = 0;
    private int requiredCorrect = 5;
    private int questionsPerQuiz = 10;

    void Start()
    {
        verdictText.text = "";
        LoadAllQuestions();
        StartQuiz();
    }

    void LoadAllQuestions()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("quiz2easy");
        if (jsonText == null)
        {
            Debug.LogError("quiz2easy.json not found in Resources!");
            return;
        }

        // Extrage lista de întrebări din categoria "Istorie și Geografie"
        var wrapper = JsonUtility.FromJson<QuestionListWrapper>("{\"questions\":" + jsonText.text.Split(new[] { ':' }, 2)[1].TrimEnd('}', '\n', '\r') + "}");
        allQuestions = wrapper.questions;
    }

    void StartQuiz()
    {
        answer1.interactable = true;
        answer2.interactable = true;
        answer3.interactable = true;
        correctAnswer = 0;
        currentQuestionIndex = 0;

        // Selectează 5 întrebări random
        quizQuestions = allQuestions.OrderBy(q => Random.value).Take(questionsPerQuiz).ToList();

        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int index)
    {
        if (quizQuestions == null || index >= quizQuestions.Count)
            return;

        var q = quizQuestions[index];
        questionText.text = q.question;
        answer1.GetComponentInChildren<TextMeshProUGUI>().text = q.options[0];
        answer2.GetComponentInChildren<TextMeshProUGUI>().text = q.options[1];
        answer3.GetComponentInChildren<TextMeshProUGUI>().text = q.options[2];

        answer1.onClick.RemoveAllListeners();
        answer2.onClick.RemoveAllListeners();
        answer3.onClick.RemoveAllListeners();

        answer1.onClick.AddListener(() => CheckAnswer(q.options[0], q.correct_answer));
        answer2.onClick.AddListener(() => CheckAnswer(q.options[1], q.correct_answer));
        answer3.onClick.AddListener(() => CheckAnswer(q.options[2], q.correct_answer));

        // Asigură-te că butoanele și întrebarea sunt vizibile
        questionText.gameObject.SetActive(true);
        answer1.gameObject.SetActive(true);
        answer2.gameObject.SetActive(true);
        answer3.gameObject.SetActive(true);
    }

    void CheckAnswer(string selected, string correct)
    {
        if (selected == correct)
        {
            correctAnswer++;
            StartCoroutine(ShowVerdictAndNext("Corect!", Color.green));
        }
        else
        {
            StartCoroutine(ShowVerdictAndNext("Greșit!", Color.red));
        }
    }

    public int getCorrectAnswer()
    {
        return correctAnswer;
    }

    IEnumerator ShowVerdictAndNext(string message, Color color)
    {
        verdictText.text = message;
        verdictText.color = color;

        // Dezactivează butoanele ca să nu poată fi apăsate de mai multe ori
        answer1.interactable = false;
        answer2.interactable = false;
        answer3.interactable = false;

        yield return new WaitForSeconds(1.5f);

        verdictText.text = "";

        // Verifică dacă a câștigat
        if (correctAnswer >= requiredCorrect)
        {
            // Quiz terminat cu succes
            questionText.text = "Felicitări! Ai răspuns corect la 5 întrebări!";
            answer1.gameObject.SetActive(false);
            answer2.gameObject.SetActive(false);
            answer3.gameObject.SetActive(false);

            yield return new WaitForSeconds(3f);

            questionText.text = "";
            yield break;
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < quizQuestions.Count)
        {
            // Mai sunt întrebări, treci la următoarea
            answer1.interactable = true;
            answer2.interactable = true;
            answer3.interactable = true;
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // Nu a reușit, reia quiz-ul
            questionText.text = "Nu ai răspuns corect la 5 întrebări. Încearcă din nou!";
            answer1.gameObject.SetActive(false);
            answer2.gameObject.SetActive(false);
            answer3.gameObject.SetActive(false);

            yield return new WaitForSeconds(2f);

            // Repornește quiz-ul
            answer1.gameObject.SetActive(true);
            answer2.gameObject.SetActive(true);
            answer3.gameObject.SetActive(true);
            StartQuiz();
        }
    }

    [System.Serializable]
    private class QuestionListWrapper
    {
        public List<QuestionItem> questions;
    }
}