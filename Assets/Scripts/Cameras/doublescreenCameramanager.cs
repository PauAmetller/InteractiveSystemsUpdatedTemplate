﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublescreenCameramanager : MonoBehaviour
{
    //Vector3 shiftingCamera1 = new Vector3(0f, 71, 0.6f);
    //Vector3 shiftingCamera2 = new Vector3(0f, 71, -0.6f);


    public Color backgroundColor;
    /// <summary>
    /// This script just open the two screens on the same allication
    /// </summary>
    void Awake()
    {
        //NB: screen indexes start from 1
        for (int i = 0; i < GameObject.FindObjectsOfType<Camera>().Length; i++)
        {
            if (i < Display.displays.Length)
            {
                Display.displays[i].Activate();
            }
        }

        Camera cam1 = transform.GetChild(0).GetComponent<Camera>();
        Camera cam2 = transform.GetChild(1).GetComponent<Camera>();

        float originalAspect = 9f / 16f;
        float targetAspect = 9.11f / 16f;

        float scaleFactor = targetAspect / originalAspect;

        Matrix4x4 projectionMatrix = cam1.projectionMatrix;

        projectionMatrix.m11 *= scaleFactor;

        cam1.projectionMatrix = projectionMatrix;
        cam2.projectionMatrix = projectionMatrix;

        transform.GetChild(0).GetComponent<Camera>().backgroundColor = backgroundColor;
        transform.GetChild(1).GetComponent<Camera>().backgroundColor = backgroundColor;
    }

}
