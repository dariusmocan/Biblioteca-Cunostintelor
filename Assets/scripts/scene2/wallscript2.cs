using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallscript2 : MonoBehaviour
{
    public QuestionManager2 correctAnswer;
    public int goal = 3;

    void Start()
    {
        // Găsește instanța s2questionmanager din scenă dacă nu este atribuită
        if (correctAnswer == null)
        {
            correctAnswer = FindObjectOfType<QuestionManager2>();
            if (correctAnswer == null)
            {
                Debug.LogError("Nu s-a găsit QuestionManager2 în scenă!");
            }
        }
    }

    void Update()
    {
        // Verifică dacă referința la s2questionmanager există
        if (correctAnswer == null)
        {
            Debug.LogWarning("QuestionManager2 reference is null! Trying to find it again...");
            correctAnswer = FindObjectOfType<QuestionManager2>();
            return;
        }

        // Afișează valoarea curentă a răspunsurilor corecte
        if (Time.frameCount % 60 == 0)
        {
            Debug.Log($"Current correct answers: {correctAnswer.getCorrectAnswer()}, Goal: {goal}");
        }

        // Verifică condiția pentru distrugerea peretelui
        if (correctAnswer.getCorrectAnswer() >= goal)
        {
            Debug.Log("Condition met! Wall should be destroyed.");
            Destroy(gameObject);
            Debug.Log("Wall Destroyed");
        }
    }
}
