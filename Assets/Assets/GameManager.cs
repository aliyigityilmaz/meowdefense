using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;

    public GameObject targetObject;

    private bool gameHasEnded = false;


    public AudioSource winSound;
    public AudioSource loseSound;


    public float timer;
    public Text timerText;
    public void Quit()
    {
        Application.Quit();
    }


    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        loseSound.Play();
        if (!gameHasEnded)
        {
            gameHasEnded = true;
        }
        Cursor.lockState = CursorLockMode.None;
    }
    public void Win()
    {
        winScreen.SetActive(true);
        winSound.Play();
        if (!gameHasEnded)
        {
            gameHasEnded = true;
        }
        Cursor.lockState = CursorLockMode.None;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null)
        {
            if (!gameHasEnded)
            {
                Lose();
            }
        }

        timer += Time.deltaTime;
        timerText.text = (120-timer).ToString("F2");
        if (timer >= 120)
        {
            if (!gameHasEnded)
            {
                Win();
            }
        }
    }
}
