﻿using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{

    public Camera camera;
    public float panSpeed = 2;
    private Vector3 dragOrigin;

    private float moveSmooth = 0.1f;


    public float x, y, width, height;


    ////////
    // Awake
    void Awake()
    {
        // DontDestroyOnLoad( this ); if( FindObjectsOfType( GetType() ).Length > 1 ) Destroy( gameObject );
        ResetCamera();
    }

    ////////
    // Start
    void Start()
    {
        ResetCamera();
    }

    ////////////////
    // RESET: Camera
    public void ResetCamera()
    {
        if (camera == null) camera = Camera.main;
        //// Camera Origin
        x = 0; y = 0;
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;
        camera.rect = new Rect(x, y, width, height);
    }

    /////////
    // Update
    void Update()
    {
        if (!ManagerGraph.instance.draggingToggle.isOn)
            return;
        
        //// Moving of camera via click / drag
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            dragOrigin = camera.ScreenToWorldPoint(dragOrigin);

        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (!Physics.Raycast(ray, 20f))
                {
                    Vector3 currentPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    currentPos = camera.ScreenToWorldPoint(currentPos);
                    Vector3 movePos = dragOrigin - currentPos;
                    transform.position += (movePos * moveSmooth);
            }
        }
    }

}