using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public static LoginManager instance;
    [Header("Prefabs")]
    // [SerializeField] GameObject playerListPrefab;
    [SerializeField] GameObject roomListPrefab;

    internal Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<int, GameObject> playerListGameObjects;
    private Dictionary<string, GameObject> roomListGameobjects;
    
    List<PlayerListEntryInitializer> playerPrefabList = new List<PlayerListEntryInitializer>();

    void Start()
    {
        if (PhotonNetwork.InRoom) Debug.Log("CURRENT ROOM NAME: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.AutomaticallySyncScene = true;
        //UIManager.instance.OpenChoosenPanel(UIManager.instance.firstPanel);

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameobjects = new Dictionary<string, GameObject>();
    }

    #region Photon Methods
    public void ConnectToPhotonServer()
    {
        if (UIManager.instance.playerNameInput.text != null)
        {
            PhotonNetwork.NickName = UIManager.instance.playerNameInput.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " is OnConnected to Master");
        if (PhotonNetwork.IsConnectedAndReady)
            PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to lobby");
        UIManager.instance.OpenChoosenPanel(UIManager.instance.createOrJoinPanel);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created room by " + PhotonNetwork.NickName);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " on Joined to Room");
        PhotonNetwork.LoadLevel("GameScene");
    }

    #endregion


    #region Speacial Methods
    public void CreateRoomClick()
    {
        string roomName = UIManager.instance.roomNameInputfield.text;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        int maxPlayersCount = int.Parse(UIManager.instance.roomMaxPlayersInputfield.text);
        roomOptions.MaxPlayers = ((byte)maxPlayersCount);

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    #endregion

    #region Photon Methods
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(room.Name))
                    cachedRoomList.Remove(room.Name);
            }
            else
            {
                if (cachedRoomList.ContainsKey(room.Name))
                    cachedRoomList[room.Name] = room;
                else
                    cachedRoomList.Add(room.Name, room);
            }
        }
        foreach (RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomListEntrygo = Instantiate(roomListPrefab);
            roomListEntrygo.transform.SetParent(UIManager.instance.roomListContent.transform);
            roomListEntrygo.transform.localScale = Vector3.one;

            roomListEntrygo.transform.Find("RoomNameText").GetComponent<Text>().text = room.Name;
            roomListEntrygo.transform.Find("RoomPlayerCountText").GetComponent<Text>().text = room.PlayerCount + "/" + room.MaxPlayers;
            roomListEntrygo.transform.Find("RoomJoinButton").GetComponent<Button>().onClick.AddListener(() => OnJoinRoomButtonClicked(room.Name));
        }
    }
    private void OnJoinRoomButtonClicked(string _roomName)
    {
        PhotonNetwork.JoinRoom(_roomName);
    }
    #endregion
}
