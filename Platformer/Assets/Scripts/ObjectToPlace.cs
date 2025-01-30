using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ObjectToPlace : MonoBehaviour
{
    private Rigidbody2D rigidbody2dPrefabs;
    private void Awake()
    {
        rigidbody2dPrefabs = GetComponent<Rigidbody2D>();
        rigidbody2dPrefabs.isKinematic = true;
        StartCoroutine(CancelRigidBody2D());
    }

    IEnumerator CancelRigidBody2D()
    {
        yield return new WaitForSeconds(5);
        rigidbody2dPrefabs.isKinematic = false;
    }
}
