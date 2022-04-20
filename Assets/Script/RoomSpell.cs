using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpell : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;
    
    public void SpawnRoom()
    {
        _ball.transform.position = transform.position;
        _ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Instantiate(_ball);
    }
}
