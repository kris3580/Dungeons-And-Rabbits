using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    GameObject doorWall;
    GameObject door;

    public void Open()
    {
        door.GetComponent<Animation>().Play();
        doorWall.layer = 0;
        door.layer = 0;
    }





    void Start()
    {
        doorWall = transform.Find("DoorWall").gameObject;
        door = transform.Find("Door").gameObject;
    }


}
