using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileUpdater : MonoBehaviour {
    public MeshRenderer render;
    public ParticleSystem particle;

    public void Kill() {
        render.enabled = false;
        particle.Stop();
    }
}
