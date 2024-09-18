using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;
using Unity.VisualScripting;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cameraRef;

    public CinemachineBasicMultiChannelPerlin shake;
    public float shakePower = 3f;
    float shakeTimeElapsed = 0f;
    float shakeReturnTime = 1.5f;
    private Coroutine shakeCoroutine;

    float FOVOutDelta = 20;
    float FOVBase;
    float zoomTimeElapsed;
    private Coroutine zoomCoroutine;


    // Start is called before the first frame update
    void Awake()
    {
        cameraRef = GetComponent<CinemachineVirtualCamera>();
        shake = cameraRef.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        FOVBase = cameraRef.m_Lens.FieldOfView;
        Meteor.MeteorDown += CameraShake;
        BigMeteor.BigMeteorSpawn += ZoomOut;
        BigMeteor.BigMeteorDown += ZoomIn;
    }

    private void Update()
    {
        if (shakeCoroutine != null)
        {
            Debug.Log(shakeTimeElapsed);
        }
    }

    // Update is called once per frame
    void CameraShake() { if (shakeCoroutine == null) shakeCoroutine = StartCoroutine("ShakeUpandDown"); }
    void ZoomOut() { if (zoomCoroutine == null) zoomCoroutine = StartCoroutine("ZoomOutCR"); }
    void ZoomIn() { if (zoomCoroutine != null) zoomCoroutine = StartCoroutine("ZoomInCR"); }
    IEnumerator ShakeUpandDown()
    {
        while (shakeTimeElapsed < .1)
        {
            shakeTimeElapsed += Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(0f, shakePower, shakeTimeElapsed / .1f);
            yield return null;
        }
        while (shakeTimeElapsed < shakeReturnTime)
        {
            shakeTimeElapsed += Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(shakePower, 0f, shakeTimeElapsed / shakeReturnTime);
            yield return null;
        }
        shakeTimeElapsed = 0f;
        shakeCoroutine = null;
        yield return null;
    }
    IEnumerator ZoomInCR()
    {
        while (zoomTimeElapsed < .3f)
        {
            zoomTimeElapsed += Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(0f, FOVOutDelta, zoomTimeElapsed / .3f);
            yield return null;
        }
        zoomTimeElapsed = 0f;
    }
    IEnumerator ZoomOutCR() {
        while (zoomTimeElapsed < .5f)
        {
            zoomTimeElapsed += Time.deltaTime;
            cameraRef.m_Lens.FieldOfView = Mathf.Lerp(FOVOutDelta, FOVBase, zoomTimeElapsed / .5f);
            yield return null;
        }
        zoomTimeElapsed = 0f;
        zoomCoroutine = null;
        yield return null;
    }
}
