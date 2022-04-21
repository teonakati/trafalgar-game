using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpell : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject ballPrefabInternal;
    private GameObject roomExternal;
    private GameObject roomInternal;
    private float maxScale = 20f;

    public float scaleSpeed = 0f;

    void Start()
    {
    }

    public void SpawnRoom()
    {
        var position = transform.position;
        var scale = new Vector3(0.1f, 0.1f, 0.1f);
        ballPrefab.transform.position = position;
        ballPrefab.transform.localScale = scale;
        ballPrefabInternal.transform.position = position;
        ballPrefabInternal.transform.localScale = scale;
        roomExternal = Instantiate(ballPrefab);
        roomInternal = Instantiate(ballPrefabInternal);
    }

    void Update()
    {
        DisableRoomIfActive();
        FollowMovement();
        ScaleRoom();
    }

    void FollowMovement()
    {
        if (roomExternal != null && roomInternal != null && scaleSpeed == 0f)
        {
            var position = transform.position;
            roomInternal.transform.position = position;
            roomExternal.transform.position = position;
        }
    }
    
    void DisableRoomIfActive()
    {
        if (Input.GetKey(KeyCode.R) && roomExternal != null)
        {
            Destroy(roomExternal);
            Destroy(roomInternal);
            scaleSpeed = 0f;
        }
    }

    void ScaleRoom()
    {
        if (roomExternal != null && roomExternal.scene.IsValid() && scaleSpeed > 0f)
        {
            var currentScale = roomExternal.transform.localScale;

            if (currentScale.x < maxScale)
            {
                roomExternal.transform.localScale += new Vector3(currentScale.x + scaleSpeed * Time.deltaTime, currentScale.y + scaleSpeed * Time.deltaTime, currentScale.z + scaleSpeed * Time.deltaTime).normalized;
                roomInternal.transform.localScale += new Vector3(currentScale.x + scaleSpeed * Time.deltaTime, currentScale.y + scaleSpeed * Time.deltaTime, currentScale.z + scaleSpeed * Time.deltaTime).normalized;
            }
        }
    }

    public void IncreaseScaleSpeed(float value, float seconds)
    {
        StartCoroutine(IncreaseSpeedAfterWait(seconds, value));
    }

    IEnumerator IncreaseSpeedAfterWait(float seconds, float value)
    {
        yield return new WaitForSeconds(seconds + 0.1f);
        scaleSpeed = value;
    }
}
