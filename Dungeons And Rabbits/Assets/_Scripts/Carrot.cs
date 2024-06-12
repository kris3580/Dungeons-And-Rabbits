using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Carrot : MonoBehaviour
{


    Animator animator;
    [SerializeField] GameObject touchParticles;
    [SerializeField] GameObject vanishParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            touchParticles.SetActive(true);
            animator.SetBool("wasTaken", true);


            Invoke("SetOffCarrot", animator.runtimeAnimatorController.animationClips[1].length);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    void SetOffCarrot()
    {
        

        vanishParticles.SetActive(true);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        Invoke("NextScene", 3f);
    }

    void NextScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level 1":
                SceneManager.LoadScene("Level 2");
                break;


            case "Level 2":
                SceneManager.LoadScene("Win");
                
                break;
        } 
    }



    }


