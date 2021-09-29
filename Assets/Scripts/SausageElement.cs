using UnityEngine;

public class SausageElement : MonoBehaviour
{
    private GameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameController.isSausageCollidedWithPlatform = true;
        }
        if (collision.gameObject.CompareTag("FinishOfGame"))
        {
            gameController.FinishReached();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameController.isSausageCollidedWithPlatform = false;
        }
    }

    private void Update()
    {
        if(transform.position.y <= gameController.valueYToDie)
        {
            gameController.LoadGameOverCanvas();
        }
    }
}
