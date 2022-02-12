using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class TapController : MonoBehaviour
{
    private const float MAX_RANDOM_ANGLE = 360f;
    private const float MIN_RANDOM_ANGLE = 0f;

    [SerializeField] private MMFeedbackRotation _feedbackRotation;

    [Range(-3, 0)] [HideInInspector]
    [SerializeField] private float _randomDistanceLeft = -1.5f; 
    [Range(0, 3)] [HideInInspector]
    [SerializeField] private float _randomDistanceRight = 1.5f;
    [Tooltip("Сила прыжка по оси Y")]
    [SerializeField] private float _jumpValue = 2f;

    [Space(10)]
    [Header("FEEL ивенты")]
    public UnityEvent OnRotate;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GameObject.FindGameObjectWithTag("Food").GetComponent<Rigidbody>();      
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
           // RotateCube();
            ChangePosition();
        }
    }
    private void RotateCube()
    {
        var rotation = new Vector3(Random.Range(MIN_RANDOM_ANGLE, MAX_RANDOM_ANGLE), Random.Range(0, MAX_RANDOM_ANGLE), Random.Range(0, MAX_RANDOM_ANGLE));
        _feedbackRotation.DestinationAngles = rotation;
       OnRotate?.Invoke();
    }

    private void ChangePosition()
    {       
       _rigidbody.AddForce(Vector3.up * _jumpValue, ForceMode.VelocityChange);

        #region Cдвиги по оси X
        /* if (_feedbackObject.transform.position.x == 0)
         {         
             var position = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 0.5f), Random.Range(0, 0));
             SetNewPosition(position);
         }

         if (_feedbackObject.transform.position.x < 0)
         {
             var position = new Vector3(Random.Range(0f, _randomDistanceRight), Random.Range(0.5f, 0.5f), Random.Range(0, 0));
             SetNewPosition(position);
         }

         if (_feedbackObject.transform.position.x > 0)
         {
             var position = new Vector3(Random.Range(_randomDistanceLeft, 0f), Random.Range(0.5f, 0.5f), Random.Range(0, 0));
             SetNewPosition(position);
         }*/
        #endregion
    }
 
}

