using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ChangeColor _changeColor;
    public UIManager _uiManager;
    public CameraAnimation _camera;
    public CameraShake _cameraShake;
    public PlayerDeath _playerDeath;

    public GameObject canvas;
    public GameObject tankPickUpCollectedEffect;
    public GameObject tankPickUpCollectedEffectPos;
    public GameObject tankForceField;
    public GameObject explosion;
    public GameObject explosionPosition;

    public AudioSource pickUpCollectedAudio;
    public AudioSource explosionAudioSource;

    public float sidewaysSpeed;
    public float forwardSpeed = 580f;
    public float swipeSpeed = 70f;

    Rigidbody rb;

    public bool gameIsRunning = false;

    public bool tankModeEnabled = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_uiManager.startButtonPressed && _camera._cameraAnimationHasFinished && !_playerDeath.playerHasDied)
        {
            //_changeColor.GetComponent<Animator>().enabled = true;

            gameIsRunning = true;            
        }

        if (gameIsRunning)
        {
            rb.AddForce(0, 0, forwardSpeed);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 playerPos = new Vector3(rb.transform.position.x + touch.deltaPosition.x * swipeSpeed, rb.transform.position.y, 0);
                    rb.AddForce(playerPos);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NearMiss"))
        {
            canvas.GetComponent<Animator>().Play("NearMissIncrease");

            _uiManager.diamondValueAmount += 5;
        }

        if (other.gameObject.CompareTag("Tank"))
        {
            Destroy(other.gameObject);
        }
    }
}
