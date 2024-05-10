using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Image teamRedImage;
	public Image teamBlueImage;
	public TextMeshProUGUI scoreRedText;
	public TextMeshProUGUI scoreBlueText;
	public TextMeshProUGUI messageText;

	private int scoreRed = 0;
	private int scoreBlue = 0;
	private bool isRedActive = false; // Indica se o time vermelho está ativo
	private bool isBlueActive = false; // Indica se o time azul está ativo

	private void OnEnable()
	{
		SerialController.OnMessageReceived += HandleSerialMessage;
		QuizManager.OnCorrectAnswer += HandleCorrectAnswer;
	}

	private void OnDisable()
	{
		SerialController.OnMessageReceived -= HandleSerialMessage;
		QuizManager.OnCorrectAnswer -= HandleCorrectAnswer;
	}

	private void HandleSerialMessage(string message)
	{
		switch (message.Trim())
		{
			case "0":
				ResetColors();
				messageText.text = "";
				isRedActive = false;
				isBlueActive = false;
				break;
			case "1":
				teamRedImage.color = Color.red;
				teamBlueImage.color = new Color(0.9f, 0.9f, 0.9f); // Cinza claro para o time azul
				messageText.text = "";
				isRedActive = true;
				isBlueActive = false;
				break;
			case "2":
				teamBlueImage.color = Color.blue;
				teamRedImage.color = new Color(0.9f, 0.9f, 0.9f); // Cinza claro para o time vermelho
				messageText.text = "";
				isBlueActive = true;
				isRedActive = false;
				break;
		}
	}

	private void HandleCorrectAnswer()
	{
		if (isRedActive)
		{
			scoreRed++;
			scoreRedText.text = scoreRed.ToString();
		}
		else if (isBlueActive)
		{
			scoreBlue++;
			scoreBlueText.text = scoreBlue.ToString();
		}
	}

	private void ResetColors()
	{
		teamRedImage.color = new Color(0.9f, 0.9f, 0.9f); // Cinza claro para o time vermelho
		teamBlueImage.color = new Color(0.9f, 0.9f, 0.9f); // Cinza claro para o time azul
	}
	
		public void OnBackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
