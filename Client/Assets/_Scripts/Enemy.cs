
using SharedLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 _velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _velocity = Vector3.zero;
    }


    void Update()
    {
        rb.velocity = _velocity;
    }

    public void UpdateEnemy(PlayerInfo playerInfo)
    {
        rb.position = new() { x = playerInfo.Position[0], y = playerInfo.Position[1], z = playerInfo.Position[2] };
        _velocity = new() { x = playerInfo.Velocity[0], y = playerInfo.Velocity[1], z = playerInfo.Velocity[2] };
    }

}
