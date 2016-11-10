using UnityEngine;
using System.Collections;

public class Times : MonoBehaviour {
    static float[] times = { 47.27954f, 68.33593f, 78.32074f, 78.29594f, 86.22523f, 111.0284f, 151.933f, 178.5503f, 197.8983f, 139.3857f, 146.1605f, 186.4356f, 256.3286f, 291.525f, 296.5274f, 263.3924f, 274.1194f, 297.1198f, 324.0011f, 341.0515f, 352.6623f, 294.2376f, 320.8093f, 357.6603f, 324.6164f, 391.0268f, 471.6492f, 427.7928f, 478.0576f, 485.6272f, 318.3074f, 340.9438f, 387.5831f, 420.8478f, 433.563f, 475.951f, 376.9513f, 421.0229f, 463.8161f, 457.9109f, 471.2205f, 509.0179f, 530.8908f, 555.2316f, 605.8992f, 538.8319f, 558.3521f, 564.2317f, 481.7366f, 540.8477f, 606.3722f, 644.4814f, 665.4974f, 702.5551f, 702.3815f, 776.5877f, 842.5008f };

    public static float[] q1 = new float[20];
    public static float[] q2 = new float[20];
    public static float[] q3 = new float[20];

    // Use this for initialization
    void Start() {
        for(int i=1; i<19; i++) {
            q1[i] = times[i * 3 - 2];
            q2[i] = times[i * 3 - 1];
            q3[i] = times[i * 3];

        }
    }

    // Update is called once per frame
    void Update() {

    }
}
