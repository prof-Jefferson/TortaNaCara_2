using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionEditorManager : MonoBehaviour
{
	// Referências aos campos de entrada de texto
	public TMP_InputField questionInputField;
	public TMP_InputField optionAInputField;
	public TMP_InputField optionBInputField;
	public TMP_InputField optionCInputField;
	public TMP_Dropdown correctAnswerDropdown;
	public Button saveButton;

	private QuestionList questionList;

	void Start()
	{
		// Carrega as questões já existentes ou inicializa um novo QuestionList
		LoadQuestions();

		// Configura o botão de salvar para adicionar a nova questão
		saveButton.onClick.AddListener(AddNewQuestion);
	}

	void LoadQuestions()
	{
		TextAsset questionData = Resources.Load<TextAsset>("Questions");
		questionList = questionData != null ? JsonUtility.FromJson<QuestionList>(questionData.text) : new QuestionList();
	}

	void SaveQuestions()
	{
		string json = JsonUtility.ToJson(questionList, true);
		System.IO.File.WriteAllText(Application.dataPath + "/Resources/Questions.json", json);
	}

	void AddNewQuestion()
	{
		// Obtém os valores das entradas de texto
		string question = questionInputField.text;
		string optionA = optionAInputField.text;
		string optionB = optionBInputField.text;
		string optionC = optionCInputField.text;
		int correctAnswer = correctAnswerDropdown.value;

		// Validação básica das entradas
		if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(optionA) ||
			string.IsNullOrWhiteSpace(optionB) || string.IsNullOrWhiteSpace(optionC))
		{
			Debug.LogWarning("Todos os campos devem ser preenchidos.");
			return;
		}

		// Cria uma nova pergunta e adiciona à lista
		Question newQuestion = new Question(question, new List<string> { optionA, optionB, optionC }, correctAnswer);
		questionList.questions.Add(newQuestion);

		// Salva as questões no arquivo JSON
		SaveQuestions();

		// Reseta os campos de entrada para a próxima adição
		questionInputField.text = "";
		optionAInputField.text = "";
		optionBInputField.text = "";
		optionCInputField.text = "";
		correctAnswerDropdown.value = 0;
	}
	
	public void OnBackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
