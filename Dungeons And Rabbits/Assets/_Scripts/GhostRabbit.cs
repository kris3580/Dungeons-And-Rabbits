using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRabbit : MonoBehaviour
{
    [SerializeField] float moveDelay = 0.5f;
    [SerializeField] float rayLength;
    bool isMoveAvailable = true;

    private void Update()
    {
        MovePlayer();
        DrawRays();

    }

    bool checkForWallForward, checkForWallBack, checkForWallLeft, checkForWallRight;
    bool checkForSpikesForward, checkForSpikesFBack, checkForSpikesRight, checkForSpikesLeft;
    bool checkForPlayerForward, checkForPlayerBack, checkForPlayerLeft, checkForPlayerRight;


    void DrawRays()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.forward * rayLength, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.left * rayLength, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right * rayLength, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.back * rayLength, Color.magenta);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Vector3.down * rayLength, Color.gray);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Vector3.down * rayLength, Color.gray);
        Debug.DrawRay(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Vector3.down * rayLength, Color.gray);
        Debug.DrawRay(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Vector3.down * rayLength, Color.gray);

        checkForWallForward = Physics.Raycast(transform.position, Vector3.forward, rayLength, LayerMask.GetMask("Wall"));
        checkForWallBack = Physics.Raycast(transform.position, Vector3.back, rayLength, LayerMask.GetMask("Wall"));
        checkForWallLeft = Physics.Raycast(transform.position, Vector3.left, rayLength, LayerMask.GetMask("Wall"));
        checkForWallRight = Physics.Raycast(transform.position, Vector3.right, rayLength, LayerMask.GetMask("Wall"));

        checkForSpikesForward = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Vector3.down, rayLength, LayerMask.GetMask("Spikes"));
        checkForSpikesFBack = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Vector3.down, rayLength, LayerMask.GetMask("Spikes"));
        checkForSpikesRight = Physics.Raycast(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Vector3.down, rayLength, LayerMask.GetMask("Spikes"));
        checkForSpikesLeft = Physics.Raycast(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Vector3.down, rayLength, LayerMask.GetMask("Spikes"));


        checkForPlayerForward = Physics.Raycast(transform.position, Vector3.forward, rayLength, LayerMask.GetMask("Player"));
        checkForPlayerBack = Physics.Raycast(transform.position, Vector3.back, rayLength, LayerMask.GetMask("Player"));
        checkForPlayerLeft = Physics.Raycast(transform.position, Vector3.left, rayLength, LayerMask.GetMask("Player"));
        checkForPlayerRight = Physics.Raycast(transform.position, Vector3.right, rayLength, LayerMask.GetMask("Player"));
    }


    void Movement(string desiredDirection)
    {
        isMoveAvailable = false;
        startPosition = transform.position;
        startRotation = transform.rotation;

        Invoke("EnableMovement", moveDelay);
        if (desiredDirection == "forward")
        {
            endRotation = Quaternion.Euler(0f, 0f, 0f);
            endPosition = startPosition + Vector3.forward;
        }
        else if (desiredDirection == "back")
        {
            endRotation = Quaternion.Euler(0f, 180f, 0f);
            endPosition = startPosition + Vector3.back;
        }
        else if (desiredDirection == "left")
        {
            endRotation = Quaternion.Euler(0f, -90f, 0f);
            endPosition = startPosition + Vector3.left;
        }
        else if (desiredDirection == "right")
        {
            endRotation = Quaternion.Euler(0f, 90f, 0f);
            endPosition = startPosition + Vector3.right;
        }
    }

    Vector3 startPosition;
    Vector3 endPosition;

    Quaternion startRotation;
    Quaternion endRotation;

    [SerializeField] float lerpMoveDuration = 0.5f;
    float elapsedTime;

    void MovePlayer()
    {
        if (!isMoveAvailable)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / lerpMoveDuration;

            transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, Mathf.SmoothStep(0, 1, percentageComplete));
        }
        else
        {
            elapsedTime = 0f;
        }
    }
    void EnableMovement()
    {
        isMoveAvailable = true;
    }


    private void Start()
    {
        StartCoroutine(AIMovement());
    }


    [SerializeField] float randomAIWaitTimeX = 3f;
    [SerializeField] float randomAIWaitTimeY = 7f;


    IEnumerator AIMovement()
    {
        loop:

        yield return new WaitForSeconds(Random.Range(randomAIWaitTimeX, randomAIWaitTimeY));
        while (true)
        {

            string[] moveTypes = { "forward", "back", "left", "right" };
            int movementNumber = Random.Range(0, 4);
            

            if (checkForPlayerForward)
            {
                Movement(moveTypes[0]);
                break;
            }
            else if (checkForPlayerBack)
            {
                Movement(moveTypes[1]);
                break;
            }
            else if (checkForPlayerLeft)
            {
                Movement(moveTypes[2]);
                break;
            }
            else if (checkForPlayerRight)
            {
                Movement(moveTypes[3]);
                break;
            }
            else if (!checkForWallForward && movementNumber == 0)
            {
                Movement(moveTypes[0]);
                break;
            }
            else if (!checkForWallBack && movementNumber == 1)
            {
                Movement(moveTypes[1]);
                break;
            }
            else if (!checkForWallLeft && movementNumber == 2)
            {
                Movement(moveTypes[2]);
                break;
            }
            else if (!checkForWallRight&& movementNumber == 3)
            {
                Movement(moveTypes[3]);
                break;
            }

        }

        
        goto loop;
    }
}
