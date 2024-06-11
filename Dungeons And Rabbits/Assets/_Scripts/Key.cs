using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject doorToUnlock;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            animator.SetBool("wasTaken", true);
            Invoke("SetOffKey", animator.runtimeAnimatorController.animationClips[1].length);
        }
    }

    void SetOffKey()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }



}
