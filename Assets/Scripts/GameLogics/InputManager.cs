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
    }

    public enum ActionControl
    {
        MoveFwd,
        MoveBck,
        TurnRight,
        TurnLeft,
        Fire,
        Reload
    }

    public enum KeyMapping
    {
		Disabled,
        KeyBoard,
        LogitechDualAction,
        LogitechF310,
        XBox360,
    }

    // Use this for initialization
    void Start () {
        foreach (String joystickName in Input.GetJoystickNames())
        {
            Debug.Log(joystickName);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
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
                switch (playerId)
                {
                    case 0:
                        return "Player1_Move";
                    case 1:
                        return "Player2_Move";
                    case 2:
                        return "Player3_Move";
                    case 3:
                        return "Player4_Move";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (playerId)
                {
                    case 0:
                        return "Player1_Turn";
                    case 1:
                        return "Player2_Turn";
                    case 2:
                        return "Player3_Turn";
                    case 3:
                        return "Player4_Turn";
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
                switch (playerId)
                {
                    case 0:
                        return "Player1_Move";
                    case 1:
                        return "Player2_Move";
                    case 2:
                        return "Player3_Move";
                    case 3:
                        return "Player4_Move";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (playerId)
                {
                    case 0:
                        return "Player1_Turn";
                    case 1:
                        return "Player2_Turn";
                    case 2:
                        return "Player3_Turn";
                    case 3:
                        return "Player4_Turn";
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
                switch (playerId)
                {
                    case 0:
                        return "Player1_Move";
                    case 1:
                        return "Player2_Move";
                    case 2:
                        return "Player3_Move";
                    case 3:
                        return "Player4_Move";
                }
                break;
            case ActionControl.TurnRight:
            case ActionControl.TurnLeft:
                switch (playerId)
                {
                    case 0:
                        return "Player1_Turn";
                    case 1:
                        return "Player2_Turn";
                    case 2:
                        return "Player3_Turn";
                    case 3:
                        return "Player4_Turn";
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
        }
        return keyCode;
    }

    private KeyCode ResolveXBox360KeyCode(int playerId, ActionControl action)
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

    private KeyCode ResolveLogitechF310KeyCode(int playerId, ActionControl action)
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

    private KeyCode ResolveLogitechDualActionKeyCode(int playerId, ActionControl action)
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
                return KeyCode.D;
            case ActionControl.TurnLeft:
                return KeyCode.Q;
            case ActionControl.Fire:
                return KeyCode.Space;
            case ActionControl.Reload:
                return KeyCode.R;
        }
        return KeyCode.None;
    }
}
