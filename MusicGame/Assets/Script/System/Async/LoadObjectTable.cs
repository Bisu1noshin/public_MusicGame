using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace LoadForAsync
{
    public class LoadObjectTable<TClass>
        where TClass : UnityEngine.Object
    {
        public List<LoadObject<TClass>> ObjectTable { get; private set; }
        public int successCnt { get; private set; }

        // コンストラクタ
        public void Initilize(List<LoadObject<TClass>> loadObjects)
        {
            ObjectTable = loadObjects;
            successCnt = 0;
        }

        public async UniTask AsyncLoadObject(int index)
        {
            bool success = await ObjectTable[index].AsyncLoadObject();
            if (success) successCnt++;
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

            assets =
                await LoadObjectForAsync.AsyncLoad<TClass>(filePath);

            return true;
        }
    }
}
