﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatisticsManager : MonoBehaviour {

    public bool clearOnStart = false;

    private string prefix;

    void Start() {
        if (clearOnStart) {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Clear(string tag) {
        string countKey = prefix + "Count." + tag;
        if (PlayerPrefs.HasKey(countKey)) {
            int count = PlayerPrefs.GetInt(countKey);
            for (int i = 0; i < count; i++) {
                string dataKey = prefix + "Data[" + i + "]." + tag;
                PlayerPrefs.DeleteKey(dataKey);
            }
            PlayerPrefs.DeleteKey(countKey);
        }
    }

    public void AddValue(float v, string tag) {
        string countKey = prefix + "Count." + tag;
        int count = 0;
        if (PlayerPrefs.HasKey(countKey)) {
            count = PlayerPrefs.GetInt(countKey);
        }

        string dataKey = prefix + "Data[" + count + "]." + tag;
        PlayerPrefs.SetFloat(dataKey, v);
        count++;
        PlayerPrefs.SetInt(countKey, count);
    }

    public void LogStats(string tag) {
        string countKey = prefix + "Count." + tag;
        int count = 0;
        if (PlayerPrefs.HasKey(countKey)) {
            count = PlayerPrefs.GetInt(countKey);
        }
        if (count == 0) {
            Debug.LogWarning("No statistics found for tag " + tag);
            return;
        }

        List<float> data = new List<float>();

        for (int i = 0; i < count; i++) {
            string dataKey = prefix + "Data[" + i + "]." + tag;
            data.Add(PlayerPrefs.GetFloat(dataKey));
        }

        float sum = 0;
        foreach (float v in data) {
            sum += v;
        }
        data.Sort();

        Debug.Log("Mean(" + tag + ") = " + (sum / count));
        Debug.Log(" Min(" + tag + ") = " + (data[0]));
        Debug.Log(" Max(" + tag + ") = " + (data[count - 1]));
        Debug.Log(" Med(" + tag + ") = " + (data[Quantile(count, 0.5f)]));
        Debug.Log("  Q1(" + tag + ") = " + (data[Quantile(count, 0.25f)]));
        Debug.Log("  Q3(" + tag + ") = " + (data[Quantile(count, 0.75f)]));
    }

    private int Quantile(int count, float frac) {
        return (int)Mathf.Ceil(frac * (count - 1));
    }

    #region Singleton
    private static StatisticsManager _instance;

    public static StatisticsManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<StatisticsManager>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance != null && _instance != this) {
            Debug.LogError("Duplicate singleton " + this.gameObject + " created; destroying it now");
            Destroy(this.gameObject);
        }

        if (_instance != this) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        prefix = "StatisticsManager.";

    }
    #endregion

}