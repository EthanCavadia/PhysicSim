using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    int i = 0;
    public Vector3 offset = new Vector3(0, 0, -50);
    public float smoothTime = 0.25f;
    public float distance = 10.0f; // Distance from the target
    public float height = 5.0f; // Height from the target
    public float damping = 2.0f; // Damping for smoothness
    public float rotationForce = 5.0f;
    bool effect = false;
    GameObject _gameObject;
    WorldManager _worldManager;
    List<GameObject> _list = new List<GameObject>();
    Vector3 currentVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameObject = GameObject.Find("Manager");
        _worldManager = _gameObject.GetComponent<WorldManager>();
        _list = _worldManager.starObjs;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            i += 1;
        }
        if (i == _list.Count) { i = 0; }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = _list[i].transform.position - _list[i].transform.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

        Quaternion targetRotation = Quaternion.Euler(0, _list[i].transform.eulerAngles.y * Time.deltaTime, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationForce);

        transform.position = Vector3.SmoothDamp(
        transform.position,
        _list[i].transform.position + offset,
        ref currentVelocity,
        smoothTime);

        transform.LookAt(_list[i].transform.position);
    }
}
