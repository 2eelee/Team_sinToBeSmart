using UnityEngine;

public class SceneBGMController : MonoBehaviour
{
    public AudioSource bgmSource;

    void Start()
    {
        bool isBGMOn = PlayerPrefs.GetInt("BGM_ON", 1) == 1;

        if (bgmSource != null)
        {
            if (isBGMOn)
            {
                if (!bgmSource.isPlaying)
                    bgmSource.Play();
            }
            else
            {
                bgmSource.Stop();
            }
        }
    }
}
