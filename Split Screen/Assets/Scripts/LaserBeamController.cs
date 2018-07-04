/*
* Created by Daniel Mak
*/

using UnityEngine;

public class LaserBeamController : MonoBehaviour {

    [Header("Properties")]
    public float chargeTime = 1f;
    public float range = 25f;
    public float beamDuration = 0.2f;
    public float damage = 1f;

    [Header("Components")]
    public ParticleSystem laserChargingParticle;
    public ParticleSystem laserParticle;
    public ParticleSystem lightningParticle;

    private ParticleSystem.MainModule laserChargingMainModule;
    private ParticleSystem.MainModule laserMainModule;
    private ParticleSystem.MainModule lightningMainModule;
    private ParticleSystem.ShapeModule lightningShapeModule;

    public void Play(float angle) {
        laserChargingMainModule.startRotation = angle * Mathf.Deg2Rad;
        laserMainModule.startRotation = angle * Mathf.Deg2Rad;
        lightningParticle.transform.rotation = Quaternion.Euler(0, 0, -angle);

        laserChargingParticle.Play();
        laserParticle.Play();
        lightningParticle.Play();
    }

    private void OnValidate() {
        #region laser charging particle setup
        laserChargingMainModule = laserChargingParticle.main;

        laserChargingMainModule.duration = chargeTime;
        laserChargingMainModule.startLifetime = chargeTime;
        #endregion

        #region laser particle setup
        laserMainModule = laserParticle.main;

        laserMainModule.duration = beamDuration;
        laserMainModule.startLifetime = beamDuration;
        laserMainModule.startDelay = chargeTime;
        laserMainModule.startSizeX = range;
        #endregion

        #region lightning particles setup
        lightningMainModule = lightningParticle.main;
        lightningShapeModule = lightningParticle.shape;

        lightningMainModule.duration = beamDuration;
        lightningMainModule.startLifetime = beamDuration;
        lightningMainModule.startDelay = chargeTime;

        lightningShapeModule.radius = range / 2f;
        lightningShapeModule.position = new Vector3(range / 2f, 0, 0);
        #endregion
    }
}