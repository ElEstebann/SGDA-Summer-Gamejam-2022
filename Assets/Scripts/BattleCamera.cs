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
    [SerializeField]
    private int focusIndex  = 4;
    [SerializeField]
    public int focusModifier = 1;
    private GameManager GM;

    private Vector3 originalPosition;
    private float originalZoom;
    private int originalFocusIndex;
    void Start()
    {
        position = transform.position;
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
        originalZoom = zoom;
        originalFocusIndex = focusIndex;
        originalPosition = transform.position;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.OnGameOver += SetWinnerFocus;
        GameManager.OnGameRestart += Restart;
    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= SetWinnerFocus;
        GameManager.OnGameRestart -= Restart;
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
        Vector3 averagePosition = Vector3.zero;
        

        for(int i= 0; i < 5; i++)
        {
            if(i == focusIndex)
            {
                averagePosition += foci[i].position*focusModifier;
            }
            else
            {
                averagePosition += foci[i].position;
            }


        }
        averagePosition = averagePosition/(4 + focusModifier);
        averagePosition.z = transform.position.z;
        return averagePosition;

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

    private void SetFocus(int index)
    {
        focusIndex = index;
    }

    private void SetWinnerFocus()
    {
        SetFocus(GM.roundWinner.playerIndex-1);
    }

    private void Restart()
    {
        transform.position = originalPosition;
        cam.orthographicSize = originalZoom;
        SetFocus(originalFocusIndex);
        
    }
}
