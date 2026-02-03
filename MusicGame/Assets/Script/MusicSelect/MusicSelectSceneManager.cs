
using System;
using System.Collections.Generic;
using LoadForAsync;
using Notes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using GameInfo;
using System.Text.RegularExpressions;

namespace MusicSelect
{
    public enum SceneState
    {
        MusicSelect,
        LevelSelect,
        EnterGame
    }

    public class MusicSelectSceneManager : MonoBehaviour, ISceneManager
    {
        IResourceManager resManager;
        public IResourceManager Resource => resManager;
        public static float MaxX => 960.0f;
        private Action deleteAction = null, createAction = null;
        AudioSource mAudio;

        private AudioClip enter, cancel, scroll, beep;

        int maxValue;
        int[] mSelectNum;
        public int[] SelectNum => mSelectNum;
        int mPrevSelectNum;
        PropertyController mProperty;
        public int MaxValue => maxValue;
        public MusicDatabase mDataBase;
        string[] mCurrNotesData;
        string mCurrMusicPath;
        public SceneState mSceneState { get; private set; }
        float timer = 0.0f;
        public float untouchableTimer { get; private set; }
        readonly string[] levelName =
        {
            "NORMAL",
            "HARD",
            "EXPERT"
        };

        [SerializeField] NotesManagerDatabase mNotesManager;
        public bool UseKeyboard => mNotesManager != null ? mNotesManager.PlayerConfig.InputDevice == InputDevice.KyeBord : true;

        Dictionary.Dic<string, GameObject> res;
        List<DemoMusicInfo> demoPropertys;
        int mMusicStateMaxValue;

        // 追記
        [SerializeField] private AssetLoadConfig AssetLoadConfig;
        [SerializeField] private List<AssetPair> musicList;
        [SerializeField] private List<AssetPair> jakcetList;
        [SerializeField] private List<TextAssetPair> textList;

        private void Awake()
        {
            Init();

            CreateMusicButtons().Invoke();
        }

        void Update()
        {
            if (timer <= 0.0f && createAction != null)
            {
                createAction?.Invoke();
                createAction = null;
            }
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            if (untouchableTimer > 0.0f)
            {
                untouchableTimer -= Time.deltaTime;
            }
            if (mSceneState == SceneState.MusicSelect && mPrevSelectNum != mSelectNum[0])
            {
                DemoMusicInfo info = demoPropertys[mSelectNum[0]];
                mProperty.SetProperty(info.name, info.audio, info.sprite);
                mPrevSelectNum = mSelectNum[0];
            }
            
        }

        void Init()
        {
            GameObject go = GameObject.Find("ResourceManager");
            if (go)
            resManager = go.GetComponent<Kameda_ResourceManager>();
            
            mSelectNum = new int[3];
            mCurrNotesData = new string[4];
            mSceneState = SceneState.MusicSelect;
            mAudio = GetComponent<AudioSource>();
            enter = resManager.GetAudioClip("Enter");
            cancel = resManager.GetAudioClip("Cancel");
            scroll = resManager.GetAudioClip("Scroll");
            beep = resManager.GetAudioClip("Beep");

            res = new(new()
            {
                { "MusicButton", Resource.GetGameObject("Music_Button", false) },
                { "LevelButton", Resource.GetGameObject("Level_Button", false) },
                { "Property", Resource.GetGameObject("Property", false) },
                { "Popup", Resource.GetGameObject("Popup", false) },
                { "Player", Resource.GetGameObject("Player", false) },
                { "GUI", Resource.GetGameObject("GUI", false) }
            });

            if (!GameObject.Find("Player"))
            {
                CreatePlayer();
            }
        }

