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


    // Start is called before the first frame update
    void Awake()
    {
        cameraRef = GetComponent<CinemachineVirtualCamera>();
        shake = cameraRef.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Meteor.MeteorSpawn += CameraShake;
    }

    private void Update()
    {
        if (shakeCoroutine != null)
        {
            Debug.Log(shakeTimeElapsed);
        }
    }

    // Update is called once per frame
    void CameraShake()
    {
        cameraRef.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if(shakeCoroutine == null) shakeCoroutine = StartCoroutine("ShakeUpandDown");
    }
    IEnumerator ShakeUpandDown()
    {
        while(shakeTimeElapsed < .1)
        {
            shakeTimeElapsed += Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(0f, shakePower, shakeTimeElapsed / .1f);
            yield return null;
        }
        while(shakeTimeElapsed < shakeReturnTime)
        {
            shakeTimeElapsed += Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(shakePower, 0f, shakeTimeElapsed / shakeReturnTime );
            yield return null;
        }
        shakeTimeElapsed = 0f;
        shakeCoroutine = null;
        yield return null;
    }
}
