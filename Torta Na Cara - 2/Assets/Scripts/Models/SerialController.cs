using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialController : MonoBehaviour
{
	public delegate void MessageReceivedHandler(string message);
	public static event MessageReceivedHandler OnMessageReceived;

	private static Thread IOThread;
	private static SerialPort sp;
	private static string incomingMsg = "";
	private static string outgoingMsg = "";
	private static bool keepRunning = true;

	private static void DataThread(string portName, int baudRate)
	{
		sp = new SerialPort(portName, baudRate);
		try
		{
			sp.Open();
			Debug.Log($"Porta Serial {portName} aberta");
		}
		catch (Exception ex)
		{
			Debug.LogError($"Erro ao abrir a porta serial {portName}: {ex.Message}");
			return;
		}

		while (keepRunning)
		{
			if (outgoingMsg != "")
			{
				Debug.Log("Enviando: " + outgoingMsg);
				sp.Write(outgoingMsg);
				outgoingMsg = "";
			}

			try
			{
				if (sp.BytesToRead > 0)
				{
					incomingMsg = sp.ReadLine().Trim(); // Remove espaços e quebras de linha
					Debug.Log("Recebido: " + incomingMsg);
					UnityMainThreadDispatcher.Enqueue(() =>
					{
						OnMessageReceived?.Invoke(incomingMsg);
					});
				}
			}
			catch (IOException ex)
			{
				// Trata exceções de IO (como desconexão da porta serial)
				Debug.LogWarning("Erro de IO na porta serial: " + ex.Message);
			}
			catch (Exception ex)
			{
				// Ignorar exceções genéricas para evitar interrupções na thread
				Debug.LogWarning("Erro genérico na porta serial: " + ex.Message);
			}

			Thread.Sleep(1); // Intervalo reduzido para melhorar a responsividade
		}

		if (sp != null && sp.IsOpen)
		{
			sp.Close();
			Debug.Log("Porta Serial fechada");
		}
	}

	private void Start()
	{
		string portName = SerialConfigManager.Instance.GetSerialPort();
		if (string.IsNullOrEmpty(portName))
		{
			Debug.LogError("Serial Port not set. Please set the port in the main menu.");
			return;
		}

		int baudRate = 9600;
		IOThread = new Thread(() => DataThread(portName, baudRate));
		keepRunning = true;
		IOThread.Start();
	}

	public void Send(string message)
	{
		outgoingMsg = message;
		Debug.Log("Mensagem para enviar enfileirada: " + message);
	}

	private void OnDestroy()
	{
		keepRunning = false;

		if (IOThread != null)
		{
			IOThread.Join(); // Aguarde a thread finalizar corretamente
			Debug.Log("Thread de IO interrompida");
		}
	}
}
