using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingText : MonoBehaviour
{
    [SerializeField] private float timeTillDestroy;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        // Make the text face towards the camera by looking at the camera's position, then flipping it 180 degrees around the y-axis
        Vector3 directionToCamera = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);

        timeTillDestroy -= Time.deltaTime;

        if (timeTillDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
