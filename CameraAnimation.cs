using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Transform player;

    public Vector3 cameraOffSetPosition;

    public bool _cameraAnimationHasFinished = false;

    float smoothSpeed = 0.025f;

    Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        if (transform.position.y <= 10)
        {
            StartCoroutine(StopCameraAnimation());

            transform.position = player.position + cameraOffSetPosition;            
        }
        else if(transform.position.y <= 10 && _cameraAnimationHasFinished == true)
        {
            StopAllCoroutines();            

            Vector3 desiredPosition = player.GetComponent<Rigidbody>().position + cameraOffSetPosition;

            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            transform.position = smoothPosition;

            transform.LookAt(player.GetComponent<Rigidbody>().position);
        }  
    }

    IEnumerator StopCameraAnimation()
    {
        yield return new WaitForSeconds(0);

        GetComponent<Animator>().enabled = false;

        _cameraAnimationHasFinished = true;
    }
}
