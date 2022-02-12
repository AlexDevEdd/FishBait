using UnityEngine;

public class MovementUnit : MonoBehaviour
{
    [Tooltip("Скорость движения по оси Х")]
    [SerializeField] private float _speedMoveX = 2f;
    [Tooltip("Скорость движения по оси Y")]
    [SerializeField] private float _speedMoveY = 0.5f;
    [Tooltip("Отклонение объекта от стартовой позиции по оси Y")]
    [SerializeField] private float _offsetY = 1.5f;

    [HideInInspector] private bool IsTurn = false;

    private float _offsetX = 0.5f;
    private Vector3 leftBot;
    private Vector3 rightTop;
    private Vector3 velocityX;
    private Vector3 velocityY;
    private Vector3 startPosition;
    private int _direction;

    private void Awake()
    {
        _direction = Random.Range(1, 2);
    }
    void Start()
    {
        Camera cam = Camera.main;
        startPosition = transform.position;

        Vector3 cameraToObject = transform.position - cam.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;

        velocityY = Vector3.up * _speedMoveY;

        leftBot = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        if (_direction == 0)
        {
            transform.transform.Rotate(0, -90, 0);
            velocityX = Vector3.left * _speedMoveX;
        }
        if (_direction == 1)
        {
            transform.transform.Rotate(0, 90, 0);
            velocityX = Vector3.right * _speedMoveX;
            IsTurn = true;
        }
    }

    private void Update()
    {
        if (transform.position.y > startPosition.y + _offsetY)
            velocityY.y = -_speedMoveY;
        else if (transform.position.y < startPosition.y - _offsetY)
            velocityY.y = _speedMoveY;

        transform.position += velocityY * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (transform.position.x - _offsetX <= leftBot.x)
        {
            if (IsTurn == false)
            {
                transform.transform.Rotate(0, -180, 0);
                velocityX.x = _speedMoveX;
                IsTurn = true;
            }
        }

        else if (transform.position.x + _offsetX >= rightTop.x)
        {
            if (IsTurn == true)
            {
                transform.transform.Rotate(0, 180, 0);
                velocityX.x = -_speedMoveX;
                IsTurn = false;
            }
        }

        transform.position += velocityX * Time.deltaTime;
    }
}


