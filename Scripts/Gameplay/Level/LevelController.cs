using Cysharp.Threading.Tasks;
using Gamebase.Systems.GlobalEvents;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private FormController[] _formController;
    private TapController _tapController;
    private List<FormController> _fishCountList = new List<FormController>();

    private async void Start()
    {
        _formController = FindObjectsOfType<FormController>();
        _tapController = FindObjectOfType<TapController>();

        foreach (var actionLose in _formController)
        {
            actionLose.OnLose += Lose;
        }

        await CheckAllSatiety();
    }

    private async UniTask CheckAllSatiety()
    {
        while (true)
        {
            await UniTask.Delay(500);

            foreach (var currentSatiety in _formController)
            {
                if (currentSatiety.IsReturnFromTask == true) return;

                if (currentSatiety.CurrentSatiety >= currentSatiety.FullyCount / 2)
                    _fishCountList.Add(currentSatiety);

                if (_fishCountList.Count == _formController.Length)
                    Victory();
            }

            _fishCountList.Clear();
        }
    }

    private void Victory()
    {
        GlobalEventsSystem.Instance.Invoke(GlobalEventType.Victory);
        DisableTapController();
    }

    private void Lose()
    {
        GlobalEventsSystem.Instance.Invoke(GlobalEventType.Lose);
        DisableTapController();
    }

    private void DisableTapController()
    {
        if (!_tapController)
            _tapController = FindObjectOfType<TapController>();

        _tapController.enabled = false;
    }
}
