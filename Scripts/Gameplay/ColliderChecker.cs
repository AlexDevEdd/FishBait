using System;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    private const string FISH = "Fish";
    private const string FOOD = "Food";

    public event Action OnSetFullBehaviour;
    public event Action OnSpawnFood;

    private void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag(FOOD))
        {
            other.gameObject.SetActive(false);
            OnSetFullBehaviour.Invoke();
            OnSpawnFood.Invoke();
            Destroy(other.gameObject, 10f);         
        }
    }
}




