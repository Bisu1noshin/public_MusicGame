using UnityEngine;
using Cysharp.Threading.Tasks;

namespace LoadForAsync
{
    public class LoadObjectForAsync
    {
        public static async UniTask<TClass> AsyncLoad<TClass>(string filePath)
            where TClass : UnityEngine.Object
        {
            ResourceRequest request = Resources.LoadAsync<TClass>(filePath);

            while (!request.isDone)
            {
                await UniTask.Yield();
            }

            return request.asset as TClass;
        }
    }
}

