using DG.Tweening;
using System.Collections.Generic;
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

    [Header("Settings")]
    public int clickValue = 1;
    public List<GameObject> upgrades;

    [HideInInspector]public int clicks = 0;

    private AudioSource audioSource;

    private int upgradeIndex = 0;
    public ShopButton upgradeButton;

    private void Start() 
    {
        clicks = PlayerPrefs.GetInt("clicks", 0);
        upgradeIndex = PlayerPrefs.GetInt("upgradeIndex", 0);
        clickValue = PlayerPrefs.GetInt("clickValue", 1);
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (i != upgradeIndex)
            {
                upgrades[i].SetActive(false);
            }
            else
            {
                upgrades[upgradeIndex].SetActive(true);
            }
        }
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

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("clicks", clicks);
        PlayerPrefs.SetInt("upgradeIndex", upgradeIndex);
        PlayerPrefs.SetInt("clickValue", clickValue);
        PlayerPrefs.Save();
    }

    public void UpgradeClicker()
    {
        if (upgradeIndex < upgrades.Count)
        {
            upgrades[upgradeIndex].SetActive(false);
            upgradeIndex++;
            upgrades[upgradeIndex].SetActive(true);

            clickValue++;
        }
        if (upgradeIndex == upgrades.Count)
        {
            Destroy(upgradeButton);
        }
    }

}
