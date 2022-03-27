using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffects : MonoBehaviour
{
    public Camera camera;
    public PostProcessVolume volume;

    private Vignette vignette;

    [Header("Стандартные настройки")]
    public Color defaultColor;
    public float defaultIntensity;
    public float defaultSmoothness;
    public float defaultCameraSize;

    [Header("Настройки эффекта иссушения")]
    public Color effectColor;
    public float effectIntensity = 0.7f;
    public float effectSmoothness = 0.9f;
    public float effectCameraSize = 5f;

    private void Awake()
    {
        vignette = volume.sharedProfile.GetSetting<Vignette>();
    }

    private void Start()
    {
        SetDefault();
    }

    private void OnDestroy()
    {
        SetDefault();
    }

    public void SetDefault()
    {
        camera.orthographicSize = defaultCameraSize;
        vignette.intensity.value = defaultIntensity;
        vignette.smoothness.value = defaultSmoothness;
        vignette.color.value = defaultColor;
    }

    public void SetDamaged()
    {
        camera.orthographicSize = effectCameraSize;
        vignette.intensity.value = effectIntensity;
        vignette.smoothness.value = effectSmoothness;
        vignette.color.value = effectColor;
    }
}