        public void GoForward()
        {
            if (untouchableTimer > 0.0f) return;

            switch (mSceneState)
            {
                case SceneState.MusicSelect:
                    if (mSelectNum[0] >= mMusicStateMaxValue - 1)
                    {
                        mAudio.PlayOneShot(beep);
                    }
                    else
                    {
                        mSelectNum[0]++;
                        mAudio.PlayOneShot(scroll);
                    }
                    break;
                case SceneState.LevelSelect:
                    if (mSelectNum[1] > maxValue - 1)
                    {
                        mAudio.PlayOneShot(beep);
                    }
                    else
                    {
                        mSelectNum[1]++;
                        mAudio.PlayOneShot(scroll);
                    }
                    break;
                default:
                    break;
            }

        }
        public void GoBack()
        {
            if (untouchableTimer > 0.0f) return;

            switch (mSceneState)
            {
                case SceneState.MusicSelect:
                    if (mSelectNum[0] <= 0)
                    {
                        mAudio.PlayOneShot(beep);
                    }
                    else
                    {
                        mSelectNum[0]--;
                        mAudio.PlayOneShot(scroll);
                    }
                    break;
                case SceneState.LevelSelect:
                    if (mSelectNum[1] <= 0)
                    {
                        mAudio.PlayOneShot(beep);
                    }
                    else
                    {
                        mSelectNum[1]--;
                        mAudio.PlayOneShot(scroll);
                    }
                    break;
                default:
                    break;
            }
        }
        public void Enter()
        {
            if (untouchableTimer > 0.0f) return;

            switch (mSceneState)
            {
                case SceneState.MusicSelect:
                    if (demoPropertys[SelectNum[0]].name == "戻る")
                    {
                        SceneManager.LoadScene("Test_ModeSelectScene");
                    }
                    else
                    {
                        mCurrNotesData[0] = mDataBase.musicDatabase[mSelectNum[0]].normalPath;
                        mCurrNotesData[1] = mDataBase.musicDatabase[mSelectNum[0]].hardPath;
                        mCurrNotesData[2] = mDataBase.musicDatabase[mSelectNum[0]].expertPath;
                        mCurrMusicPath = mDataBase.musicDatabase[mSelectNum[0]].musicPath;
                        DeleteAndExecuteAction(CreateLevelButtons());
                        mAudio.PlayOneShot(enter);
                        timer = 0f;
                        untouchableTimer = 0.4f;
                    }
                    
                    break;
                case SceneState.LevelSelect:
                    if (mCurrNotesData[SelectNum[1]] == string.Empty)
                    {
                        mAudio.PlayOneShot(beep);
                        return;
                    }
                    else
                    {
                        DeleteAndExecuteAction(() => deleteAction += MakePopupInstance());
                        mAudio.PlayOneShot(enter);
                        timer = 0.1f;
                        untouchableTimer = 0.1f;
                    }
                    break;
                case SceneState.EnterGame:
                    Resource.ReleaseAll?.Invoke();
                    SingletonDataManager.instance.SetMusicId(mDataBase.musicDatabase[SelectNum[0]]);
                    LoadSceneRef(mCurrMusicPath, mCurrNotesData[SelectNum[1]]);
                    break;
            }
            if (mSceneState != SceneState.EnterGame)
            {
                mSceneState++;
            }
        }
        public void Undo()
        {
            if (untouchableTimer > 0.0f) return;

            switch (mSceneState)
            {
                case SceneState.MusicSelect:
                    SceneManager.LoadScene("Test_ModeSelectScene");
                    break;
                case SceneState.LevelSelect:
                    DeleteAndExecuteAction(CreateMusicButtons(SelectNum[0]));
                    mAudio.PlayOneShot(cancel);
                    timer = 0f;
                    untouchableTimer = 0.4f;
                    mPrevSelectNum = mSelectNum[0];
                    break;
                case SceneState.EnterGame:
                    DeleteAndExecuteAction(CreateLevelButtons(mSceneState));
                    mAudio.PlayOneShot(cancel);
                    timer = 0f;
                    untouchableTimer = 0.4f;
                    break;
            }
            if (mSceneState > SceneState.MusicSelect)
            {
                mSceneState--;
            }
        }

        Player_forMusicSelect CreatePlayer()
        {
            GameObject res = this.res.GetValue("Player");
            GameObject instance = Instantiate(res);
            return instance.GetComponent<Player_forMusicSelect>();
        }

        Action CreateMusicButtons(int currentNum = 0)
            //currentNum : 入るときに選択されている番号
        {
            Action f = () => {
                (PropertyController, Action) tuple =  PropertyController.CreateInstance(res.GetValue("Property"));
                mProperty = tuple.Item1;
                deleteAction += tuple.Item2;
                demoPropertys = new();
                int num = 0;
                foreach (MusicData m in mDataBase.musicDatabase)
                {
                    deleteAction += MusicButtonController.CreateInstance(res.GetValue("MusicButton"), m.name, num++, currentNum);
                    demoPropertys.Add(new(m.name, Resource.GetAudioClip(m.musicScriptData.fileName), Resource.GetSprite(m.musicScriptData.fileName)));
                }
                //末尾に戻るボタンを追加
                deleteAction += MusicButtonController.CreateInstance(res.GetValue("MusicButton"), "戻る", num++, currentNum);
                demoPropertys.Add(new("戻る", null, null));
                mMusicStateMaxValue = num;

                //deleteAction += GUIController.CreateInstance(res.GetValue("GUI"), "決定(A)", new(-180.0f, -50.0f));
                //deleteAction += GUIController.CreateInstance(res.GetValue("GUI"), "戻る(B)", new(0.0f, -50.0f));
                mPrevSelectNum = mSelectNum[0];
                mProperty.SetProperty(demoPropertys[mPrevSelectNum].name, demoPropertys[mPrevSelectNum].audio, demoPropertys[mPrevSelectNum].sprite);
            };
            return f;
        }

