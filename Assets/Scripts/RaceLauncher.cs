using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public InputField playerName;
    byte maxPlayerPerRoom = 4;
    bool isConnecting;
    public Text networkText;
    string gameVersion = "2";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PlayerPrefs.HasKey("PlayerName")) playerName.text =
        PlayerPrefs.GetString("PlayerName");
    }
    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }
    public void StartTrial()
    {
        SceneManager.LoadScene(1);
    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting = true;

        PhotonNetwork.NickName = playerName.text;
        PhotonNetwork.GameVersion = gameVersion;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            networkText.text += "Joining room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting...\n";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        networkText.text += "Connected to Master Server.\n";

        if (isConnecting)
        {
            networkText.text += "Joining random room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join random room. \n";

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayerPerRoom });
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because " + cause + "\n";
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        networkText.text += "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players.\n";
        PhotonNetwork.LoadLevel("Track");
    }


}
