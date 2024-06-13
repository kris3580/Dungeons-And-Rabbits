
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

    


    public static bool isWindowed;
    public static float volume;

    private void Awake()
    {
        
        


        isWindowed = PlayerPrefs.GetInt("isWindowed") != 0;
        volume = PlayerPrefs.GetFloat("volume");

        ApplyDisplayModeChanges();


        SoundManager.SFXSource.volume = volume;
        SoundManager.MusicSource.volume = volume;

    }


    



    // MENU

    public void PlayButton()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        blackOverlay.SetActive(true);
        Invoke("LoadGame", 0.4f);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SettingsButton()

    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        transparentOverlay.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void CreditsButton()

    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        transparentOverlay.SetActive(true);
        creditsPanel.SetActive(true);
    }

    public void ExitWindowButton()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[3]);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        transparentOverlay.SetActive(false);

    }


    // GENERAL

    void CloseGame()
    {
        Application.Quit();

    }

    public void ExitButton()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[3]);
        Invoke("CloseGame", 0.5f);
        
    }

    public void ChangeDisplayModeButton()
    {
        isWindowed = !isWindowed;
        ApplyDisplayModeChanges();
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);

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
        SoundManager.MusicSource.volume = volume;
        SoundManager.SFXSource.volume = volume;

        PlayerPrefs.SetFloat("volume", volume);
    }

    // PAUSED

    public void PauseButton()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[3]);
        pauseButtonUI.SetActive(false);
        transparentOverlay.SetActive(true);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        pauseButtonUI.SetActive(true);
        transparentOverlay.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public static void RestartLevel()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseSettings()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        pausePanel.SetActive(false);
        pauseSettingsPanel.SetActive(true);
    }

    public void ExitToMenu()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[4]);
        Time.timeScale = 1f;
        Destroy(GameObject.Find("SoundManager"));
        SceneManager.LoadScene("Menu");
        
    }

    public void ReturnToPausePanel()
    {
        SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[3]);
        pausePanel.SetActive(true);
        pauseSettingsPanel.SetActive(false);
    }

}
