using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("UI Panels")]
    [SerializeField] internal List<GameObject> panels = new List<GameObject>();
    [SerializeField] internal GameObject firstPanel;
    [SerializeField] internal GameObject createOrJoinPanel;
    [SerializeField] internal GameObject createRoomPanel;

    [SerializeField] internal InputField playerNameInput;
    [SerializeField] internal InputField roomNameInputfield;
    [SerializeField] internal InputField roomMaxPlayersInputfield;
    [SerializeField] internal GameObject roomListContent;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        panels = new List<GameObject>() { firstPanel, createOrJoinPanel, createRoomPanel};
    }

    public void OpenChoosenPanel(GameObject choosenPanel)
    {
        foreach (GameObject panel in panels)
            panel.SetActive(false);
        choosenPanel.SetActive(true);
    }
}
