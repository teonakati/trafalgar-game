using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollision : MonoBehaviour
{

    private GameObject player;
    public bool playerInRoom { get; private set; } = false;
    private CharacterController controller;
    private GameObject other;
    private AudioManager audioManager;
    private PlayerAnimationController playerAnimation;
    private Camera cam;
    private GameObject crosshair;

    public LayerMask layer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        controller = player.GetComponent<CharacterController>();
        audioManager = FindObjectOfType<AudioManager>();
        playerAnimation = FindObjectOfType<PlayerAnimationController>();
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var position = new Vector3(0.5f, 0.75f, 0f);
            var ray = cam.ViewportPointToRay(position);

            var maxDistance = 20f;
            if (Physics.Raycast(ray, out var hit, maxDistance, layer))
            {
                crosshair.transform.position = cam.WorldToScreenPoint(hit.point); //remover dps
                Debug.Log($"Hit something: {hit.transform.name}");

            }
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

    public void Teleport()
    {
        audioManager.PlayTeleportSound();
        var otherPosition = other.gameObject.transform.position;
        var newPlayerPosition = new Vector3(otherPosition.x, otherPosition.y, otherPosition.z);
        other.gameObject.transform.position = player.gameObject.transform.position;

        //was necessary to disable character controller in order to teleport the player
        controller.enabled = false;
        player.transform.position = newPlayerPosition;
        controller.enabled = true;
    }
}
