using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LoadForAsync
{
    public sealed class LoadSceneManager<TClass> : MonoBehaviour
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
        private void Start()
        {
            
        }
    }
}
