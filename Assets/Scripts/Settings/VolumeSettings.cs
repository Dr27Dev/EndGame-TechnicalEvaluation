using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _master;
    [SerializeField] private Slider _bgm;
    [SerializeField] private Slider _sfx;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume_Master")) _master.value = PlayerPrefs.GetFloat("Volume_Master");
        else _master.value = 1;
        if (PlayerPrefs.HasKey("Volume_BGM")) _bgm.value = PlayerPrefs.GetFloat("Volume_BGM");
        else _bgm.value = 1;
        if (PlayerPrefs.HasKey("Volume_SFX")) _sfx.value = PlayerPrefs.GetFloat("Volume_SFX");
        else _sfx.value = 1;

        _master.onValueChanged.AddListener(SetMaster);
        _bgm.onValueChanged.AddListener(SetBGM);
        _sfx.onValueChanged.AddListener(SetSFX);
    }

    private void Start()
    {
        _mixer.SetFloat("MasterVol", SliderToDecibel(_master.value));
        _mixer.SetFloat("MusicVol", SliderToDecibel(_bgm.value));
        _mixer.SetFloat("SFXVol", SliderToDecibel(_sfx.value));
    }

    public void SetMaster(float sliderValue)
    {
        _mixer.SetFloat("MasterVol", SliderToDecibel(sliderValue));
        PlayerPrefs.SetFloat("Volume_Master", sliderValue);
    }

    public void SetBGM(float sliderValue)
    {
        _mixer.SetFloat("MusicVol", SliderToDecibel(sliderValue));
        PlayerPrefs.SetFloat("Volume_BGM", sliderValue);
    }

    public void SetSFX(float sliderValue)
    {
        _mixer.SetFloat("SFXVol", SliderToDecibel(sliderValue));
        PlayerPrefs.SetFloat("Volume_SFX", sliderValue);
    }

    private float SliderToDecibel(float sliderValue)
    {
        return Mathf.Lerp(-40f, 0f, sliderValue);
    }
}
