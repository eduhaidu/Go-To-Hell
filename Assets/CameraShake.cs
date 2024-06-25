using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake() => Instance = this;

    private void onShake(float duration, float strength){
        transform.DOShakePosition(duration,strength);
        transform.DOShakeRotation(duration,strength);
    }

    public static void Shake(float duration, float strength) => Instance.onShake(duration,strength);
}
