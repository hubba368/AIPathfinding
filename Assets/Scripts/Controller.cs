using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{



    Camera mainCam;

    float speed = 6;
   // float zoomSpeed = 3;
    
    int scrollDist = 5;
    float scrollSpeed = 10;

    //Camera Controls
    void Update ()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        //RTS style camera movement
        if (mousePosX < scrollDist)
        {
            transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
        }
        else if(mousePosX >= Screen.width - scrollDist)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
        else if(mousePosY < scrollDist)
        {
            transform.Translate(Vector3.forward * -scrollSpeed * Time.deltaTime);
        }
        else if (mousePosY >= Screen.height - scrollDist)
        {
            transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
        }
    }

}