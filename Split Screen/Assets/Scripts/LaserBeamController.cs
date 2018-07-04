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
    public Color beamColor;

    [Header("Components")]
    public ParticleSystem laserChargingCenterParticle;
    public ParticleSystem laserChargingRingParticle;
    public ParticleSystem laserParticle;
    public ParticleSystem lightningParticle;

    private ParticleSystem.MainModule laserChargingCenterMainModule;
    private ParticleSystem.MainModule laserChargingRingMainModule;
    private ParticleSystem.MainModule laserMainModule;
    private ParticleSystem.MainModule lightningMainModule;

    private ParticleSystem.ShapeModule lightningShapeModule;

    public void PlayBeam(float angle) {
        laserChargingCenterMainModule.startRotation = angle * Mathf.Deg2Rad;
        laserChargingRingMainModule.startRotation = angle * Mathf.Deg2Rad;
        laserMainModule.startRotation = angle * Mathf.Deg2Rad;
        lightningParticle.transform.rotation = Quaternion.Euler(0, 0, -angle);

        laserChargingCenterParticle.Play();
        laserChargingRingParticle.Play();
        laserParticle.Play();
        lightningParticle.Play();
    }

    public bool IsPlaying() {
        return laserChargingCenterParticle.isPlaying || laserChargingRingParticle.isPlaying ||
            laserParticle.isPlaying || lightningParticle.isPlaying;
    }

    private void OnValidate() {
        #region laser charging center particle setup
        laserChargingCenterMainModule = laserChargingCenterParticle.main;

        laserChargingCenterMainModule.duration = chargeTime;
        laserChargingCenterMainModule.startLifetime = chargeTime;

        laserChargingCenterMainModule.startColor = beamColor;
        #endregion

        #region laser charging ring particle setup
        laserChargingRingMainModule = laserChargingRingParticle.main;

        laserChargingRingMainModule.duration = chargeTime;
        laserChargingRingMainModule.startLifetime = chargeTime;

        laserChargingRingMainModule.startColor = beamColor;
        #endregion

        #region laser particle setup
        laserMainModule = laserParticle.main;

        laserMainModule.duration = beamDuration;
        laserMainModule.startLifetime = beamDuration;
        laserMainModule.startDelay = chargeTime;
        laserMainModule.startSizeX = range;

        laserMainModule.startColor = beamColor;
        #endregion

        #region lightning particles setup
        lightningMainModule = lightningParticle.main;
        lightningShapeModule = lightningParticle.shape;

        lightningMainModule.duration = beamDuration;
        lightningMainModule.startLifetime = beamDuration;
        lightningMainModule.startDelay = chargeTime;

        lightningMainModule.startColor = beamColor;

        lightningShapeModule.position = new Vector3(range / 2f, 0, 0);
        lightningShapeModule.scale = new Vector3(range, 1, 0);
        #endregion
    }
}