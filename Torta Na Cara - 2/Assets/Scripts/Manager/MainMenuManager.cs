using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Dropdown serialPortDropdown;

    void Start()
    {
        // Adiciona opções de portas seriais ao dropdown
        string[] ports = System.IO.Ports.SerialPort.GetPortNames();
        serialPortDropdown.ClearOptions();
        serialPortDropdown.AddOptions(new List<string>(ports));

        // Carrega a última porta salva, se houver
        string savedPort = SerialConfigManager.Instance.GetSerialPort();
        int index = Array.IndexOf(ports, savedPort);
        if (index >= 0)
        {
            serialPortDropdown.value = index;
        }

        // Adiciona listener para mudanças no dropdown
        serialPortDropdown.onValueChanged.AddListener(OnSerialPortChanged);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnEditQuestionsButton()
    {
        SceneManager.LoadScene("CadastroDeQuestoes");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnSerialPortChanged(int index)
    {
        // Salva a porta serial selecionada
        string selectedPort = serialPortDropdown.options[index].text;
        SerialConfigManager.Instance.SetSerialPort(selectedPort);
    }
}
