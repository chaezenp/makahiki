// Code originally used by David Wood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject spear;
    public float offsetSmoothing;
    private Vector3 spearPosition;

    void Update()
    {
        spearPosition = new Vector3(spear.transform.position.x, spear.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, spearPosition, offsetSmoothing * Time.deltaTime);
    }   
}