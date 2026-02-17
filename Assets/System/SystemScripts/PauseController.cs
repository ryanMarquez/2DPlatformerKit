using UnityEngine;
using TMPro;

public class PauseController : MonoBehaviour
{
    public GameObject pauseContainer;
    private bool isPaused = false;
    private bool pausedThisPress = false;

    private bool holdingPause = false;
    private float holdPauseStartTime = 0;
    private readonly float quitHoldTime = 2f;

    public TMP_Text pauseText;

    void Start()
    {
        pauseContainer.SetActive(false);
        #if UNITY_WEBGL
        if(pauseText != null)
        {
            pauseText.text = "<b>Paused</b>";
        }
        #endif
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if (!isPaused)
            {
                Pause();
                pausedThisPress = true;
            }
            else
            {
                holdingPause = true;
                holdPauseStartTime = Time.realtimeSinceStartup;
            }
        }

        if(Input.GetButtonUp("Pause"))
        {
            holdingPause = false;
            if (!pausedThisPress)
            {
                if (isPaused) UnPause();
            }
            else
            {
                pausedThisPress = false;
            }
        }

        if (holdingPause && Time.realtimeSinceStartup - holdPauseStartTime > quitHoldTime)
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseContainer.SetActive(true);
        AudioController.GetMixer().SetFloat("pauseVolume", -15);
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseContainer.SetActive(false);
        AudioController.GetMixer().SetFloat("pauseVolume", 0);
    }
}
