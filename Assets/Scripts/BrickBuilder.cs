using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickBuilder : MonoBehaviour {



    public GameObject[] brickPrefabs;

    public List<Color> brickColors = new List<Color>();

    private string levelName = "None";

    // Use this for initialization
    void Start() {

        int level = LevelManager.instance.current_level;
        int iteration = (level + LevelManager.instance.levels.Length - 1) / LevelManager.instance.levels.Length;
        int textureNum = (level + LevelManager.instance.levels.Length - 1) % LevelManager.instance.levels.Length;
        Texture2D texture = LevelManager.instance.levels[textureNum];
        levelName = texture.name;
        LevelManager.instance.numBricks = MakeBricks(texture, iteration);


    }

    public string GetLevelName() {
        return levelName;
    }

    public int MakeBricks(Texture2D texture, int iteration) {
        int count = 0;
        for (int i = 0; i < LevelManager.instance.bricksPerRow; i++) {
            for (int j = 0; j < LevelManager.instance.bricksPerColumn; j++) {
                float x = LevelManager.instance.playSpaceLeft + i * LevelManager.instance.brickWidth;
                float y = LevelManager.instance.playSpaceBottom + j * LevelManager.instance.brickHeight;
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
        float h1, s1, v1;
        float h2, s2, v2;

        Color.RGBToHSV(a, out h1, out s1, out v1);
        Color.RGBToHSV(b, out h2, out s2, out v2);

        float ds = (s1 - s2);

        return CircularDistance(h1, h2) * (2 * ds * ds - 2 * ds + 1) + 0.1f * Mathf.Abs(v1 - v2) + 0.05f * Mathf.Abs(ds);
    }

    float CircularDistance(float a, float b) {
        float d = a - b;
        if (d < 0) {
            d = -d;
        }
        if (1 - d < d) {
            d = 1 - d;
        }
        return d;
    }

    public void Fill() {
        int xcount = 0;
        for (float x = LevelManager.instance.playSpaceLeft; x <= LevelManager.instance.playSpaceRight; x += LevelManager.instance.brickWidth) {
            xcount++;
            int ycount = 0;
            for (float y = LevelManager.instance.playSpaceBottom; y <= LevelManager.instance.playSpaceTop; y += LevelManager.instance.brickHeight) {
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
        GameObject brick = Instantiate(brickPrefabs[type], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        brick.transform.SetParent(gameObject.transform);
        brick.GetComponent<SpriteRenderer>().color = color;
        brick.transform.localScale = LevelManager.instance.brickScale;

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
