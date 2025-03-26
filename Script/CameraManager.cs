using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    int i = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gameObject = GameObject.Find("Manager");
        WorldManager worldManager = gameObject.GetComponent<WorldManager>();
        List<GameObject> list = worldManager.starObjs;

        Camera camera = GetComponent<Camera>();
        
        if(Input.GetKeyDown(KeyCode.D))
        {
            camera.transform.SetParent(list[i].transform, false);
            i += 1;
            
        }
        if (i == list.Count) { i = 0; }
    }
}
