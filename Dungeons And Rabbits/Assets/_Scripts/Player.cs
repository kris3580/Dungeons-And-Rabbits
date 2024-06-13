using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    [SerializeField] float moveDelay = 0.5f;
    [SerializeField] float rayLength;
    public bool isMoveAvailable = true;

    private void Update()
    {
        PlayerInput();
        MovePlayer();
        DrawRays();
        TorchHandler();
        CheckForIdleTime();

    }

    bool checkForWallForward, checkForWallBack, checkForWallLeft, checkForWallRight;
    bool checkForSpikesForward, checkForSpikesFBack, checkForSpikesRight, checkForSpikesLeft;

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

        // Debug.Log($"{checkForWallForward}, {checkForWallBack}, {checkForWallLeft}, {checkForWallRight}");

    }


    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && isMoveAvailable)
        {
            Movement("forward");
        }
        else if (Input.GetKeyDown(KeyCode.S) && isMoveAvailable)
        {
            Movement("back");
        }
        else if (Input.GetKeyDown(KeyCode.A) && isMoveAvailable)
        {
            Movement("left");
        }
        else if (Input.GetKeyDown(KeyCode.D) && isMoveAvailable)
        {
            Movement("right");
        }
    }


    void Movement(string desiredDirection)
    {
        isMoveAvailable = false;
        startPosition = transform.position;
        startRotation = transform.rotation;
        idleTime = 0;

        TriggerBoolAnimation(rabbitModelAnimator, 1.13f / 5, "isJumpingState");
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


    int health = 5;
    [SerializeField] Image[] heartSprites = new Image[5];
    [SerializeField] Sprite emptyHeartSprite;
    [SerializeField] GameObject healthPanel;

    public void RemoveHealth()
    {
        health--;
        heartSprites[health].sprite = emptyHeartSprite;

        healthPanel.GetComponent<Animation>().Play();
        

    }


    public static float torchAmount = 0f;
    MeshRenderer handTorchMesh;
    GameObject handTorchParticles;

    void TorchHandler()
    {
        

        if (torchAmount > 0f)
        {
            handTorchMesh.enabled = true;
            handTorchParticles.SetActive(true);
            torchAmount -= Time.deltaTime;
        }
        else 
        {
            handTorchMesh.enabled = false;
            handTorchParticles.SetActive(false);
        }
    }
    


    private void Start()
    {
        handTorchMesh = GameObject.Find("HandTorch").GetComponent<MeshRenderer>();
        handTorchParticles = GameObject.Find("HandTorch");

        rabbitModelAnimator = GameObject.Find("RabbitModel").GetComponent<Animator>();

        Invoke("EnableMovement", moveDelay);
    }


    public Animator rabbitModelAnimator;

    public void TriggerBoolAnimation(Animator targetAnimator, float currentAnimationDuration, string boolToToggle)
    {
        
        targetAnimator.SetBool(boolToToggle, true);
        StartCoroutine(Redeactivate());

        IEnumerator Redeactivate()
        {
            Debug.Log(1);
            yield return new WaitForSeconds(currentAnimationDuration);
            targetAnimator.SetBool(boolToToggle, false);
        }
    }

    float idleTime = 0;

    void CheckForIdleTime()
    {

        if(idleTime >= Random.Range(7,12) && isMoveAvailable)
        {
            idleTime = 0f;
            isMoveAvailable = false;
            switch (Random.Range(0, 2))
            {
                case 0:
                    TriggerBoolAnimation(rabbitModelAnimator, 2.09f, "backFlipState");

                    break;
                case 1:
                    TriggerBoolAnimation(rabbitModelAnimator, 1.12f, "idleState");

                    break;
            }
            isMoveAvailable = true;
        }
        else
        {
            idleTime += Time.deltaTime;
        }
    }


    public Animator hurtRabbitAnimator;


    

}
