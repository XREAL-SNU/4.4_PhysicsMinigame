using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTerrainMissile", menuName = "Missiles/TerrainMissile", order = 0)]
public class TerrainMissile : Missile {
    public float terrainRadius = 2f;
    private Collider[] colc = new Collider[49];

    protected override void Impact(Rigidbody player, Vector3 pos, float str) {
        int n = Physics.OverlapSphereNonAlloc(pos, terrainRadius, colc, 1 << 6, QueryTriggerInteraction.Collide);
        for (int i = 0; i < n; i++) {
            Block b = colc[i].gameObject.GetComponent<Block>();
            if (b != null) b.Kill();
        }
        base.Impact(player, pos, str);
    }
}
