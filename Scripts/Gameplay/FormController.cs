using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class FormController : MonoBehaviour
{
    [Header("Значения для расчёта сытости")]
    [Tooltip("Максимальное число сытости")]
    [SerializeField] private float _fullyCount = 2;
    [Tooltip("Отнимаемый показатель сытости в секунду")]
    [SerializeField] private float _satietyPerSec = 0.1f;
    [Tooltip("Значение сытости еды ")]
    [SerializeField] private float _peiceOfFood = 1;
    [Tooltip("Доп. значение до взрыва рыбы")]
    [SerializeField] private float spareFood = 0.5f;

    private float _currentSatiety = 0.1f;
    private ColliderChecker _colliderChecker;

    [Header("Feedbacks Events")] [Space(10)]
    public UnityEvent OnEnableFullyModel;
    public UnityEvent OnEnableHungryModel;
    public UnityEvent OnDisableFullyModel;
    public UnityEvent OnDisableHungryModel;  

    [HideInInspector]
    public bool IsReturnFromTask;
    public float CurrentSatiety { get => _currentSatiety; }
    public float FullyCount { get => _fullyCount; }

    public event Action OnLose;
   
    private async void Awake()
    {
        IsReturnFromTask = false;

        _colliderChecker = GetComponent<ColliderChecker>();
        _colliderChecker.OnSetFullBehaviour += ApplyFood;

        await DecreaceSatiety();
    }

    private void Update() => Check();

    private void OnDestroy()
    {
        _colliderChecker.OnSetFullBehaviour -= ApplyFood;
        IsReturnFromTask = true;
    }

    private void ApplyFood()
    {
        _currentSatiety += _peiceOfFood;
    }

    private void Check()
    {
        if (_currentSatiety == 0) return;
      
        else if (_currentSatiety < _fullyCount / 2)
        {
            OnEnableHungryModel?.Invoke();
            OnDisableFullyModel?.Invoke();
        }

        else if (_currentSatiety >= _fullyCount && _currentSatiety <= _fullyCount + spareFood)
        {
            OnEnableFullyModel?.Invoke();
            OnDisableHungryModel?.Invoke();
        }

        else if (_currentSatiety > _fullyCount && _currentSatiety >= _fullyCount + spareFood)
        {
            OnLose?.Invoke();
            gameObject.SetActive(false);
        }
    }  

    private async UniTask DecreaceSatiety()
    {
        while (true)
        {
            _currentSatiety -= _satietyPerSec;

            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
            if (IsReturnFromTask == true) return;

            else if (_currentSatiety <= 0f)
                _currentSatiety = 0.1f;         
        }
    }
}

