using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceen : MonoBehaviour
{
    public Text textHihgscore;
    public static int highscore;

    public Button playButton;
    public Button exitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("RECORD");
        textHihgscore.text = highscore.ToString();

        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("JuegoCentral");
    }

    public void ExitGame()
    {
        Debug.Log("Exit pulsado");
        Application.Quit();
    }
}
