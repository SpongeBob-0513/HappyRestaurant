using System;
using Common;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public RoleType roleType;

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
