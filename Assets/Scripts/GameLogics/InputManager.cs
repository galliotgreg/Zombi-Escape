using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static InputManager instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        initJoystickIdResultutionTab();
    }

    public enum ActionControl
    {
        MoveFwd,
        MoveBck,
        TurnRight,
        TurnLeft,
        StraffRight,
        StraffLeft,
        Fire,
        Reload,
        Heal,
        ToggleLight,
        Run
    }

    public enum KeyMapping
    {
		Disabled,
        KeyBoard,
        LogitechDualAction,
        LogitechF310,
        XBox360,
        PS3,
    }

    [SerializeField]
    private int[] joysticResTable = {-1, -1, -1, -1};

    public int[] JoysticResTable
    {
        get
        {
            return joysticResTable;
        }

        set
        {
            joysticResTable = value;
        }
    }

    // Use this for initialization
    void Start () {
       
    }

    private void initJoystickIdResultutionTab()
    {
        int curInd = 0;
        String[] joystickNames = Input.GetJoystickNames();
        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (curInd >= 4)
            {
                break;
            }
            if (joystickNames[i] != null && joystickNames[i] != "")
            {
                JoysticResTable[curInd++] = i;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    

    public String GetJoystickNameFromPlayerId(int playerId)
    {
        int joystickId = InputManager.instance.JoysticResTable[playerId];
        if (joystickId == -1)
        {
            return null;
        }

        String joystickName = Input.GetJoystickNames()[joystickId];
        return joystickName;
    }

    public KeyMapping GetKeymapFromJoytickName(String joystikName)
    {
        switch (joystikName)
        {
            case "Controller (XBOX 360 For Windows)":
                return KeyMapping.XBox360;
            case "Controller (Gamepad F310)":
                return KeyMapping.LogitechF310;
            case "Logitech Dual Action":
                return KeyMapping.LogitechDualAction;
            case "PLAYSTATION(R)3 Controller":
                return KeyMapping.PS3;
        }
        Debug.Log(joystikName + " Not recognized. XBox Map setted by default");
        return KeyMapping.XBox360;
    }

    public float GetAxis(int playerId, KeyMapping keyMap, ActionControl action)
    {
        if (keyMap == KeyMapping.KeyBoard)
        {
            bool isAction = GetKey(playerId, keyMap, action);
            if (isAction)
            {
                switch (action)
                {
                    case ActionControl.MoveFwd:
                        return 1;
                    case ActionControl.MoveBck:
                        return -1;
                    case ActionControl.StraffRight:
                        return 1;
                    case ActionControl.StraffLeft:
                        return -1;
                    case ActionControl.TurnRight:
                        return -1;
                    case ActionControl.TurnLeft:
                        return 1;
                }
            }
        } else { 
            String axisName = ResolveAxis(playerId, keyMap, action);
            return -Input.GetAxis(axisName);
        }

        return 0;
    }

    public bool GetKeyDown(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = ResolveKeyCode(playerId, keyMap, action);
        if (keyCode != KeyCode.None)
        {
            return Input.GetKeyDown(keyCode);
        }
        return false;
    }

    public bool GetKeyUp(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = ResolveKeyCode(playerId, keyMap, action);
        if (keyCode != KeyCode.None)
        {
            return Input.GetKeyUp(keyCode);
        }
        return false;
    }

    public bool GetKey(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = ResolveKeyCode(playerId, keyMap, action);
        if (keyCode != KeyCode.None)
        {
            return Input.GetKey(keyCode);
        }
        return false;
    }

    private string ResolveAxis(int playerId, KeyMapping keyMap, ActionControl action)
    {
        String axisName = null;
        switch (keyMap)
        {
            case KeyMapping.LogitechDualAction:
                axisName = ResolveLogitechDualActionAxis(playerId, action);
                break;
            case KeyMapping.LogitechF310:
                axisName = ResolveLogitechF310Axis(playerId, action);
                break;
            case KeyMapping.XBox360:
                axisName = ResolveXBox360Axis(playerId, action);
                break;
            case KeyMapping.PS3:
                axisName = ResolvePS3Axis(playerId, action);
                break;
        }
        return axisName;
    }

    private string ResolveXBox360Axis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move";
                    case 1:
                        return "Joystick2_Move";
                    case 2:
                        return "Joystick3_Move";
                    case 3:
                        return "Joystick4_Move";
                    case 4:
                        return "Joystick5_Move";
                    case 5:
                        return "Joystick6_Move";
                    case 6:
                        return "Joystick7_Move";
                    case 7:
                        return "Joystick8_Move";
                }
                break;
            case ActionControl.StraffRight:
            case ActionControl.StraffLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Straff";
                    case 1:
                        return "Joystick2_Straff";
                    case 2:
                        return "Joystick3_Straff";
                    case 3:
                        return "Joystick4_Straff";
                    case 4:
                        return "Joystick5_Straff";
                    case 5:
                        return "Joystick6_Straff";
                    case 6:
                        return "Joystick7_Straff";
                    case 7:
                        return "Joystick8_Straff";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Turn";
                    case 1:
                        return "Joystick2_Turn";
                    case 2:
                        return "Joystick3_Turn";
                    case 3:
                        return "Joystick4_Turn";
                    case 4:
                        return "Joystick5_Turn";
                    case 5:
                        return "Joystick6_Turn";
                    case 6:
                        return "Joystick7_Turn";
                    case 7:
                        return "Joystick8_Turn";
                }
                break;
        }
        return axisName;
    }

    private string ResolveLogitechF310Axis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move";
                    case 1:
                        return "Joystick2_Move";
                    case 2:
                        return "Joystick3_Move";
                    case 3:
                        return "Joystick4_Move";
                    case 4:
                        return "Joystick5_Move";
                    case 5:
                        return "Joystick6_Move";
                    case 6:
                        return "Joystick7_Move";
                    case 7:
                        return "Joystick8_Move";
                }
                break;
            case ActionControl.StraffRight:
            case ActionControl.StraffLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Straff";
                    case 1:
                        return "Joystick2_Straff";
                    case 2:
                        return "Joystick3_Straff";
                    case 3:
                        return "Joystick4_Straff";
                    case 4:
                        return "Joystick5_Straff";
                    case 5:
                        return "Joystick6_Straff";
                    case 6:
                        return "Joystick7_Straff";
                    case 7:
                        return "Joystick8_Straff";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Logitech_F310_1_Turn";
                    case 1:
                        return "Logitech_F310_2_Turn";
                    case 2:
                        return "Logitech_F310_3_Turn";
                    case 3:
                        return "Logitech_F310_4_Turn";
                    case 4:
                        return "Logitech_F310_5_Turn";
                    case 5:
                        return "Logitech_F310_6_Turn";
                    case 6:
                        return "Logitech_F310_7_Turn";
                    case 7:
                        return "Logitech_F310_8_Turn";
                }
                break;
        }
        return axisName;
    }

    private string ResolveLogitechDualActionAxis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move";
                    case 1:
                        return "Joystick2_Move";
                    case 2:
                        return "Joystick3_Move";
                    case 3:
                        return "Joystick4_Move";
                    case 4:
                        return "Joystick5_Move";
                    case 5:
                        return "Joystick6_Move";
                    case 6:
                        return "Joystick7_Move";
                    case 7:
                        return "Joystick8_Move";
                }
                break;
            case ActionControl.StraffRight:
            case ActionControl.StraffLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Straff";
                    case 1:
                        return "Joystick2_Straff";
                    case 2:
                        return "Joystick3_Straff";
                    case 3:
                        return "Joystick4_Straff";
                    case 4:
                        return "Joystick5_Straff";
                    case 5:
                        return "Joystick6_Straff";
                    case 6:
                        return "Joystick7_Straff";
                    case 7:
                        return "Joystick8_Straff";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Logitech_DualAction_1_Turn";
                    case 1:
                        return "Logitech_DualAction_2_Turn";
                    case 2:
                        return "Logitech_DualAction_3_Turn";
                    case 3:
                        return "Logitech_DualAction_4_Turn";
                    case 4:
                        return "Logitech_DualAction_5_Turn";
                    case 5:
                        return "Logitech_DualAction_6_Turn";
                    case 6:
                        return "Logitech_DualAction_7_Turn";
                    case 7:
                        return "Logitech_DualAction_8_Turn";
                }
                break;
        }
        return axisName;
    }

    private string ResolvePS3Axis(int playerId, ActionControl action)
    {
        String axisName = null;
        switch (action)
        {
            case ActionControl.MoveFwd:
            case ActionControl.MoveBck:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Move";
                    case 1:
                        return "Joystick2_Move";
                    case 2:
                        return "Joystick3_Move";
                    case 3:
                        return "Joystick4_Move";
                    case 4:
                        return "Joystick5_Move";
                    case 5:
                        return "Joystick6_Move";
                    case 6:
                        return "Joystick7_Move";
                    case 7:
                        return "Joystick8_Move";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return "Joystick1_Turn";
                    case 1:
                        return "Joystick2_Turn";
                    case 2:
                        return "Joystick3_Turn";
                    case 3:
                        return "Joystick4_Turn";
                    case 4:
                        return "Joystick5_Turn";
                    case 5:
                        return "Joystick6_Turn";
                    case 6:
                        return "Joystick7_Turn";
                    case 7:
                        return "Joystick8_Turn";
                }
                break;
        }
        return axisName;
    }

    private KeyCode ResolveKeyCode(int playerId, KeyMapping keyMap, ActionControl action)
    {
        KeyCode keyCode = KeyCode.None;
        switch (keyMap)
        {
            case KeyMapping.KeyBoard:
                keyCode = ResolveKeyBoardKeyCode(action);
                break;
            case KeyMapping.LogitechDualAction:
                keyCode = ResolveLogitechDualActionKeyCode(playerId, action);
                break;
            case KeyMapping.LogitechF310:
                keyCode = ResolveLogitechF310KeyCode(playerId, action);
                break;
            case KeyMapping.XBox360:
                keyCode = ResolveXBox360KeyCode(playerId, action);
                break;
            case KeyMapping.PS3:
                keyCode = ResolvePS3KeyCode(playerId, action);
                break;
        }
        return keyCode;
    }

    private KeyCode ResolveXBox360KeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    case 4:
                        return KeyCode.Joystick5Button5;
                    case 5:
                        return KeyCode.Joystick6Button5;
                    case 6:
                        return KeyCode.Joystick7Button5;
                    case 7:
                        return KeyCode.Joystick8Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button2;
                    case 1:
                        return KeyCode.Joystick2Button2;
                    case 2:
                        return KeyCode.Joystick3Button2;
                    case 3:
                        return KeyCode.Joystick4Button2;
                    case 4:
                        return KeyCode.Joystick5Button2;
                    case 5:
                        return KeyCode.Joystick6Button2;
                    case 6:
                        return KeyCode.Joystick7Button2;
                    case 7:
                        return KeyCode.Joystick8Button2;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Heal:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button1;
                    case 1:
                        return KeyCode.Joystick2Button1;
                    case 2:
                        return KeyCode.Joystick3Button1;
                    case 3:
                        return KeyCode.Joystick4Button1;
                    case 4:
                        return KeyCode.Joystick5Button1;
                    case 5:
                        return KeyCode.Joystick6Button1;
                    case 6:
                        return KeyCode.Joystick7Button1;
                    case 7:
                        return KeyCode.Joystick8Button1;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.ToggleLight:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button3;
                    case 1:
                        return KeyCode.Joystick2Button3;
                    case 2:
                        return KeyCode.Joystick3Button3;
                    case 3:
                        return KeyCode.Joystick4Button3;
                    case 4:
                        return KeyCode.Joystick5Button3;
                    case 5:
                        return KeyCode.Joystick6Button3;
                    case 6:
                        return KeyCode.Joystick7Button3;
                    case 7:
                        return KeyCode.Joystick8Button3;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Run:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    case 4:
                        return KeyCode.Joystick5Button4;
                    case 5:
                        return KeyCode.Joystick6Button4;
                    case 6:
                        return KeyCode.Joystick7Button4;
                    case 7:
                        return KeyCode.Joystick8Button4;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolveLogitechF310KeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    case 4:
                        return KeyCode.Joystick5Button5;
                    case 5:
                        return KeyCode.Joystick6Button5;
                    case 6:
                        return KeyCode.Joystick7Button5;
                    case 7:
                        return KeyCode.Joystick8Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button2;
                    case 1:
                        return KeyCode.Joystick2Button2;
                    case 2:
                        return KeyCode.Joystick3Button2;
                    case 3:
                        return KeyCode.Joystick4Button2;
                    case 4:
                        return KeyCode.Joystick5Button2;
                    case 5:
                        return KeyCode.Joystick6Button2;
                    case 6:
                        return KeyCode.Joystick7Button2;
                    case 7:
                        return KeyCode.Joystick8Button2;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Heal:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button1;
                    case 1:
                        return KeyCode.Joystick2Button1;
                    case 2:
                        return KeyCode.Joystick3Button1;
                    case 3:
                        return KeyCode.Joystick4Button1;
                    case 4:
                        return KeyCode.Joystick5Button1;
                    case 5:
                        return KeyCode.Joystick6Button1;
                    case 6:
                        return KeyCode.Joystick7Button1;
                    case 7:
                        return KeyCode.Joystick8Button1;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.ToggleLight:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button3;
                    case 1:
                        return KeyCode.Joystick2Button3;
                    case 2:
                        return KeyCode.Joystick3Button3;
                    case 3:
                        return KeyCode.Joystick4Button3;
                    case 4:
                        return KeyCode.Joystick5Button3;
                    case 5:
                        return KeyCode.Joystick6Button3;
                    case 6:
                        return KeyCode.Joystick7Button3;
                    case 7:
                        return KeyCode.Joystick8Button3;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Run:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    case 4:
                        return KeyCode.Joystick5Button4;
                    case 5:
                        return KeyCode.Joystick6Button4;
                    case 6:
                        return KeyCode.Joystick7Button4;
                    case 7:
                        return KeyCode.Joystick8Button4;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolveLogitechDualActionKeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    case 4:
                        return KeyCode.Joystick5Button5;
                    case 5:
                        return KeyCode.Joystick6Button5;
                    case 6:
                        return KeyCode.Joystick7Button5;
                    case 7:
                        return KeyCode.Joystick8Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button0;
                    case 1:
                        return KeyCode.Joystick2Button0;
                    case 2:
                        return KeyCode.Joystick3Button0;
                    case 3:
                        return KeyCode.Joystick4Button0;
                    case 4:
                        return KeyCode.Joystick5Button0;
                    case 5:
                        return KeyCode.Joystick6Button0;
                    case 6:
                        return KeyCode.Joystick7Button0;
                    case 7:
                        return KeyCode.Joystick8Button0;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Heal:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button2;
                    case 1:
                        return KeyCode.Joystick2Button2;
                    case 2:
                        return KeyCode.Joystick3Button2;
                    case 3:
                        return KeyCode.Joystick4Button2;
                    case 4:
                        return KeyCode.Joystick5Button2;
                    case 5:
                        return KeyCode.Joystick6Button2;
                    case 6:
                        return KeyCode.Joystick7Button2;
                    case 7:
                        return KeyCode.Joystick8Button2;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.ToggleLight:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button3;
                    case 1:
                        return KeyCode.Joystick2Button3;
                    case 2:
                        return KeyCode.Joystick3Button3;
                    case 3:
                        return KeyCode.Joystick4Button3;
                    case 4:
                        return KeyCode.Joystick5Button3;
                    case 5:
                        return KeyCode.Joystick6Button3;
                    case 6:
                        return KeyCode.Joystick7Button3;
                    case 7:
                        return KeyCode.Joystick8Button3;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Run:
                switch (JoysticResTable[playerId])
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    case 4:
                        return KeyCode.Joystick5Button4;
                    case 5:
                        return KeyCode.Joystick6Button4;
                    case 6:
                        return KeyCode.Joystick7Button4;
                    case 7:
                        return KeyCode.Joystick8Button4;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolvePS3KeyCode(int playerId, ActionControl action)
    {
        switch (action)
        {
            case ActionControl.Fire:
                switch (playerId)
                {
                    case 0:
                        return KeyCode.Joystick1Button5;
                    case 1:
                        return KeyCode.Joystick2Button5;
                    case 2:
                        return KeyCode.Joystick3Button5;
                    case 3:
                        return KeyCode.Joystick4Button5;
                    default:
                        return KeyCode.None;
                }
            case ActionControl.Reload:
                switch (playerId)
                {
                    case 0:
                        return KeyCode.Joystick1Button4;
                    case 1:
                        return KeyCode.Joystick2Button4;
                    case 2:
                        return KeyCode.Joystick3Button4;
                    case 3:
                        return KeyCode.Joystick4Button4;
                    default:
                        return KeyCode.None;
                }
        }
        return KeyCode.None;
    }

    private KeyCode ResolveKeyBoardKeyCode(ActionControl action)
    {
        switch (action)
        {
            case ActionControl.MoveFwd:
                return KeyCode.Z;
            case ActionControl.MoveBck:
                return KeyCode.S;
            case ActionControl.TurnRight:
                return KeyCode.RightArrow;
            case ActionControl.TurnLeft:
                return KeyCode.LeftArrow;
            case ActionControl.StraffRight:
                return KeyCode.D;
            case ActionControl.StraffLeft:
                return KeyCode.Q;
            case ActionControl.Fire:
                return KeyCode.Space;
            case ActionControl.Reload:
                return KeyCode.R;
            case ActionControl.Heal:
                return KeyCode.E;
            case ActionControl.ToggleLight:
                return KeyCode.A;
            case ActionControl.Run:
                return KeyCode.LeftShift;
        }
        return KeyCode.None;
    }
}
