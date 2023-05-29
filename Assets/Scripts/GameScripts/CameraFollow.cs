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
            if (item.GetComponent<PhotonView>().IsMine)
            {
                player = item.GetComponent<Transform>();
            }
        }
    }
    void Update()
    {
        foreach (var item in SpawnManager.instance.playerPrefabList)
        {
            if (item.GetComponent<PhotonView>().IsMine)
            {
                transform.position = new Vector3(item.transform.position.x, item.transform.position.y, -10);
                Debug.Log("isMine Player pos = " + item.GetComponent<Transform>().position);
            }
        }

    }
}