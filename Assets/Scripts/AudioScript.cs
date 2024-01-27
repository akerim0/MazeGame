using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip slash,footSteps,gruntAudio;
    Animator animator;
    private AudioSource audioSource;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void StepAudio()
    {
       
        audioSource.PlayOneShot(footSteps, 0.3f);
    }
    void WalkStep()
    {
        if(animator.GetFloat("Blend")>0.1f && animator.GetFloat("Blend") <= 0.5f)
        {
            audioSource.PlayOneShot(footSteps,0.3f);
        }
    }
    void RunStep()
    {
        if (animator.GetFloat("Blend") > 0.5f && animator.GetFloat("Blend") <= 1.0f)
        {
            audioSource.PlayOneShot(footSteps, 0.3f);
        }
    }

    void BackWalkStep()
    {
        if (animator.GetFloat("backBlend") > 0.1f && animator.GetFloat("backBlend") <= 0.5f)
        {
            audioSource.PlayOneShot(footSteps, 0.3f);
        }
    }
    void BackRunStep()
    {
        if (animator.GetFloat("backBlend") > 0.5f && animator.GetFloat("backBlend") <= 1.0f)
        {
            audioSource.PlayOneShot(footSteps, 0.35f);
        }
    }
    void Slash()
    {
        audioSource.PlayOneShot(slash, 0.45f);
    }
    public void PlayGruntAudio()
    {
        audioSource.PlayOneShot(gruntAudio,0.5f);
    }
}
