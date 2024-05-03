using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Importe para usar o componente Image

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public List<TextMeshProUGUI> optionTexts;
    public List<Image> optionBackgrounds; // Fundos das opções
    public List<GameObject> correctIndicators; // Indicadores de resposta correta
    public List<GameObject> wrongIndicators; // Indicadores de resposta errada

    private QuestionList questionList;
    private Question currentQuestion;
    private int selectedIndex = -1;
    private bool answerRevealed = false;

    void Start()
    {
        TextAsset questionData = Resources.Load<TextAsset>("Questions");
        questionList = JsonUtility.FromJson<QuestionList>(questionData.text);
        DrawQuestion();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!answerRevealed)
            {
                RevealAnswer();
            }
            else
            {
                DrawQuestion();
            }
        }
    }

    void DrawQuestion()
    {
        answerRevealed = false;
        selectedIndex = -1;
        ResetVisuals();

        int index = Random.Range(0, questionList.questions.Count);
        currentQuestion = questionList.questions[index];
        questionText.text = currentQuestion.question;

        string[] optionLabels = {"  A", "  B", "  C"};
        for (int i = 0; i < optionTexts.Count; i++)
        {
            if (i < currentQuestion.options.Count)
            {
                optionTexts[i].text = $"{optionLabels[i]}) {currentQuestion.options[i]}";
                optionTexts[i].gameObject.SetActive(true);
                optionBackgrounds[i].color = Color.white; // Cor padrão
            }
            else
            {
                optionTexts[i].gameObject.SetActive(false);
            }
        }
    }

    void RevealAnswer()
    {
        for (int i = 0; i < correctIndicators.Count; i++)
        {
            if (i == currentQuestion.answer)
            {
                correctIndicators[i].SetActive(true); // Mostra o indicador correto
            }
            else
            {
                wrongIndicators[i].SetActive(true); // Mostra o indicador errado
            }
        }

        optionBackgrounds[currentQuestion.answer].color = Color.green;
        if (selectedIndex != -1 && selectedIndex != currentQuestion.answer)
        {
            optionBackgrounds[selectedIndex].color = Color.red;
        }

        answerRevealed = true;
    }

    public void OnAnswerSelected(int optionIndex)
    {
        if (selectedIndex == -1)
        {
            selectedIndex = optionIndex;
            foreach (var img in optionBackgrounds)
            {
                img.color = Color.white; // Reset todas as cores
            }
            optionBackgrounds[optionIndex].color = Color.yellow; // Destaca a seleção
        }
    }

    void ResetVisuals()
    {
        foreach (var img in optionBackgrounds)
        {
            img.color = Color.white; // Reset cor de fundo
        }
        foreach (var indicator in correctIndicators)
        {
            indicator.SetActive(false); // Desativa indicadores corretos
        }
        foreach (var indicator in wrongIndicators)
        {
            indicator.SetActive(false); // Desativa indicadores errados
        }
    }
}
