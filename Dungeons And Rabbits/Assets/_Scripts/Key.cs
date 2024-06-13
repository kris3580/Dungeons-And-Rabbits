using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Animator animator;
    [SerializeField] Door doorToUnlock;
    [SerializeField] GameObject touchParticles;
    [SerializeField] GameObject vanishParticles;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            SoundManager.SFXSource.PlayOneShot(SoundManager.sfxClips[7]);
            doorToUnlock.Open();
            touchParticles.SetActive(true);
            animator.SetBool("wasTaken", true);
            Invoke("SetOffKey", animator.runtimeAnimatorController.animationClips[1].length);
        }
    }

    void SetOffKey()
    {
        vanishParticles.SetActive(true);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }



}
