using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject torchParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Player.torchDuration += 15f;

            Destroy(torchParent);


        }
    }







}
