using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public QuestionManager correctAnswer;
    public int goal = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        // Găsește instanța QuestionManager din scenă dacă nu este atribuită
        if (correctAnswer == null)
        {
            correctAnswer = FindObjectOfType<QuestionManager>();
            if (correctAnswer == null)
            {
                Debug.LogError("Nu s-a găsit QuestionManager în scenă!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Verifică dacă referința la QuestionManager există
        if (correctAnswer == null)
        {
            Debug.LogWarning("QuestionManager reference is null! Trying to find it again...");
            correctAnswer = FindObjectOfType<QuestionManager>();
            return;
        }
        
        // Afișează valoarea curentă a răspunsurilor corecte
        if (Time.frameCount % 60 == 0) // Afișează o dată la ~1 secundă pentru a nu umple consola
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
