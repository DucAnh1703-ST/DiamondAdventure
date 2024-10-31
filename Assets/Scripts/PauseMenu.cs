using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   
    

    [SerializeField] GameObject pauseMenu;
    

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

        // Tim lai am thanh trong BackgroundSound theo tag va tat no
        GameObject backgroundSound = GameObject.FindGameObjectWithTag("BackgroundSound");
        if (backgroundSound != null)
        {
            AudioSource audioSource = backgroundSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Pause();
            }
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("Start Screen");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        // Tim lai am thanh trong BackgroundSound theo tag va bat lai no
        GameObject backgroundSound = GameObject.FindGameObjectWithTag("BackgroundSound");
        if (backgroundSound != null)
        {
            AudioSource audioSource = backgroundSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.UnPause();
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
   
}

