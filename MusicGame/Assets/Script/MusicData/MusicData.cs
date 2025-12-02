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
    public string musicPath; //"Resource/name.mp3"
    public string demoMusicPath;
    public int BPM;
    public Notes.NotesData[] notesData = new Notes.NotesData[4];
}