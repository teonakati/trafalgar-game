using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpell : MonoBehaviour
{
    [SerializeField]
    private GameObject _ballPrefab;
    [SerializeField]
    private GameObject _ballPrefabInternal;
    private GameObject _roomExternal;
    private GameObject _roomInternal;
    private float _maxScale = 20f;

    public float _scaleSpeed = 0f;

    void Start()
    {
    }

    public void SpawnRoom()
    {
        var position = transform.position;
        var scale = new Vector3(0.1f, 0.1f, 0.1f);
        _ballPrefab.transform.position = position;
        _ballPrefab.transform.localScale = scale;
        _ballPrefabInternal.transform.position = position;
        _ballPrefabInternal.transform.localScale = scale;
        _roomExternal = Instantiate(_ballPrefab);
        _roomInternal = Instantiate(_ballPrefabInternal);
    }

    void Update()
    {
        DisableRoomIfActive();
        FollowMovement();
        ScaleRoom();
    }

    void FollowMovement()
    {
        if (_roomExternal != null && _roomInternal != null && _scaleSpeed == 0f)
        {
            var position = transform.position;
            _roomInternal.transform.position = position;
            _roomExternal.transform.position = position;
        }
    }
    
    void DisableRoomIfActive()
    {
        if (Input.GetKey(KeyCode.R) && _roomExternal != null)
        {
            Destroy(_roomExternal);
            Destroy(_roomInternal);
            _scaleSpeed = 0f;
        }
    }

    void ScaleRoom()
    {
        if (_roomExternal != null && _roomExternal.scene.IsValid() && _scaleSpeed > 0f)
        {
            var currentScale = _roomExternal.transform.localScale;

            if (currentScale.x < _maxScale)
            {
                _roomExternal.transform.localScale += new Vector3(currentScale.x + _scaleSpeed * Time.deltaTime, currentScale.y + _scaleSpeed * Time.deltaTime, currentScale.z + _scaleSpeed * Time.deltaTime).normalized;
                _roomInternal.transform.localScale += new Vector3(currentScale.x + _scaleSpeed * Time.deltaTime, currentScale.y + _scaleSpeed * Time.deltaTime, currentScale.z + _scaleSpeed * Time.deltaTime).normalized;
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
        _scaleSpeed = value;
    }
}
