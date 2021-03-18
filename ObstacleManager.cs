using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    GameObject player;
    GameObject camera;
    GameObject canvas;

    public GameObject explosion;
    public GameObject explosionPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.GetComponent<Player>().tankModeEnabled)
        {
            player.GetComponent<Player>().explosionAudioSource.Play();

            camera.GetComponent<CameraShake>().ShakeIt();

            Instantiate(explosion, explosionPosition.gameObject.transform.position,
                explosionPosition.gameObject.transform.rotation);            

            canvas.GetComponent<Animator>().Play("DiamondIncrease");

            canvas.GetComponent<UIManager>().diamondValueAmount += 5;

            Destroy(gameObject);
        }
    }
}
