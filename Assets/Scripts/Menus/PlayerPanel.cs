using System;
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

    void Start()
    {
        initControlDropDown();
    }

    private void initControlDropDown()
    {
        //Retreive DropDown
        Dropdown playerControl = retreiveChild("slt_control").GetComponent<Dropdown>();
        playerControl.ClearOptions();

        //Get Joystick Name
        String[] joystickNames = Input.GetJoystickNames();
        String joystickName = null;
        if (playerId < joystickNames.Length)
        {
            int joystickId = InputManager.instance.JoysticResTable[playerId];
            if (joystickId != -1) {
                joystickName = joystickNames[joystickId];
            }
        }

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData(InputManager.KeyMapping.Disabled.ToString()));
        options.Add(new Dropdown.OptionData(InputManager.KeyMapping.KeyBoard.ToString()));
        if (joystickName != null && joystickName != "")
        {
            options.Add(new Dropdown.OptionData(InputManager.instance.GetKeymapFromJoytickName(joystickName).ToString()));
        }
        playerControl.AddOptions(options);
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
                lobbyPlayer.KeyMap = InputManager.instance.GetKeymapFromJoytickName(
                    InputManager.instance.GetJoystickNameFromPlayerId(playerId));
                break;
        }

        //Notify LobbyManager
        LobbyManager.instance.LobbyPlayers[playerId] = lobbyPlayerPrefab;
    }

    private GameObject retreiveChild(string name)
    {
        Transform childTransform = transform.Find(name);
        return childTransform.gameObject;
    }
}
