
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscellaneousEvents : MonoBehaviour
{


    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;

    [SerializeField] GameObject transparentOverlay;
    [SerializeField] GameObject blackOverlay;

    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    public static bool isWindowed;
    public static float volume;

    private void Awake()
    {
        // Player prefs stuff
        isWindowed = PlayerPrefs.GetInt("isWindowed") != 0;
        volume = PlayerPrefs.GetFloat("volume");

        


        SFXSource.volume = volume;
        MusicSource.volume = volume;



        DontDestroyOnLoad(gameObject);
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

        PlayerPrefs.SetFloat("isWindowed", isWindowed ? 1 : 0);

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






}
