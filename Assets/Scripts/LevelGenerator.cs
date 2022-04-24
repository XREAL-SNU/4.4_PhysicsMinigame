using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public const int SIZE = 7;
    public const float LAYERDIST = 5f;
    public const int DESPAWNLAYER = 2;

    public static float playerGroundy = 0f;

    public Block defaultBlock;
    public Block[] blockPrefabs;

    void Start() {
        MakeFloor(0f);
    }

    void Update() {

    }

    public void MakeFloor(float y) {
        MakeLayerBlocks(y, 1, -1, 0, 1, defaultBlock, false);
    }

    public void MakeLayerBlocks(float y, float scl, float thresh, float offset, float density, Block defaultBlock, bool additionalBlocks) {
        int n = SIZE / 2;
        for (int i = -n; i <= n; i++) {
            for (int j = -n; j <= n; j++) {
                if (Perlin(i, j, scl, thresh, offset, density)) {
                    SetBlock(defaultBlock, i, j, y);
                }
                if (additionalBlocks) {
                    //todo
                }
            }
        }
    }

    public void SetBlock(Block block, float x, float z, float layer) {
        Instantiate(block.gameObject, new Vector3(x, layer, z), Quaternion.identity);
    }

    public bool Perlin(float x, float y, float scl, float thresh, float offset, float density) {
        return Perlin(x + offset, y, scl, thresh) && Random.Range(0f, 1f) <= density;
    }

    public bool Perlin(float x, float y, float scl, float thresh) {
        return Mathf.PerlinNoise(x / scl, y / scl) >= thresh;
    }
}
