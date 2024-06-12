using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepParticles : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float down;

    void Update()
    {
        transform.position = player.transform.position + Vector3.down * down;
    }
}
