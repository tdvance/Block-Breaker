using UnityEngine;
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

    public float GetQ1() {
        return q1;
    }

    public float GetQ3() {
        return q3;
    }

    public float GetMed() {
        return med;
    }

    float  med, q1, q3;

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

        //mean = sum / count;
        //min = data[0];
        //max = data[count - 1];
        med = (data[Quantile(count, 0.5f)]);
        q1 = (data[Quantile(count, 0.25f)]);
        q3 = data[Quantile(count, 0.75f)];
        Debug.Log(tag + " q1: " + q1);
        Debug.Log(tag + " q2: " + med);
        Debug.Log(tag + " q3: " + q3);
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
