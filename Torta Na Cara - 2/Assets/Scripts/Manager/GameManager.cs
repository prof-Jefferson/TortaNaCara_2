using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image teamRedImage;
    public Image teamBlueImage;

    private void OnEnable()
    {
        SerialController.OnMessageReceived += HandleSerialMessage;
    }

    private void OnDisable()
    {
        SerialController.OnMessageReceived -= HandleSerialMessage;
    }

    private void HandleSerialMessage(string message)
    {
        switch (message.Trim())
        {
            case "1":
                teamRedImage.color = Color.red;
                teamBlueImage.color = new Color(0.9f, 0.9f, 0.9f); // Cinza claro para a outra equipe
                break;
            case "2":
                teamBlueImage.color = Color.blue;
                teamRedImage.color = new Color(0.9f, 0.9f, 0.9f); // Cinza claro para a outra equipe
                break;
            case "0":
                // Redefine ambas as imagens para cinza claro
                teamRedImage.color = new Color(0.9f, 0.9f, 0.9f);
                teamBlueImage.color = new Color(0.9f, 0.9f, 0.9f);
                break;
        }
    }
}
