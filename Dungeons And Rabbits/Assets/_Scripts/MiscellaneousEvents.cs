
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscellaneousEvents : MonoBehaviour
{


    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseSettingsPanel;

    [SerializeField] GameObject pauseButtonUI;
    [SerializeField] GameObject transparentOverlay;
    [SerializeField] GameObject blackOverlay;

    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;


    public static bool isWindowed;
    public static float volume;

    private void Awake()
    {
        MusicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        SFXSource = GameObject.Find("SFX").GetComponent<AudioSource>();


        // Player prefs stuff
        isWindowed = PlayerPrefs.GetInt("isWindowed") != 0;
        volume = PlayerPrefs.GetFloat("volume");

        ApplyDisplayModeChanges();


        SFXSource.volume = volume;
        MusicSource.volume = volume;

    }



    // MENU

    public void PlayButton()
    {
        blackOverlay.SetActive(true);
        Invoke("LoadGame", 2.0f);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SettingsButton()

    {
        transparentOverlay.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void CreditsButton()

    {
        transparentOverlay.SetActive(true);
        creditsPanel.SetActive(true);
    }

    public void ExitWindowButton()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        transparentOverlay.SetActive(false);

    }


    // GENERAL

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ChangeDisplayModeButton()
    {
        isWindowed = !isWindowed;
        ApplyDisplayModeChanges();

        
        PlayerPrefs.SetInt("isWindowed", isWindowed ? 1 : 0);

    }

    private void ApplyDisplayModeChanges()
    {
        if (isWindowed)
        {
            Screen.SetResolution(Display.main.systemWidth / 2, Display.main.systemHeight / 2, !isWindowed);
        }
        else
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, isWindowed);
            
        }
        Screen.fullScreen = !isWindowed;

    }

    public void OnVolumeChange()
    {
        MusicSource.volume = volume;
        SFXSource.volume = volume;

        PlayerPrefs.SetFloat("volume", volume);
    }

    // PAUSED

    public void PauseButton()
    {
        pauseButtonUI.SetActive(false);
        transparentOverlay.SetActive(true);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseButtonUI.SetActive(true);
        transparentOverlay.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseSettings()
    {
        pausePanel.SetActive(false);
        pauseSettingsPanel.SetActive(true);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReturnToPausePanel()
    {
        pausePanel.SetActive(true);
        pauseSettingsPanel.SetActive(false);
    }

}
