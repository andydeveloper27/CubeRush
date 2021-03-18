using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPickUpManager : MonoBehaviour
{
    GameObject player;

    public GameObject tankPickUpCollectedEffect;
    public GameObject tankPickUpCollectedEffectPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().pickUpCollectedAudio.Play();

            player.GetComponent<Player>().tankModeEnabled = true;

            Instantiate(tankPickUpCollectedEffect, tankPickUpCollectedEffectPos.transform.position,
                tankPickUpCollectedEffectPos.transform.rotation);

            player.GetComponent<Player>().tankForceField.SetActive(true);
        }
    }
}
