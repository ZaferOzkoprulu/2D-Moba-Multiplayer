using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerListEntryInitializer : MonoBehaviour
{
    //[Header("UI References")]
    [SerializeField] Text playerNameText;
    [SerializeField] internal Dropdown playerTeamDropdownMenu;
    [SerializeField] internal Toggle playerReadyToggle;
    internal int actorNum;
    private bool isPlayerReady = false;
    internal string playerTeamRed = "0";

    public void Initialize(int playerID, string playerName)
    {
        playerNameText.text = playerName;

        if (PhotonNetwork.LocalPlayer.ActorNumber != playerID)
        {
            // playerTeamDropdownMenu.interactable = false;
        }
        else
        {
            ExitGames.Client.Photon.Hashtable initalProps = new ExitGames.Client.Photon.Hashtable() { { PhotonPrototypeConstants.PLAYER_READY, isPlayerReady } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(initalProps);

            playerReadyToggle.onValueChanged.AddListener((x) =>
            {
                isPlayerReady = !isPlayerReady;

                ExitGames.Client.Photon.Hashtable newProps = new ExitGames.Client.Photon.Hashtable() { { PhotonPrototypeConstants.PLAYER_READY, isPlayerReady } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(newProps);
            });
        }
    }
}
