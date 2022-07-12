using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource fx;
    public AudioSource music;
    public AudioClip xPlacement;
    public AudioClip oPlacement;
    public AudioClip clickSound;

    void Awake() {
        instance = this;
    }

    public static void PlayPlayerPlacementSound(int player) {
        if (player == 1) {
            instance.fx.clip = instance.xPlacement;
        } else {
            instance.fx.clip = instance.oPlacement;
        }
        instance.fx.Play();
    }

    public static void PlayClickSound() {
        instance.fx.clip = instance.clickSound;
        instance.fx.Play();
    }
    
}
