using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("효과음 버튼")]
    public Button sfxOnBtn;
    public Button sfxOffBtn;

    [Header("배경음 버튼")]
    public Button bgmOnBtn;
    public Button bgmOffBtn;

    [Header("소리 오디오 소스")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public Color32 onColor = new Color32(220, 242, 222, 255);  // #DCF2DE
    public Color32 offColor = new Color32(217, 217, 217, 255); // #D9D9D9

    private const string SFX_KEY = "SFX_ON";
    private const string BGM_KEY = "BGM_ON";

    void Start()
    {
        if (!PlayerPrefs.HasKey(SFX_KEY)) PlayerPrefs.SetInt(SFX_KEY, 1);
        if (!PlayerPrefs.HasKey(BGM_KEY)) PlayerPrefs.SetInt(BGM_KEY, 1);
        PlayerPrefs.Save();

        sfxOnBtn.onClick.AddListener(() => SetSFX(true));
        sfxOffBtn.onClick.AddListener(() => SetSFX(false));
        bgmOnBtn.onClick.AddListener(() => SetBGM(true));
        bgmOffBtn.onClick.AddListener(() => SetBGM(false));

        UpdateUI();
        ApplyAudioSettings();
    }

    void SetSFX(bool isOn)
    {
        PlayerPrefs.SetInt(SFX_KEY, isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateUI();
        ApplySFXSetting();
    }

    void SetBGM(bool isOn)
    {
        PlayerPrefs.SetInt(BGM_KEY, isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateUI();
        ApplyBGMSetting();
    }

    void UpdateUI()
    {
        bool sfxOn = PlayerPrefs.GetInt(SFX_KEY) == 1;
        bool bgmOn = PlayerPrefs.GetInt(BGM_KEY) == 1;

        SetButtonColors(sfxOnBtn, sfxOffBtn, sfxOn);
        SetButtonColors(bgmOnBtn, bgmOffBtn, bgmOn);
    }

    void SetButtonColors(Button onBtn, Button offBtn, bool isOn)
    {
        if (onBtn == null || offBtn == null) return;

        onBtn.image.color = isOn ? onColor : offColor;
        offBtn.image.color = isOn ? offColor : onColor;
    }


    void ApplyAudioSettings()
    {
        ApplyBGMSetting();
        ApplySFXSetting();
    }

    void ApplyBGMSetting()
    {
        if (bgmSource == null) return;
        bool isOn = PlayerPrefs.GetInt(BGM_KEY) == 1;
        if (isOn && !bgmSource.isPlaying) bgmSource.Play();
        else if (!isOn && bgmSource.isPlaying) bgmSource.Stop();
    }

    void ApplySFXSetting()
    {
        if (sfxSource == null) return;
        bool isOn = PlayerPrefs.GetInt(SFX_KEY) == 1;
        sfxSource.mute = !isOn;
    }
}
