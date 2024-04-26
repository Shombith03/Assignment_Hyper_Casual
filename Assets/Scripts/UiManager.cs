using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private Text _trapText;
    [SerializeField]
    private Button _resetButton;

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameOver;
    }

    public void ShowTrapInfo(string trapName)
    {
        _trapText.text = trapName;
        StartCoroutine(RemoveTrapName());
    }

    IEnumerator RemoveTrapName()
    {
        yield return new WaitForSeconds(3f);
        _trapText.text = "";
    }

    public void GameOver()
    {
        GameManager.Instance.isGameOver = false;
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
