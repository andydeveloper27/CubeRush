using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : MonoBehaviour
{
    GameObject _uiManager;
    public GameObject effect;
    GameObject gemPickUpAudio;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("MainCanvas");
        gemPickUpAudio = GameObject.FindGameObjectWithTag("GemAudioSource");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gemPickUpAudio.GetComponent<AudioSource>().Play();

            _uiManager.GetComponent<UIManager>().diamondValueAmount += 1;

            Instantiate(effect, transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
