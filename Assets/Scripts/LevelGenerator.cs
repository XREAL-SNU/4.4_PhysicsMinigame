using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public const int SIZE = 7;
    public const float LAYERDIST = 5f;
    public const int DESPAWNLAYER = 2;

    public static float playerGroundy = 0f;
    public int lastLevel = 0;
    public Vector2 offsetGround = new Vector2(0, 0);

    public Block defaultBlock, floorBlock;
    public Block[] blockPrefabs;
    public GameObject border;
    public PlayerControl pcon;

    public static PlayerControl pconInstance;

    private float seed;

    private void Awake() {
        playerGroundy = 0f;
    }

    void Start() {
        pconInstance = pcon;
        offsetGround = new Vector2(0, 0);
        MakeFloor(0f);
        seed = Random.value * 1000f;
    }

    void Update() {
        MakeLevels();
    }

    public void MakeLevels() {
        while (Mathf.Max(playerGroundy, pcon.transform.position.y) - 1f + DESPAWNLAYER * LAYERDIST > lastLevel * LAYERDIST) {
            MakeLevel(lastLevel + 1);
        }
    }

    public void MakeFloor(float y) {
        MakeLayerBlocks(y, 1, -1, 0, 1, floorBlock, false);
    }

    public void MakeLevel(int level) {
        if (lastLevel >= level) return;
        if(Chance(0.1f * (level - 10))) offsetGround += new Vector2(Random.Range(0, 0.3f), Random.Range(0, 0.3f));
        lastLevel = level;

        MakeLayerBlocks(level * LAYERDIST, Scl(level), Thresh(level), level * SIZE * 2f, Density(level), defaultBlock, true);

        //todo wind, ice sections, ect
    }

    public void MakeLayerBlocks(float y, float scl, float thresh, float offset, float density, Block defaultBlock, bool additionalBlocks) {
        int n = SIZE / 2;
        for (int i = -n; i <= n; i++) {
            for (int j = -n; j <= n; j++) {
                bool placed = false;
                if (additionalBlocks) {
                    foreach(Block b in blockPrefabs) {
                        if(Perlin(b, i, j, scl, thresh, offset, density)) {
                            SetBlock(b, i + offsetGround.x, j + offsetGround.y, y + HeightMap((int)(y / LAYERDIST), i, j));
                            placed = true; break;
                        }
                    }
                }

                if (!placed && Perlin(defaultBlock, i, j, scl, thresh, offset, density)) {
                    SetBlock(defaultBlock, i + offsetGround.x, j + offsetGround.y, y + HeightMap((int)(y / LAYERDIST), i, j));
                }
            }
        }

        Instantiate(border, new Vector3(offsetGround.x, y, offsetGround.y), Quaternion.identity);
    }

    public void SetBlock(Block block, float x, float z, float layer) {
        Instantiate(block.gameObject, new Vector3(x, layer, z), Quaternion.identity);
    }

    private bool Chance(float a) {
        return Random.Range(0f, 1f) <= a;
    }

    private float Scl(int l) {
        return 1 / l + 1f;
    }

    private float Thresh(int l) {
        return Mathf.Clamp(0.5f - l / 500f, 0.2f, 0.4f);
    }

    private float Density(int l) {
        return Mathf.Clamp(1f - l / 1000f, 0.5f, 1f);
    }

    private int HeightMap(int l, float x, float y) {
        if (l < 15) return 0;
        if (Chance(1 - l / 50f)) return 0;
        return Mathf.FloorToInt(Mathf.PerlinNoise((x / 3f + l * 497) % 1f, (y / 3f + seed) % 1f) * 3) - 1;
    }

    public bool Perlin(Block block, float x, float y, float scl, float thresh, float offset, float density) {
        return Perlin(x, y, scl * block.scl, thresh * block.thresh, offset, density);
    }

    public bool Perlin(float x, float y, float scl, float thresh, float offset, float density) {
        return Perlin(x + offset, y, scl, thresh) && Random.Range(0f, 1f) <= density;
    }

    public bool Perlin(float x, float y, float scl, float thresh) {
        if (thresh < -0.1) return true;
        return Mathf.PerlinNoise((x / scl) % 1f, (y / scl + seed) % 1f) <= thresh;
    }
}
