using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMenu : MonoBehaviour
{
    public Animator rabbitModelAnimator;

    private void Update()
    {
        CheckForIdleTime();
    }
    

    private void Start()
    {
        rabbitModelAnimator = GameObject.Find("RabbitModel").GetComponent<Animator>();
    }

    public void TriggerBoolAnimation(Animator targetAnimator, float currentAnimationDuration, string boolToToggle)
    {

        targetAnimator.SetBool(boolToToggle, true);
        StartCoroutine(Redeactivate());

        IEnumerator Redeactivate()
        {

            yield return new WaitForSeconds(currentAnimationDuration);
            targetAnimator.SetBool(boolToToggle, false);
        }
    }

    float idleTime = 0;

    void CheckForIdleTime()
    {

        if (idleTime >= Random.Range(7, 12))
        {
            idleTime = 0f;
            switch (Random.Range(0, 2))
            {
                case 0:
                    TriggerBoolAnimation(rabbitModelAnimator, 2.09f, "backFlipState");

                    break;
                case 1:
                    TriggerBoolAnimation(rabbitModelAnimator, 1.12f, "idleState");

                    break;
            }
        }
        else
        {
            idleTime += Time.deltaTime;
        }
    }

}
