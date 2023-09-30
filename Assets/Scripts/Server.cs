using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviourPunCallbacks
{
    #region VARIABLES
    public static Server Instance { get; private set; }
    #endregion

    #region UNITY CALLBACKS
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        // Connect to the Photon server.
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    #region SERVER CALLBACKS
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    #endregion

    #region LOBBY CALLBACKS
    public override void OnJoinedLobby()
    {
        GameManager.Instance.ShowLobby();
    }

    public override void OnJoinedRoom()
    {
        GameManager.Instance.OnJoinedRoom();
    }

    internal void CreateRoom(string roomID)
    {
        //Optional
        //Set your room properties if need
        RoomOptions roomOptions = new RoomOptions() {
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    internal void JoinRoom(string roomID)
    {
        PhotonNetwork.JoinRoom(roomID);
    }
    #endregion

    #region INSTANTIATE
    internal void InstantiateGameObject(string goName, Vector3 pos, Quaternion rotation)
    {
       PhotonNetwork.Instantiate(goName, pos, rotation);
    }
    #endregion

    #region CUSTOM PROPERTIES
    internal void SetCustomProperties(Dictionary<string, string> dataDictionary)
    {
        Hashtable customProperties = new Hashtable();

        //Add properties as you need
        foreach (KeyValuePair<string, string> pair in dataDictionary)
        {
            customProperties[pair.Key] = pair.Value;
        }

        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
    }

    internal void UpdateCustomProperty(string key, string value)
    {
        Hashtable customProperties = new Hashtable();
        customProperties[key] = value;

        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
    }

    internal string GetCustomProperty(string key)
    {
        return PhotonNetwork.LocalPlayer.CustomProperties[key].ToString();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        //Each player in a room is assigned a different ActorNumber.
        //This ensures that no two players within the same room share the same identifier.
        //The first player to join typically gets an ActorNumber of 1, the second player gets 2, and so on.

        if (changedProps["score"] != null)
        {
            GameManager.Instance.UpdatePlayerScoreUI(targetPlayer.ActorNumber, changedProps["score"].ToString());
        }  
    }
    #endregion

    #region PLAYER
    internal Player GetLocalPlayer()
    {
        return PhotonNetwork.LocalPlayer;
    }

    internal void SetNickName(string name)
    {
        PhotonNetwork.NickName = name;
    }

    internal string GetNickName(PhotonView photonView)
    {
        return photonView.Owner.NickName;
    }

    internal Player[] GetOtherPlayers()
    {
        return PhotonNetwork.PlayerListOthers;
    }
    #endregion
}