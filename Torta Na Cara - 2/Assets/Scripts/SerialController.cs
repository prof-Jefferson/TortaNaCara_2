using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialController : MonoBehaviour
{
    private static Thread IOThread;
    private static SerialPort sp;
    private static string incomingMsg = "";
    private static string outgoingMsg = "";

    private static void DataThread()
    {
        sp = new SerialPort("COM7", 9600); // Certifique-se de substituir "COM7" pelo número da porta que o Arduino está usando
        sp.Open();

        while (true)
        {
            if (outgoingMsg != "")
            {
                sp.Write(outgoingMsg);
                outgoingMsg = ""; // Limpa a mensagem após o envio
            }

            if (sp.BytesToRead > 0)
            {
                incomingMsg = sp.ReadLine();
            }

            Thread.Sleep(100); // Intervalo para reduzir a carga de trabalho
        }
    }

    private void Start()
    {
        IOThread = new Thread(new ThreadStart(DataThread));
        IOThread.Start();
    }

    private void Update()
    {
        if (incomingMsg != "")
        {
            Debug.Log("Dados recebidos: " + incomingMsg);
            incomingMsg = ""; // Limpa a mensagem após o uso
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            outgoingMsg = "0"; // Altere para enviar o dado que quiser ao Arduino
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            outgoingMsg = "1";
    }

    private void OnDestroy()
    {
        if (IOThread != null)
        {
            IOThread.Abort(); // Encerra a thread quando o jogo fecha
        }

        if (sp != null && sp.IsOpen)
        {
            sp.Close(); // Fecha a porta serial
        }
    }
}
