using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.UI;

namespace OnLine
{
    public struct SendHostReadyData : NetworkMessage
    {
        public bool IsHostReady;
    }

    public class TestNetworkDiscovery : NetworkDiscovery
    {
        private ServerResponse _discoveredServer;

        private void OnDestroy()
        {
            //シーン遷移などで破棄されたタイミングで検索をやめる
            StopDiscovery();
        }

        private void Awake()
        {
            //データ受信の準備
            NetworkClient.RegisterHandler<SendHostReadyData>(ReceivedReadyInfo);
            //NetworkClient.RegisterHandler<SendPlayerCountData>(ReceivedPlayerCountInfo);

            //サーバー見つけたらこれが呼ばれる
            OnServerFound.AddListener(serverResponse =>
            {
                //見つけたサーバーをServerResponseに登録
                _discoveredServer = serverResponse;
                Debug.Log("ServerFound");
            });
        }

        private void ReceivedReadyInfo(SendHostReadyData receivedData)
        {

        }
    }
}

