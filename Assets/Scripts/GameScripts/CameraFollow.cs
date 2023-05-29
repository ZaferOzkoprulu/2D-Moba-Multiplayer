using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviourPun
{
    [SerializeField] Transform player;
    [SerializeField] float camDistance;
    void Start()
    {
        foreach (var item in SpawnManager.instance.playerPrefabList)
        {
            if (item.transform.GetChild(0).GetComponent<PhotonView>().IsMine)
            {
                player = item.transform.GetChild(0).GetComponent<Transform>();
            }
        }
    }
    void Update()
    {
        foreach (var item in SpawnManager.instance.playerPrefabList)
        {
            if (item.transform.GetChild(0).GetComponent<PhotonView>().IsMine)
            {
                transform.position = new Vector3(item.transform.GetChild(0).position.x, item.transform.GetChild(0).position.y, -10);
                Debug.Log("isMine Player pos = " + item.transform.GetChild(0).GetComponent<Transform>().position);
            }
        }

    }
}