using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicDatabase : ScriptableObject
{
    public List<MusicData> musicDatabase;
}

[System.Serializable]
public class MusicData
{
    public int musicNo;
    public string musicName;
    public string composerName;
    public string musicPath; //"Resource/name.mp3"
    public int BPM;
}