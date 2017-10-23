using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField]
    private int playerId;
    [SerializeField]
    private string playerName = "";
    [SerializeField]
    private string playerControl = "Disabled";
    [SerializeField]
    private GameObject lobbyPlayerPrefab;

    public int PlayerId
    {
        get
        {
            return playerId;
        }

        set
        {
            playerId = value;
        }
    }

    public string PlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public string PlayerControl
    {
        get
        {
            return playerControl;
        }

        set
        {
            playerControl = value;
        }
    }

    public void SetReady()
    {
        //Set widget uninteractable
        InputField playerName = retreiveChild("txt_playerName").GetComponent<InputField>();
        playerName.interactable = false;
        Dropdown playerClass = retreiveChild("slt_class").GetComponent<Dropdown>();
        playerClass.interactable = false;
        Dropdown playerControl = retreiveChild("slt_control").GetComponent<Dropdown>();
        playerControl.interactable = false;
        Button readyButton = retreiveChild("btn_ready").GetComponent<Button>();
        readyButton.interactable = false;

        //Alter lobbyPlayer according to the values of the widgets
        lobbyPlayerPrefab = Instantiate(lobbyPlayerPrefab);
        LobbyPlayer lobbyPlayer = lobbyPlayerPrefab.GetComponent<LobbyPlayer>();
        lobbyPlayer.PlayerId = playerId;

        switch(playerControl.value)
        {
            case 0:
				lobbyPlayer.KeyMap = InputManager.KeyMapping.Disabled;
                break;
            case 1:
                lobbyPlayer.KeyMap = InputManager.KeyMapping.KeyBoard;
                break;
            case 2:
                lobbyPlayer.KeyMap = InputManager.KeyMapping.LogitechDualAction;
                break;
            case 3:
                lobbyPlayer.KeyMap = InputManager.KeyMapping.LogitechF310;
                break;
            case 4:
                lobbyPlayer.KeyMap = InputManager.KeyMapping.XBox360;
                break;
        }

        //Notify LobbyManager
        LobbyManager.instance.LobbyPlayers[playerId] = lobbyPlayerPrefab;
        LobbyManager.instance.ReadyTab[playerId] = true;
    }

    private GameObject retreiveChild(string name)
    {
        Transform childTransform = transform.Find(name);
        return childTransform.gameObject;
    }
}
