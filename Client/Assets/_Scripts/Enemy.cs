
using SharedLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _velocity;
    private GameObject _head;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _velocity = Vector3.zero;
        _head = transform.GetChild(0).gameObject;
    }


    void Update()
    {
        _rb.velocity = _velocity;
    }

    public void UpdateEnemy(PlayerInfo playerInfo)
    {
        transform.position = new() { x = playerInfo.Position[0], y = playerInfo.Position[1], z = playerInfo.Position[2] };
        _velocity = new() { x = playerInfo.Velocity[0], y = playerInfo.Velocity[1], z = playerInfo.Velocity[2] };
        transform.rotation = Quaternion.Euler(0, playerInfo.Rotation[1], 0);
        _head.transform.localRotation = Quaternion.Euler(playerInfo.Rotation[0], 0, 0);
    }

}
