using Mirror;
using Steamworks;
using UnityEngine;

public class TestSteam : MonoBehaviour
{
    // コールバック（Steamからの返答を受け取る窓口）
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<LobbyMatchList_t> lobbyMatchList;

    private NetworkManager networkManager;
    private const string HostAddressKey = "HostAddress";

    void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized) return;

        // コールバックの初期化
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        lobbyMatchList = Callback<LobbyMatchList_t>.Create(OnLobbyMatchList);
    }

    private void OnDestroy()
    {
        // Mirrorのホストを開始
        if (NetworkServer.active)
            networkManager.StopHost();
    }

    // ボタン等から呼ぶ：ロビー作成開始
    public void HostLobby()
    {
        // 公開ロビーを最大4人で作成
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 4);
    }

    // Steam側でロビーが作られたら呼ばれる
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) return;

        // Mirrorのホストを開始
        networkManager.StartHost();

        // ロビーデータに自分のSteamIDを書き込む（これを見た人が接続してくる）
        CSteamID lobbyId = new CSteamID(callback.m_ulSteamIDLobby);
        SteamMatchmaking.SetLobbyData(lobbyId, HostAddressKey, SteamUser.GetSteamID().ToString());

        Debug.Log("Lobby Created Successfully!");
    }

    // Steamの「フレンドのゲームに参加」を押した時に呼ばれる
    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    // ロビーに入場した時に呼ばれる（ホスト・ゲスト両方）
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) return; // ホストなら何もしない

        // ロビーデータからホストのSteamIDを取得して接続
        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();
    }
    
    // ロビーを検索するボタンから呼ぶ
    public void FindLobbies()
    {
        Debug.Log("ロビーを検索中...");
        // フィルターなしで検索（特定の条件で絞り込むことも可能）
        SteamMatchmaking.RequestLobbyList();
    }

    // 検索結果が返ってきたら呼ばれる
    void OnLobbyMatchList(LobbyMatchList_t callback)
    {
        Debug.Log($"{callback.m_nLobbiesMatching} 件のロビーが見つかりました");

        for (int i = 0; i < callback.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            // ここでは、最初に見つかったロビーに自動で参加する例
            SteamMatchmaking.JoinLobby(lobbyID);
            break;
        }
    }
}
