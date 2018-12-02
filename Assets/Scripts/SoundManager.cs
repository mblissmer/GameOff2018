using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager sm;
    private AudioSource[] asources;
    private AudioSource brushingSource;
    private AudioSource snoringSource;
    public AudioClip brushing;
    public AudioClip snoring;
    public AudioClip[] pickUp;
    public AudioClip[] drawing;
    public AudioClip[] eating;
    public AudioClip[] footsteps;
    public AudioClip[] growl;
    public AudioClip[] purr;
    public AudioClip[] buttonHover;
    public AudioClip[] buttonPress;
    public AudioClip[] drops;
    [Header("Mixer Groups")]
    public AudioMixerGroup snoringGroup;
    public AudioMixerGroup pickupGroup;
    public AudioMixerGroup uIGroup;
    public AudioMixerGroup footstepsGroup;
    public AudioMixerGroup dropGroup;
    public AudioMixerGroup drawingGroup;
    public AudioMixerGroup eatingGroup;
    public AudioMixerGroup growlsGroup;
    public AudioMixerGroup brushingGroup;
    public AudioMixerGroup purrsGroup;


    void Awake () {
        sm = this;
        SetupSources();
        SetupSingleSource(ref snoringSource, snoring, snoringGroup);
        SetupSingleSource(ref brushingSource, brushing, brushingGroup);

    }

    private void SetupSources() {
        asources = new AudioSource[10];
        for (int i = 0; i < asources.Length; i++) {
            asources[i] = gameObject.AddComponent<AudioSource>();
            asources[i].playOnAwake = false;
        }
    }

    private void SetupSingleSource(ref AudioSource source, AudioClip clip, AudioMixerGroup amg) {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.outputAudioMixerGroup = amg;
        source.clip = clip;
        source.loop = true;
    }

    public void DrawingSound() {
        PlaySound(drawing,drawingGroup);
    }

    public void EatingSound() {
        PlaySound(eating,eatingGroup);
    }

    public void FootstepsSound() {
        PlaySound(footsteps,footstepsGroup);
    }

    public void GrowlSound() {
        PlaySound(growl,growlsGroup);
    }

    public void PurrSound() {
        PlaySound(purr,purrsGroup);
    }

    public void ButtonHoverSound() {
        PlaySound(buttonHover,uIGroup);
    }

    public void ButtonPressSound() {
        PlaySound(buttonPress,uIGroup);
    }

    public void DropSound() {
        PlaySound(drops,dropGroup);
    }

    public void PickUp() {
        PlaySound(pickUp,pickupGroup);
    }

    private void PlaySound(AudioClip[] sounds, AudioMixerGroup amg) {
        int choice = Random.Range(0, sounds.Length);
        for (int i = 0; i < asources.Length; i++) {
            if (!asources[i].isPlaying) {
                asources[i].outputAudioMixerGroup = amg;
                asources[i].clip = sounds[choice];
                asources[i].Play();
                break;
            }
        }
    }

    public void BrushingStart() {
        if (!brushingSource.isPlaying) brushingSource.Play();
    }

    public void BrushingStop() {
        if (brushingSource.isPlaying) brushingSource.Stop();
    }

    public void SnoringStart() {
        if (!snoringSource.isPlaying) snoringSource.Play();
    }

    public void SnoringStop() {
        if (snoringSource.isPlaying) snoringSource.Stop();
    }
}
