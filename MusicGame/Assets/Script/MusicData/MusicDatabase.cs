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
    public int id;
    public string name;
    public string composerName; //作曲者名
    public string musicPath; //"folder/name.mp3"
    public string demoMusicPath;
    public int BPM;
    public string jacketPath;
    public string normalPath;
    public string hardPath;
    public string expertPath;
    public Notes.NotesData[] notesData = new Notes.NotesData[4];
    public MusicScriptData musicScriptData;
}

[System.Serializable]
public sealed class MusicScriptData
{
    [Tooltip("コード上で呼び出す際の名前")]
    public string fileName;
}