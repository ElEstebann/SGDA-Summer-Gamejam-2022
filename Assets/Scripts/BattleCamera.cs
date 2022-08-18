using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform[] foci = new Transform[5];
    private Vector3 position;
    private Camera cam;
    private float zoom;
    [SerializeField]
    private float moveEase;
    [SerializeField]
    private float zoomBuffer;
    [SerializeField]
    private float zoomEase;
    [SerializeField]
    private float minZoom;
    void Start()
    {
        position = transform.position;
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        position = FindFoci();
        zoom = FindZoom(position) + zoomBuffer;
        if(zoom < minZoom)
        {
            zoom = minZoom;
        }
        transform.position = Vector3.Lerp(transform.position,position,Time.deltaTime/moveEase);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,zoom,Time.deltaTime/zoomEase);
        
    }

    private Vector3 FindFoci()
    {
        position = Vector3.zero;
        

        for(int i= 0; i < 4; i++)
        {
            position += foci[i].position;


        }
        position /= 4;
        position += foci[4].position;
        position /= 2;
        position.z = transform.position.z;
        return position;

    }

    private float FindZoom(Vector3 position)
    {
        float maxX = 0;
        float maxY = 0;


        foreach(Transform x in foci)
        {
            if(Mathf.Abs(x.position.x - position.x) > maxX)
            {
                maxX = Mathf.Abs(x.position.x - position.x);
            }
            if(Mathf.Abs(x.position.y - position.y) > maxY)
            {
                maxY = Mathf.Abs(x.position.y - position.y);
            }
        }
        //Debug.Log(cam.aspect);
        //maxX *= Screen.width/Screen.height;

        if(maxX/cam.aspect > maxY)
        {
            return maxX/cam.aspect;
        }
        else
        {
            return maxY;
        }

    }
}
