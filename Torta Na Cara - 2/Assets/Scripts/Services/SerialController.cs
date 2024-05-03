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

    private static void DataThread()
    {
        sp = new SerialPort("COM7", 9600);
        sp.Open();

        Debug.Log("Porta Serial aberta");

        while (true)
        {
            if (outgoingMsg != "")
            {
                Debug.Log("Enviando: " + outgoingMsg);
                sp.Write(outgoingMsg);
                outgoingMsg = "";
            }

            if (sp.BytesToRead > 0)
            {
                incomingMsg = sp.ReadLine();
                Debug.Log("Recebido: " + incomingMsg);
                UnityMainThreadDispatcher.Enqueue(() =>
                {
                    OnMessageReceived?.Invoke(incomingMsg);
                });
            }

            Thread.Sleep(100);
        }
    }

    private void Start()
    {
        IOThread = new Thread(new ThreadStart(DataThread));
        IOThread.Start();
    }

    public void Send(string message)
    {
        outgoingMsg = message;
        Debug.Log("Mensagem para enviar enfileirada: " + message);
    }

    private void OnDestroy()
    {
        if (IOThread != null)
        {
            IOThread.Abort();
            Debug.Log("Thread de IO interrompida");
        }

        if (sp != null && sp.IsOpen)
        {
            sp.Close();
            Debug.Log("Porta Serial fechada");
        }
    }
}
