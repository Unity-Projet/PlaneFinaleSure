using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] options;
        public int correctAnswerIndex;
    }

    public List<Question> questions;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts;
    public Button[] optionButtons;
    public Button nextButton;
    public TextMeshProUGUI scoreText;

    private int currentQuestionIndex;
    private int score;
    private bool hasAnswered = false; 
    private float shakeThreshold = 1.5f;
    private Vector3 lastAcceleration;

    private void Start()
    {
        currentQuestionIndex = 0;
        score = 0;
        DisplayQuestion();
        lastAcceleration = Input.acceleration;
    }

    private void Update()
    {
        DetectShake();
    }

    private void DetectShake()
    {
        Vector3 currentAcceleration = Input.acceleration;
        if (currentAcceleration.magnitude > shakeThreshold && lastAcceleration.magnitude <= shakeThreshold)
        {
            NextQuestion();
        }
        lastAcceleration = currentAcceleration;
    }

    public void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < optionTexts.Length; i++)
            {
                optionTexts[i].text = currentQuestion.options[i];
                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
            }

            hasAnswered = false; 
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            ShowScore();
        }
    }

    public void CheckAnswer(int chosenIndex)
    {
        if (!hasAnswered)
        {
            hasAnswered = true; 
            if (questions[currentQuestionIndex].correctAnswerIndex == chosenIndex)
            {
                score++;
            }
            nextButton.gameObject.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion();
        }
        else
        {
            ShowScore();
        }
    }

    private void ShowScore()
    {
        scoreText.text = "Your score: " + score + "/" + questions.Count;
        nextButton.gameObject.SetActive(false); 
    }
}
