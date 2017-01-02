using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public enum RotationAxes {  MouseXandY = 0, MouseX = 1, MouseY = 2}
    public RotationAxes axes = RotationAxes.MouseXandY;

    float speed = 15f;
    float rotateY = 0f;   
    float scrollSpeed = 10f;
    
    

    //Camera Controls
    void KeyboardInput()
    {
        //WASD camera movement
        if (Input.GetKey(KeyCode.A))
        {
            //left
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //right
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //down
            transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //up
            transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
        }
    }

    void MouseControl()
    {
        //Controls view of camera in first person    

        if (axes == RotationAxes.MouseXandY)
        {
            //if both axes are being moved/rotated
            float rotateX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speed;   //rotate X axis

            rotateY += Input.GetAxis("Mouse Y") * speed;
            rotateY = Mathf.Clamp(rotateY, -60f, 60f);   //clamp to stop from looping all the way around

            transform.localEulerAngles = new Vector3(-rotateY, rotateX, 0);   //rotate Y axis
        }
        else if (axes == RotationAxes.MouseX)
        {
            //rotate X axis
            transform.Rotate(0, Input.GetAxis("Mouse X") * speed, 0);
        }
        else
        {
            //rotateY Y axis
            rotateY += Input.GetAxis("Mouse Y") * speed;
            rotateY = Mathf.Clamp(rotateY, -60f, 60f);

            transform.localEulerAngles = new Vector3(-rotateY, transform.localEulerAngles.y, 0);
        }

        //camera zooming - Not needed due to FPS camera control
      /*  float scroll = Input.GetAxis("Mouse ScrollWheel");
        float camDist = 10f;

        camDist += scroll * scrollSpeed;
        camDist = Mathf.Clamp(camDist, 5f, 20f);*/
    }


    void Update ()
    {
        MouseControl();
        KeyboardInput();
    }

}