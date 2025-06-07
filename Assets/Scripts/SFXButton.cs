using UnityEngine;

public class SFXButton : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip clip;

    public void PlayClickSFX()
    {
        if (PlayerPrefs.GetInt("SFX_ON", 1) == 1 && clip != null && sfxSource != null)
        {
            if (sfxSource.isPlaying)
                sfxSource.Stop();

            sfxSource.clip = clip;
            sfxSource.Play();
        }
    }
}
