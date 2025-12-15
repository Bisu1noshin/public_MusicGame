using UnityEngine;
using Cysharp.Threading.Tasks;

namespace LoadForAsync
{
    public class LoadObjectForAsync<TClass>
        where TClass : UnityEngine.Object
    {

        private readonly string FilePath;

        public TClass Object { get; private set; } = default;

        public LoadObjectForAsync(string filePath)
        {
            FilePath = filePath;
        }

        public async UniTask AsyncLoad()
        {
            ResourceRequest request = Resources.LoadAsync<TClass>(FilePath);

            while (!request.isDone)
            {
                await UniTask.Yield();
            }

            this.Object = request.asset as TClass;
        }
    }
}

