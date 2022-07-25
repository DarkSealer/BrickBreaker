using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFX : MonoBehaviour
{
    [SerializeField]
    private AudioSource _as = null;

    public void PlaySFX(AudioClip audioClip) 
    {
        _as.clip = audioClip;
        _as.Play();
    }
}
