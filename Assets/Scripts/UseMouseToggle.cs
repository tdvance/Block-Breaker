using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UseMouseToggle : MonoBehaviour {

    public static bool useMouse = true;

    void Start() {
        GetComponent<Toggle>().isOn = useMouse;
    }

    public void ToggleMouse() {
        useMouse = GetComponent<Toggle>().isOn;
        if (useMouse) {
            PlayerPrefs.SetInt("UseMouse", 1);
        } else {
            PlayerPrefs.SetInt("UseMouse", 0);
        }
    }

}
