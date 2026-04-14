using Immersal;
using Immersal.XR;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string SelectedBossName { get; private set; }

    [System.Serializable]
    public class BossEntry
    {
        public string name;
        public GameObject prefab;
    }

    public List<BossEntry> bosses;

    private Dictionary<string, GameObject> bossDict;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        bossDict = new Dictionary<string, GameObject>();
        foreach (var boss in bosses)
        {
            bossDict[boss.name] = boss.prefab;
        }
    }

    public void SetSelectedBoss(string selectedBossName)
    {
        SelectedBossName = selectedBossName;
    }

    public GameObject GetSelectedBossPrefab()
    {
        Debug.Log("Selected Boss Name: " + SelectedBossName);
        if (bossDict.ContainsKey(SelectedBossName))
            return bossDict[SelectedBossName];

        return null;
    }
}
