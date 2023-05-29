using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [Header("Spawn Player")]
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] internal GameObject SkillShotBall;
    [SerializeField] GameObject playerParent;
    [SerializeField] internal List<GameObject> playerPrefabList = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < 30; i++)
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                SpawnNow();
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
    Vector2 RandomPos()
    {
        Vector2 vec = new Vector2(Random.Range(0, 1), Random.Range(0, 1));
        return vec;
    }
    void SpawnNow()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject mp = PhotonNetwork.Instantiate(PlayerPrefab.name, RandomPos(), Quaternion.identity);
            playerPrefabList.Add(mp);
        }
        else
        {
            GameObject op = PhotonNetwork.Instantiate(PlayerPrefab.name, RandomPos(), Quaternion.identity);
            playerPrefabList.Add(op);
        }

    }
}
