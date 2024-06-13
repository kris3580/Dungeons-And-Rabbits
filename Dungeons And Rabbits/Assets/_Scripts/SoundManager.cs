using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    
    public static AudioSource MusicSource;
    public static AudioSource SFXSource;
    [SerializeField] public static AudioClip[] sfxClips;

    private void Awake()
    {
        MusicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        SFXSource = GameObject.Find("SFX").GetComponent<AudioSource>();
        sfxClips = new AudioClip[8];
        
        sfxClips[0] = Resources.Load<AudioClip>("amber_impact");
        sfxClips[1] = Resources.Load<AudioClip>("belle_free_jingle");
        sfxClips[2] = Resources.Load<AudioClip>("care_chime");
        sfxClips[3] = Resources.Load<AudioClip>("menu_quit");

        sfxClips[4] = Resources.Load<AudioClip>("menu_select");
        sfxClips[5] = Resources.Load<AudioClip>("step_0");
        sfxClips[6] = Resources.Load<AudioClip>("step_1");
        sfxClips[7] = Resources.Load<AudioClip>("wavey_appear");
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "Win")
        {
            Invoke("DestroySoundManager", 22.9f);
        }


        void DestroySoundManager()
        {
            Destroy(gameObject);
        }

    }
}
