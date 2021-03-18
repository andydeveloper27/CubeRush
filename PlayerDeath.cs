using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public CameraShake _cameraShake;
    public Player _player;
    public UIManager _uiManager;

    public GameObject canvas;

    public AudioSource playerDeathSound;

    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    public bool playerHasDied = false;

    // Use this for initialization
    void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle") && !GetComponent<Player>().tankModeEnabled)
        {
            _uiManager.runtimeAudioSource.Stop();
            playerDeathSound.Play();

            _cameraShake.ShakeIt();

            explode();

            playerHasDied = true;
            GetComponent<Player>().gameIsRunning = false;

            canvas.GetComponent<Animator>().Play("EndGameAnimation");

            _player.tankModeEnabled = false;
            _uiManager.pickUpText.gameObject.SetActive(false);
            _uiManager.pickUpTimer.gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            _uiManager.runtimeAudioSource.Stop();
            playerDeathSound.Play();

            _cameraShake.ShakeIt();

            explode();

            playerHasDied = true;
            GetComponent<Player>().gameIsRunning = false;

            canvas.GetComponent<Animator>().Play("EndGameAnimation");

            _player.tankModeEnabled = false;
            _uiManager.pickUpText.gameObject.SetActive(false);
            _uiManager.pickUpTimer.gameObject.SetActive(false);
        }
    }

    

    public void explode()
    {
        gameObject.SetActive(false);

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        Vector3 explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    void createPiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;

        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
}
