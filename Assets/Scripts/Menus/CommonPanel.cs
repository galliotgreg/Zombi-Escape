﻿using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommonPanel : MonoBehaviour
{
    [SerializeField]
    private string mapName = "";
    [SerializeField]
    Dropdown mapSelect;

    public string MapName
    {
        get
        {
            return mapName;
        }

        set
        {
            mapName = value;
        }
    }

    void Start()
    {
        initMapDropDown();
    }

    private void Update()
    {
        mapName = mapSelect.options[mapSelect.value].text;
    }

    private void initMapDropDown()
    {
        //Get Map dropdown
		mapSelect = GameObject.Find("slt_map").GetComponent<Dropdown>();

        //Get Level Scenes In Dev
#if UNITY_EDITOR
        EditorBuildSettingsScene[] allScenes = EditorBuildSettings.scenes;
        List<EditorBuildSettingsScene> levelScenes = new List<EditorBuildSettingsScene>();
        foreach (EditorBuildSettingsScene scene in allScenes)
        {
            String[] tokens = scene.path.Split('/');
            if (tokens[tokens.Length - 2] == "Levels")
            {
                levelScenes.Add(scene);
            }
        }

        //Setup Drop Down Options
        mapSelect.ClearOptions();
        foreach (EditorBuildSettingsScene scene in levelScenes)
        {
            String[] tokens = scene.path.Split('/');
            String sceneName = tokens[tokens.Length - 1];
            tokens = sceneName.Split('.');
            sceneName = tokens[0];

            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            options.Add(new Dropdown.OptionData(sceneName));
            mapSelect.AddOptions(options);
        }
#else
         List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            options.Add(new Dropdown.OptionData("LostInSewer"));
            mapSelect.AddOptions(options);
#endif
    }

    private GameObject retreiveChild(string name)
    {
        Transform childTransform = transform.Find(name);
        return childTransform.gameObject;
    }
}
