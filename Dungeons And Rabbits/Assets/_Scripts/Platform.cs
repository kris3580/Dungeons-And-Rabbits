using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float randomStartPosRangeX = -5f;
    [SerializeField] float randomStartPosRangeY = -5f;

    [SerializeField] float randomMoveDurationRangeX = 0.3f;
    [SerializeField] float randomMoveDurationRangeY = 0.5f;

    [SerializeField] float randomEndPosRangeX = -0.1f;
    [SerializeField] float randomEndPosRangeY = 0.1f;


    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(randomStartPosRangeX, randomStartPosRangeY), transform.position.z);

        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, Random.Range(randomEndPosRangeX, randomEndPosRangeY), transform.position.z);
        lerpMoveDuration = Random.Range(randomMoveDurationRangeX, randomMoveDurationRangeY);
    }

    Vector3 startPosition;
    Vector3 endPosition;
    [SerializeField] float lerpMoveDuration;
    float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / lerpMoveDuration;

        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));

    }
}
