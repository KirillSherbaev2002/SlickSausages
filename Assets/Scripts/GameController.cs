using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject[] SausageElements;
    public GameObject Trajectory;

    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 differenceInTouch;
    [SerializeField] private Vector3 dragBeginPosition;
    [SerializeField] private Vector3 dragEndPosition;
    [SerializeField] private float movingImpulse;
    [SerializeField] private float movingImpulseMultiplayer;

    private void OnMouseDown()
    {
        dragBeginPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //dragBeginPosition
    }

    private void OnMouseUp()
    {
        dragEndPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        foreach(GameObject sausageElement in SausageElements)
        {
            sausageElement.GetComponent<Rigidbody>().AddForce(differenceInTouch * (movingImpulse * movingImpulseMultiplayer));
        }
    }

    private void OnMouseDrag()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //Get mousePosition

        differenceInTouch = dragBeginPosition - mousePosition;
        //Get differenceInTouch

        movingImpulse = Vector3.Distance(dragBeginPosition, dragEndPosition);
    }
}
