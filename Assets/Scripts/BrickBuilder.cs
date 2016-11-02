using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickBuilder : MonoBehaviour {
    float left = -30;
    float right = 30;
    float top = 17.5f;
    float bottom = -10f;
    float width = 4f;
    float height = 1.28f;

    int maxXCount = 16;
    int maxYCount = 22;

    public GameObject[] brickPrefabs;

    public List<Color> brickColors = new List<Color>();


    // Use this for initialization
    void Start() {
        int level = LevelManager.current_level;
        int iteration = (level + LevelManager.levels.Length - 1) / LevelManager.levels.Length;
        Texture2D texture = LevelManager.levels[(level + LevelManager.levels.Length - 1) % LevelManager.levels.Length];
        LevelManager.numBricks = MakeBricks(texture, iteration);
    }

    public int MakeBricks(Texture2D texture, int iteration) {
        int count = 0;
        for (int i = 0; i < maxXCount; i++) {
            for (int j = 0; j < maxYCount; j++) {
                float x = left + i * width;
                float y = bottom + j * height;
                Color c = texture.GetPixel(i, j);
                if (c != Color.white) {
                    int type = (int)(c.a * 3.9);
                    if (iteration > 1 && type < 2) {
                        type++;
                        if (iteration > 2 && type < 2) {
                            type++;
                        }
                    }
                    c = FindNearestBrickColor(c);
                    if (c != Color.white) {
                        MakeBrick(x, y, c, type);
                        if (type < 3) {
                            count++;
                        }
                    }
                }
            }
        }
        return count;
    }

    public Color FindNearestBrickColor(Color c) {
        if (brickColors.Count == 0) {
            return c;
        }

        Color bestColor = Color.white;
        float bestDistance = ColorDistance(bestColor, c);
        foreach (Color bc in brickColors) {
            float d = ColorDistance(bc, c);
            if (d < bestDistance) {
                bestColor = bc;
                bestDistance = d;
            }
        }
        return bestColor;
    }

    public float ColorDistance(Color a, Color b) {
        return (a.r - b.r) * (a.r - b.r) + (a.g - b.g) * (a.g - b.g) + (a.b - b.b) * (a.b - b.b);

    }

    public void Fill() {
        int xcount = 0;
        for (float x = left; x <= right; x += width) {
            xcount++;
            int ycount = 0;
            for (float y = bottom; y <= top; y += height) {
                ycount++;
                MakeBrick(x, y);
            }
        }
    }

    public GameObject MakeBrick(float x, float y) {
        Color color = RandomBrickColor();
        return MakeBrick(x, y, color, 0);
    }

    public GameObject MakeBrick(float x, float y, Color color, int type) {
        //Debug.Log("Making brick: " + color + ", " + type);
        GameObject brick = Instantiate(brickPrefabs[type], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        brick.transform.SetParent(gameObject.transform);
        brick.GetComponent<SpriteRenderer>().color = color;
        return brick;
    }

    Color RandomBrickColor() {
        Color color = Color.red;

        if (brickColors.Count > 0) {
            int i = Random.Range(0, brickColors.Count);
            color = brickColors[i];
        }

        return color;
    }

}
