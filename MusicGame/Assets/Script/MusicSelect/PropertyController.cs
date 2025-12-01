using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PropertyController : MonoBehaviour
{
    TextMeshProUGUI mText;
    AudioSource mAudio;
    Image mThumbnail;
    private void Awake()
    {
        mAudio = GetComponent<AudioSource>();
        mThumbnail = transform.GetChild(0).GetComponent<Image>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetProperty(string text, string audioPath, string imagePath)
    {
        mText.text = text;
        if (audioPath != null) 
        {
            mAudio.resource = Resources.Load(CreateMusicPath(audioPath)) as AudioClip;
            mAudio.Play();
        }
        if (imagePath != null) 
        {
            mThumbnail = Resources.Load<Image>(imagePath);
        }
    }
    string CreateMusicPath(string path_)
    {
        string res = "Music/" + path_;
        return res;
    }
}
