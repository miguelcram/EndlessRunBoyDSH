using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    public Text textScore;
    public Button replayButton;
    public Button exitButton;

    private LevelDistance levelDistance;
    private int lastScore;
    
    // Start is called before the first frame update
    void Start()
    {
        levelDistance = FindObjectOfType<LevelDistance>();
        lastScore = levelDistance.distance;
        
        //Si se supera el record, se guarda
        int recordScore = PlayerPrefs.GetInt("RECORD");
        if(lastScore > recordScore)
        {
            PlayerPrefs.SetInt("RECORD", lastScore);
        }

        textScore.text = lastScore.ToString();

        replayButton.onClick.AddListener(ReplayGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("JuegoCentral");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}