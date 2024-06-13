using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject torchParent;
    public static float torchDurationLimit = 20f;
    public static float torchDuration = 0f;


    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[3]);

            Player.torchAmount += torchDurationLimit;
            Mathf.Clamp(Player.torchAmount, 0 , torchDurationLimit);
            Destroy(torchParent);


        }
    }
    






}
