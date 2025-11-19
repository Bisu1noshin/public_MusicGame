using UnityEngine;
using UnityEngine.Audio;

public enum EAudioMixerType {Maseter, BGM};
public class Ooo_AudioManager : MonoBehaviour
{
    public static Ooo_AudioManager instance;
    [SerializeField] private AudioMixer mixer;

    private bool[] isMute = new bool[2];
    private float[] audioVolume = new float[2];

    private void Awake()
    {
        instance = this;
    }
    public void SetVolume(EAudioMixerType audioMixerType, float volume)
    {
        mixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
    }

    public void SetMute(EAudioMixerType audioMixerType)
    {
        int type = (int)audioMixerType;
        if (!isMute[type])
        {
            isMute[type] = true;
            mixer.GetFloat(audioMixerType.ToString(), out float curVolume);
            audioVolume[type] = curVolume;
            SetVolume(audioMixerType, 0.001f);
        }
        else
        {
            isMute[type] = false;
            SetVolume(audioMixerType, audioVolume[type]);
        }
    }

    public void Mute()
    {
        Ooo_AudioManager.instance.SetMute(EAudioMixerType.BGM);
    }

    public void ChangeVolume(float volume)
    {
        Ooo_AudioManager.instance.SetVolume(EAudioMixerType.BGM, volume);
    }
}
