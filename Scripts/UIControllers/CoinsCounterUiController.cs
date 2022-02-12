using Doozy.Engine.Progress;
using Gamebase.Systems.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCounterUiController : MonoBehaviour
{
    [SerializeField] private Progressor progressor;

    private void Start()
    {
        progressor.SetValue(ResourcesSystem.Instance.GetResourceCount<Coins>(), true);
        ResourcesSystem.Instance.SubscribeResourceChange<Coins>(OnCoinsAmountChange);
    }

    private void OnDestroy()
    {
        ResourcesSystem.Instance.UnsubscribeResourceChange<Coins>(OnCoinsAmountChange);
    }

    private void OnCoinsAmountChange(int amount)
    {
        progressor.SetValue(amount);
    }
}
