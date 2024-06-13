using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRabbitHit : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.RemoveHealth();
            player.TriggerBoolAnimation(player.rabbitModelAnimator, 1.1f, "jumpGhostState");
            player.TriggerBoolAnimation(player.hurtRabbitAnimator, 1.1f, "isHurtState");


        }
    }

}