        Action CreateLevelButtons(SceneState ss = SceneState.LevelSelect)
        {
            Action f = () => {
                maxValue = 0;
                for (int i = 0; i < 3; ++i)
                {
                    deleteAction += LevelButtonController.CreateInstance(res.GetValue("LevelButton"), i, levelName[i], ss, mCurrNotesData[i] == string.Empty);
                    maxValue++;
                }
                deleteAction += LevelButtonController.CreateInstance(res.GetValue("LevelButton"), maxValue++, "戻る", ss);
                //deleteAction += GUIController.CreateInstance(res.GetValue("GUI"), "決定(A)", new(-180.0f, -50.0f));
                //deleteAction += GUIController.CreateInstance(res.GetValue("GUI"), "戻る(B)", new(0.0f, -50.0f));
            };
            return f;
        }

        void DeleteAndExecuteAction(Action action)
        {
            deleteAction?.Invoke();
            deleteAction = null;
            createAction += action;
        }

        Action MakePopupInstance()
        {
            var loadObj = res.GetValue("Popup");

            GameObject go = Instantiate(loadObj);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.transform.localScale = Vector3.one;

            Action f = () => { Destroy(go); };
            return f;
        }

        // 追記
        private async void LoadSceneRef(string musicPath_, string notesPath_)
        {
            // 直接書き換えない
            var references = new AssetLoadConfig();
            references = AssetLoadConfig;

            // 選択された楽曲を設定
            foreach (var obj in references.ReferencesAssets)
            {
                if (obj.ObjectPath == "Music")
                {
                    // リストの中から musicPath_ と一致するものを探す
                    var found = musicList.Find(x => x.key == musicPath_);
                    if (found != null) obj.AssetReference = found.assetRef;
                }
            }

            // 選択された楽曲を設定
            {
                var found = jakcetList.Find(x => x.key == musicPath_);
                AssetReferenceObject asset = new("MusicJakcet", found.assetRef);
                references.ReferencesAssets.Add(asset);
            }

            // 選択されたノーツファイルを設定
            {
                var found = textList.Find(x => x.key == notesPath_);
                if (found != null)
                {
                    int index = 1;
                    foreach (var text in found.assetRefs)
                    {
                        AssetReferenceObject asset =
                            new("TextAsset_" + index.ToString(), text);
                        references.ReferencesAssets.Add(asset);
                        index++;
                    }                    
                }
            }

            // 難易度の定義
            {
                var level = Regex.Match(notesPath_, @"_(.*?)\.").Groups[1].Value;
                SingletonDataManager.instance.SetMusicLevel(level);
            }

            string naxtSceneName = "Ooo_MainTest";
            await DataTransferSystem.LoadSceneRef(references, naxtSceneName);
        }
    }
    public interface ISceneManager : IMusicSelecter, ILevelSelecter
    {
        bool UseKeyboard { get; }
        IResourceManager Resource { get; }
    }

    public interface IMusicSelecter
    {
        int[] SelectNum { get; }
        void GoForward();
        void GoBack();
        void Enter();
        void Undo();
    }

    public interface ILevelSelecter
    {
        SceneState mSceneState { get; }
        int[] SelectNum { get; }
        int MaxValue { get; }
        float untouchableTimer { get; }
    }

    public class DemoMusicInfo
    {
        public string name;
        public AudioClip audio;
        public Sprite sprite;
        public DemoMusicInfo(string name, AudioClip audio, Sprite sprite)
        {
            this.name = name;
            this.audio = audio;
            this.sprite = sprite;
        }
    }

    [System.Serializable]
    public class AssetPair
    {
        public string key; 
        public AssetReference assetRef;
    }

    [System.Serializable]
    public class TextAssetPair
    {
        public string key;
        public List<AssetReference> assetRefs;
    }
}
