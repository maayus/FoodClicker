using DG.Tweening;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [Header("Animation settings")]
    public float scale = 1.2f;
    public float duration = 0.1f;
    public Ease ease;

    [Header("Sound")]
    public AudioClip clickSound;

    [Header("VFX")]
    public ParticleSystem clickParticles;

    [HideInInspector]public int clicks = 0;

    private AudioSource audioSource;
   
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown() 
    {
        clickParticles.Emit(1);
        clicks++;
        UiManager.instance.UpdateClicks(clicks);
        UiManager.instance.clicks++;
        UiManager.instance.Invoke("Count", 1);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clickSound);

        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease);//ease - how the animation will be played
            //.SetLoops(2, LoopType.Yoyo);
    }

}
