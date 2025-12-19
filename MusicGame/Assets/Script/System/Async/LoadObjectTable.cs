using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LoadForAsync
{
    public class LoadObjectTable<TClass>
        where TClass : UnityEngine.Object
    {
        public List<LoadObject<TClass>> ObjectTable { get; private set; } = default;

        // 読み込み成功フラグ
        public int successCnt { get; private set; }

        // コンストラクタ
        public void Initilize()
        {
            ObjectTable = new();
            successCnt = 0;
        }

        public void AddGameObject<T>(LoadObject<T> loadObject)
            where T : UnityEngine.Object
        {
            //ObjectTable.Add(loadObject as TClass);
        }

        public async UniTask<bool> LoadAsyncObjects()
        {
            int index = 0;

            while (ObjectTable.Count <= successCnt)
            {
                await ObjectTable[index].AsyncLoadObject();
                successCnt++;
            }

            return true;
        }
    }

    public class LoadObject<TClass>
        where TClass : UnityEngine.Object
    {
        public TClass assets;
        public string filePath;

        public LoadObject(string file)
        {
            assets = null;
            filePath = file;
        }

        public async UniTask<bool> AsyncLoadObject()
        {
            if (assets != null) { return false; }

            ResourceRequest request = Resources.LoadAsync<TClass>(filePath);

            while (!request.isDone)
            {
                await UniTask.Yield();
            }

            this.assets = request.asset as TClass;

            return true;
        }
    }
}
