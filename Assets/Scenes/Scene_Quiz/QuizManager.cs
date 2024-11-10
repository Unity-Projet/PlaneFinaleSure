using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private bool hasAttempted = false; // Tracks if user has made an attempt
    private bool isProcessingNextQuestion = false;
    private float shakeThreshold = 1.5f;
    private Vector3 lastAcceleration;

    // Set the desired button color A785CC (RGB: 167, 133, 204)
    private Color A785CC = new Color(167f / 255f, 133f / 255f, 204f / 255f);

    private Color originalButtonColor;
    private Color correctAnswerColor = Color.green;

    private void Start()
    {
        currentQuestionIndex = 0;
        score = 0;

        // Set the color immediately when the quiz starts
        if (optionButtons.Length > 0)
        {
            originalButtonColor = A785CC; // Set to A785CC color
            foreach (Button button in optionButtons)
            {
                button.GetComponent<Image>().color = originalButtonColor; // Set the color for all buttons initially
            }
        }

        DisplayQuestion();
        lastAcceleration = Input.acceleration;

        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void Update()
    {
        if (!isProcessingNextQuestion && hasAttempted) // Only detect shake after an attempt
        {
            DetectShake();
        }
    }

    private void DetectShake()
    {
        Vector3 currentAcceleration = Input.acceleration;
        if (currentAcceleration.magnitude > shakeThreshold && lastAcceleration.magnitude <= shakeThreshold)
        {
            ShowCorrectAnswer();
        }
        lastAcceleration = currentAcceleration;
    }

    public void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Debug.Log($"Displaying question {currentQuestionIndex + 1} of {questions.Count}");

            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            // Display options and reset button color
            for (int i = 0; i < optionTexts.Length; i++)
            {
                if (i < currentQuestion.options.Length)
                {
                    optionTexts[i].text = currentQuestion.options[i];
                    optionButtons[i].gameObject.SetActive(true);
                    optionButtons[i].GetComponent<Image>().color = originalButtonColor; // Reset button color to A785CC

                    int index = i;
                    optionButtons[i].onClick.RemoveAllListeners();
                    optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false);
                }
            }

            hasAnswered = false;
            hasAttempted = false; // Reset attempt tracker
            nextButton.gameObject.SetActive(false);
            isProcessingNextQuestion = false;

            // Update next button text based on question number
            if (currentQuestionIndex == questions.Count - 1)
            {
                nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Confirm";
            }
            else
            {
                nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
            }
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
            hasAttempted = true; // User has attempted an answer
            if (questions[currentQuestionIndex].correctAnswerIndex == chosenIndex)
            {
                score++;
            }
            nextButton.gameObject.SetActive(true);
        }
    }

    private void OnNextButtonClicked()
    {
        if (isProcessingNextQuestion) return;
        isProcessingNextQuestion = true;

        if (currentQuestionIndex == questions.Count - 1)
        {
            ShowScore();
            Invoke("LoadNextScene", 2.0f);
        }
        else
        {
            NextQuestion();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Scene plane 1");
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        DisplayQuestion();
    }

    private void ShowScore()
    {
        Debug.Log("All questions completed. Showing score.");
        scoreText.text = "Your score: " + score + "/" + questions.Count;
        nextButton.gameObject.SetActive(false);
    }

    private void ShowCorrectAnswer()
    {
        if (hasAnswered && hasAttempted) // Only show if the user has attempted an answer
        {
            int correctIndex = questions[currentQuestionIndex].correctAnswerIndex;
            optionButtons[correctIndex].GetComponent<Image>().color = correctAnswerColor;

            Debug.Log("Correct answer shown.");
        }
    }
}
