using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    internal bool isGameOver;
    internal event Action OnGameOver;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(isGameOver)
        {
            Time.timeScale = 0.1f;
            // show UI or reset
            OnGameOver?.Invoke();
        }
    }

    internal void ShowTrapName(string trapName)
    {
        FindObjectOfType<UiManager>().ShowTrapInfo(trapName);
    }
}
