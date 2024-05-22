using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public TextMeshProUGUI GameId;

    public Button NewGameButton;
    public Button JoinGameButton;
    
    public TMP_InputField GameIdInput;
    public Button SubmitButton;

    public Button ResumeButton;

    void Start()
    {
        GameId.gameObject.SetActive(false);
        GameIdInput.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(false);
    }

    public void ShowGameId(int id)
    {
        GameId.gameObject.SetActive(true);
        GameId.text = "Game ID: " + id;
    }

    public int GetInput()
    {
        int id = int.Parse(GameIdInput.text);
        GameIdInput.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        return id;
    }

    public void ShowInput()
    {
        HideButtons();
        GameIdInput.gameObject.SetActive(true);
        SubmitButton.gameObject.SetActive(true);
    }

    public void HideButtons()
    {
        NewGameButton.gameObject.SetActive(false);
        JoinGameButton.gameObject.SetActive(false);
    }

    public void Pause()
    {
        ResumeButton.gameObject.SetActive(true);
    }

    public void Resume()
    {
        ResumeButton.gameObject.SetActive(false);
    }
}
