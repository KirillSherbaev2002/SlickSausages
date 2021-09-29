using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject[] SausageElements;

    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 differenceInTouch;
    [SerializeField] private Vector3 dragBeginPosition;
    [SerializeField] private Vector3 dragEndPosition;
    [SerializeField] private float movingImpulse;
    [SerializeField] private float movingImpulseMaxValue;
    [SerializeField] private float movingImpulseMultiplayer;
    [SerializeField] private float currentMovingImpulseValue;

    public GameObject Trajectory;
    public Image[] TrajectoryElements;
    public Image SpriteTrajectoryMiddleElement;
    public Image SpriteTrajectoryEndElement;

    public GameObject Ball;
    [SerializeField] private float minBallSpeed;

    private void OnMouseDown()
    {
        dragBeginPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //dragBeginPosition
    }

    private void OnMouseUp()
    {
        dragEndPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        currentMovingImpulseValue = movingImpulse * movingImpulseMultiplayer;

        if(currentMovingImpulseValue > movingImpulseMaxValue)
        {
            currentMovingImpulseValue = movingImpulseMaxValue;
        }

        foreach (GameObject sausageElement in SausageElements)
        {
            sausageElement.GetComponent<Rigidbody>().AddForce(differenceInTouch * currentMovingImpulseValue);
        }
    }

    private void OnMouseDrag()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //Get mousePosition

        differenceInTouch = dragBeginPosition - mousePosition;
        //Get differenceInTouch

        movingImpulse = Vector3.Distance(dragBeginPosition, dragEndPosition);
        //Get impulse needed to add to Player

        print(currentMovingImpulseValue * 0.5f * Time.deltaTime);

        if(currentMovingImpulseValue * 0.5f * Time.deltaTime >= minBallSpeed)
        {
            GameObject ball = Instantiate(Ball, (SausageElements[1].transform.position + SausageElements[2].transform.position) / 2, transform.rotation);
            ball.GetComponent<Rigidbody>().AddForce(differenceInTouch * currentMovingImpulseValue * 1f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        mainCam.transform.position = new Vector3((SausageElements[1].transform.position.x + SausageElements[2].transform.position.x)/2, 
            mainCam.transform.position.y, mainCam.transform.position.z);
        //Cam follows the sausage
    }
}
