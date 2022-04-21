using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private AudioManager _audioManager;

    void StepSound()
    {
        _audioManager.PlayStepSound();
    }

    void RunSound()
    {
        _audioManager.PlayRunSound();
    }
    void RoomSound()
    {
        _audioManager.PlayRoomSound();
    }
}
