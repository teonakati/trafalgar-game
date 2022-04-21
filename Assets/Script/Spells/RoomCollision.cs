using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollision : MonoBehaviour
{

    private GameObject player;
    private bool playerInRoom = false;
    private CharacterController controller;
    private GameObject other;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRoom)
        {
            var otherPosition = other.gameObject.transform.position;
            var newPlayerPosition = new Vector3(otherPosition.x, otherPosition.y, otherPosition.z);
            other.gameObject.transform.position = player.gameObject.transform.position;

            //was necessary to disable character controller in order to teleport the player
            controller.enabled = false;
            player.transform.position = newPlayerPosition;
            controller.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRoom = true;
        }

        if (other.gameObject.tag == "NPC")
        {
            this.other = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRoom = false;
        }
    }
}
