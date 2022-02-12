using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamebase.Systems.GlobalEvents;
using Gameplay.GdPanel.Attributes;
using UnityEngine.SceneManagement;

/// <summary>
/// Загружает префаб текущего уровня
/// </summary>
public class LevelLoader : MonoBehaviour
{
    private void Awake()
    {
        // Загружаем префаб уровня
        Instantiate(LevelsList.Instance.currentLevel);
        GlobalEventsSystem.Instance.Subscribe(GlobalEventType.Victory, OnVictory);
        GlobalEventsSystem.Instance.Invoke(GlobalEventType.LevelStart);
    }

    private void OnDestroy()
    {
        GlobalEventsSystem.Instance.Unsubscribe(GlobalEventType.Victory, OnVictory);
    }

    private void OnVictory()
    {
        // Переходим на следующий уровень
        LevelsList.Instance.currentLevelIndex++;
    }

    /// <summary>
    /// Уровень для загрузки с помощью ГД панели
    /// </summary>
    [GDPanelExpose]
    private int debugLevel = 1;

    /// <summary>
    /// Загрузить уровень через ГД панель
    /// </summary>
    [GDPanelExpose]
    private void LoadDebugLevel()
    {
        debugLevel = Mathf.Clamp(debugLevel, 1, LevelsList.Instance.levels.Count);
        LevelsList.Instance.currentLevelIndex = debugLevel - 1;
        if (Application.isPlaying) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Сбросить игровой прогресс
    /// </summary>
    [GDPanelExpose]
    private void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        if (Application.isPlaying) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
