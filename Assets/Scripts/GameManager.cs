using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject loading;

    [Header("LOBBY")]
    [SerializeField] private GameObject lobby;
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private TMP_InputField createRoomField;
    [SerializeField] private TMP_InputField joinRoomField;

    [Header("PLAYER")]
    [SerializeField] private SpawnPlayer spawnPlayer;

    [Header("GAME PLAY")]
    [SerializeField] private TextMeshProUGUI playerScoreTxt;
    [SerializeField] private TextMeshProUGUI opponentScoreTxt;
    #endregion

    #region UNITY CALLBACKS
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        loading.SetActive(true);
        lobby.SetActive(false);
    }
    #endregion

    #region LOBBY
    internal void ShowLobby()
    {
        loading.SetActive(false);
        lobby.SetActive(true);
    }

    public void CreateRoom()
    {
        Server.Instance.CreateRoom(createRoomField.text);
    }

    public void JoinRoom()
    {
        Server.Instance.JoinRoom(joinRoomField.text);
    }

    internal void OnJoinedRoom()
    {
        SetPlayerProperties();
        StartGame();
    }
    #endregion

    #region GAMEPLAY
    private void SetPlayerProperties()
    {
        Server.Instance.SetNickName(playerName.text);

        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("score", "0");

        Server.Instance.SetCustomProperties(data);
    }

    private void StartGame()
    {
        lobby.SetActive(false);
        spawnPlayer.Spawn();
    }

    internal void UpdatePlayerScoreUI(int actorNumber, string score)
    {
        if (actorNumber == 1)
        {
            playerScoreTxt.text = score;
        }
        else
        {
            opponentScoreTxt.text = score;
        }
    }
    #endregion
}