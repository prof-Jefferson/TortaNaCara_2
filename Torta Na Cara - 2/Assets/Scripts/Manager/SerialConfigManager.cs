using System.IO;
using UnityEngine;

public class SerialConfigManager : MonoBehaviour
{
    private static SerialConfigManager _instance;
    public static SerialConfigManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SerialConfigManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(nameof(SerialConfigManager));
                    _instance = singleton.AddComponent<SerialConfigManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    private string serialPort;
    private const string ConfigPath = "/Resources/SerialConfig.json";

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        LoadConfig();
    }

    private void LoadConfig()
    {
        string path = Application.dataPath + ConfigPath;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerialConfig config = JsonUtility.FromJson<SerialConfig>(json);
            serialPort = config.serialPort;
        }
    }

    private void SaveConfig()
    {
        string path = Application.dataPath + ConfigPath;
        SerialConfig config = new SerialConfig(serialPort);
        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(path, json);
    }

    public void SetSerialPort(string port)
    {
        serialPort = port;
        SaveConfig();
    }

    public string GetSerialPort()
    {
        return serialPort;
    }
}