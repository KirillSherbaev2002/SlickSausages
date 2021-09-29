using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Objects")]
    public Camera mainCam;
    public GameObject[] SausageElements;

    [Header("Controllers")]
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 differenceInTouch;
    [SerializeField] private Vector3 dragBeginPosition;
    [SerializeField] private Vector3 dragEndPosition;
    [SerializeField] private float movingImpulse;
    [SerializeField] private float movingImpulseMaxValue;
    [SerializeField] private float movingImpulseMultiplayer;
    [SerializeField] private float currentMovingImpulseValue;

    [Header("Trajectory")]
    public GameObject Ball;
    [SerializeField] private float minBallSpeed;
    public bool isSausageCollidedWithPlatform;
    [SerializeField] private float impulseMultiplayerForBall;

    [Header("GameOver")]
    public GameObject GameOverCanvas;
    public float valueYToDie;

    private void OnMouseDown()
    {
        dragBeginPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //dragBeginPosition
    }

    private void OnMouseUp()
    {
        if (isSausageCollidedWithPlatform)
        {
            dragEndPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            currentMovingImpulseValue = movingImpulse * movingImpulseMultiplayer;

            if (currentMovingImpulseValue > movingImpulseMaxValue)
            {
                currentMovingImpulseValue = movingImpulseMaxValue;
            }

            foreach (GameObject sausageElement in SausageElements)
            {
                sausageElement.GetComponent<Rigidbody>().AddForce(differenceInTouch * currentMovingImpulseValue);
            }
        }
    }

    private void OnMouseDrag()
    {
        if (isSausageCollidedWithPlatform)
        {
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //Get mousePosition

            differenceInTouch = dragBeginPosition - mousePosition;
            //Get differenceInTouch

            movingImpulse = Vector3.Distance(dragBeginPosition, dragEndPosition);
            //Get impulse needed to add to Player

            if (impulseMultiplayerForBall * currentMovingImpulseValue * Time.deltaTime >= minBallSpeed)
            {
                GameObject ball = Instantiate(Ball, (SausageElements[1].transform.position + SausageElements[2].transform.position) / 2, transform.rotation);
                ball.GetComponent<Rigidbody>().AddForce(differenceInTouch * currentMovingImpulseValue * 1f * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        mainCam.transform.position = new Vector3((SausageElements[1].transform.position.x + SausageElements[2].transform.position.x)/2, 
            mainCam.transform.position.y, mainCam.transform.position.z);
        //Cam follows the sausage
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
        GameOverCanvas.SetActive(false);
    }

    public void LoadGameOverCanvas()
    {
        GameOverCanvas.SetActive(true);
    }
}
