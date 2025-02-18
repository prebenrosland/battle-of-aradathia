using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class BossBattleController : MonoBehaviour
{
    public AudioSource bossMusic;
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot bossSnapshot;
    public Animator doorAnimator;
    public string doorCloseTrigger = "CloseDoor";
    public float transitionTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartBossBattle();
        }
    }

    private void StartBossBattle()
    {
        bossMusic.Play();
        bossSnapshot.TransitionTo(transitionTime);
        doorAnimator.SetTrigger(doorCloseTrigger);
    }
}
