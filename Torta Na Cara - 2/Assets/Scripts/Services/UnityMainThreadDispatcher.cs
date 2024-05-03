using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly ConcurrentQueue<System.Action> _queue = new ConcurrentQueue<System.Action>();

    public static void Enqueue(System.Action action)
    {
        _queue.Enqueue(action);
    }

    private void Update()
    {
        while (_queue.TryDequeue(out var action))
        {
            action.Invoke();
        }
    }

    private static UnityMainThreadDispatcher _instance = null;

    public static UnityMainThreadDispatcher Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<UnityMainThreadDispatcher>();
            if (!_instance)
            {
                var go = new GameObject("Dispatcher");
                _instance = go.AddComponent<UnityMainThreadDispatcher>();
                DontDestroyOnLoad(go);
            }
        }
        return _instance;
    }
}
