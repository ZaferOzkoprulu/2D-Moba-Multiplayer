using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (photonView.IsMine)
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void GetPlayersLeave()
    {
        Debug.Log("GetPlayersLeave");
        PhotonNetwork.LeaveRoom();
    }
}
