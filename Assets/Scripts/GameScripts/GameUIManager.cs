using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;
    [SerializeField] internal Button kickButton;
    [SerializeField] internal Text redGoalText;
    [SerializeField] internal Text blueGoalText;
    [SerializeField] internal Text teamWinText;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

}
