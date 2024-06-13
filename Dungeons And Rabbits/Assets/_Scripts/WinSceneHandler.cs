using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneHandler : MonoBehaviour
{
    [SerializeField] GameObject fireworks;

    private void Start()
    {
        Invoke("GoToMenu", 23f);
        StartCoroutine(FireworksHandler());
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator FireworksHandler()
    {
        restart:

        yield return new WaitForSeconds(Random.Range(2.5f, 3f));
        fireworks.SetActive(false);
        fireworks.SetActive(true);

        goto restart;


    }

}
