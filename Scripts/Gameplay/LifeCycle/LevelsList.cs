using System.Collections.Generic;
using UnityEngine;
using Gamebase.Miscellaneous;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using System.Linq;
using Gameplay.GdPanel.Attributes;

/// <summary>
/// Список конфигов игровых уровней
/// Содержит индекс текущего уровня
/// </summary>
[CreateAssetMenu(fileName = "LevelsList", menuName = "Settings/LevelsList")]
public class LevelsList : StaticScriptableObject<LevelsList>
{
    [Tooltip("Игровые уровни")]
    public List<GameObject> levels = new List<GameObject>();

    /// <summary>
    /// Индекс текущего уровня
    /// </summary>
    public int currentLevelIndex
    {
        get => PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        set => PlayerPrefs.SetInt("CurrentLevelIndex", Mathf.Clamp(value, 0, levels.Count - 1));
    }

    /// <summary>
    /// Префаб текущего уровня
    /// </summary>
    public GameObject currentLevel
    {
        get
        {
            if (currentLevelIndex >= levels.Count) currentLevelIndex = 0;

            return levels[currentLevelIndex];
        }
    }

    /// <summary>
    /// Установить уровень с указанным именем в качестве текущего уровня
    /// </summary>
    /// <param name="levelName"></param>
    public void SetCurrentLevel(string levelName)
    {
        int idx = levels.FindIndex((s) => s.name == levelName);

        Debug.Assert(idx >= 0, "Невозможно найти уровень в списке уровней " + levelName);

        currentLevelIndex = idx;
    }

    /// <summary>
    /// Проверить корректность списка уровней
    /// </summary>
    /// <returns></returns>
    public bool CheckAsserts()
    {
        if (levels.Count == 0) Debug.LogError("Не указан ни один уровень в " + name);

        if (levels.Find(l => l == null) != null) Debug.LogError("Не указан один из уровней в " + name);

        var knownKeys = new HashSet<string>();
        if (levels.Any(l => !knownKeys.Add(l.name))) Debug.LogError("Повторяющиеся уровни в " + name);

        return true;
    }
}
