using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public float damping;
    public float rotationForce;
    public float sensitivity = 10f;
    float dist;
    int currStar;
    Vector3 offset = new Vector3();
   
    bool locked = true;
    public float speed = 10f;
    GameObject _gameObject;
    WorldManager _worldManager;
    List<GameObject> _list = new List<GameObject>();

    public TMP_Text _starName;

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
        ChangeTarget();
        Zoom();
        LockOnStar();
        MoveCam();
        _starName.text = _list[currStar].GetComponent<Star>()._name;
    }

    private void LateUpdate()
    {
        TransformByMagnitude();
    }

    void LockOnStar()
    {
        if (Input.GetKeyDown(KeyCode.F) && locked == true)
        {
            locked = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && locked == false)
        {
            locked = true;
        }
    }

    void ChangeTarget()
    {
        if (Input.GetKeyDown(KeyCode.Q) && locked == true)
        {
            currStar += 1;
        }
        if (Input.GetKeyDown(KeyCode.Q) && locked == true && currStar == _list.Count)
        {
            currStar += 2;
        }
        if (Input.GetKeyDown(KeyCode.E) && locked == true)
        {
            currStar -= 1;
        }
        if (currStar > _list.Count && locked == true)
        { 
            currStar = 0;
        }
        else if (currStar < 0 && locked == true)
        {
            currStar = _list.Count - 1;
        }
    }

    void Zoom()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            dist += 4f;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            dist -= 4f;
        }
    }
    void MoveCam()
    {
        float yaw = 0f;
        float pitch = 0f;

        if (Input.GetKey(KeyCode.W) && locked == false)
        {
            transform.position += speed * Vector3.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && locked == false)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && locked == false)
        {
            transform.position += speed * Vector3.back * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && locked == false)
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }
        if (Input.GetMouseButton(1) && locked == false)
        {
            yaw += Input.mousePosition.x;
            pitch -= Input.mousePosition.y;
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
    void TransformByMagnitude()
    {
        if (locked == true)
        {
            offset = new Vector3(0f, 0f, dist);
            offset.y = _list[currStar].GetComponent<Star>()._velocity.magnitude;
            Vector3 desiredPosition = _list[currStar].transform.position - _list[currStar].transform.forward * _list[currStar].GetComponent<Star>()._velocity.magnitude + Vector3.up * _list[currStar].GetComponent<Star>()._velocity.magnitude;
            transform.position = Vector3.Lerp(transform.position, desiredPosition + offset, Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(_list[currStar].transform.eulerAngles.x, _list[currStar].transform.eulerAngles.y, _list[currStar].transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationForce);

            transform.LookAt(_list[currStar].transform.position);
        }
    }
}
