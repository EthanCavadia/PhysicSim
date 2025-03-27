using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject starObj;

    public Vector3 _pos;
    public Vector3 _velocity;
    public Color _color;

    public string _name;
    public float _mass;

    float timeMult;

    public Star(GameObject obj,string name, float mass, Vector3 pos, Vector3 velocity, Color color)
    {
        starObj = obj;
        _name = name;
        _mass = mass;
        _pos = pos;
        _velocity = velocity;
        _color = color;
    }

    private void Start()
    {
        transform.position = _pos;

        starObj.SetActive(true);
        var starRendrer = starObj.GetComponent<Renderer>();
        starRendrer.material.color = _color;

        starObj.GetComponent<Rigidbody>().mass = _mass;
        starObj.GetComponent<Rigidbody>().linearVelocity = _velocity;

    }

    private void FixedUpdate()
    {
        GameObject gameObject = GameObject.Find("Manager");
        WorldManager worldManager = gameObject.GetComponent<WorldManager>();
        timeMult = worldManager.timeMult;
        starObj.transform.position += Time.deltaTime * starObj.GetComponent<Rigidbody>().linearVelocity;
        
        foreach(GameObject currStar in worldManager.starObjs)
        { 
            //_velocity += Gravity(currStar, starObj);
            starObj.GetComponent<Rigidbody>().AddForce(Gravity(currStar, starObj));
        }
        
        Debug.Log(_name);
        Debug.Log(_velocity.magnitude);
    }

    Vector3 Gravity(GameObject currStar_, GameObject star_)
    {
        var currStar = currStar_.GetComponent<Star>();
        var star = star_.GetComponent<Star>();

        float dist = Vector3.Distance(currStar.transform.position, star.transform.position);
        float forceMagnitude = WorldManager.G * currStar._mass * star._mass / Mathf.Pow(dist, 2);
        if (forceMagnitude < float.MaxValue || forceMagnitude > float.MinValue)
        {
            Vector3 force = (currStar.transform.position - star.transform.position).normalized * forceMagnitude;
            return force;
        }
        else
        {
            return Vector3.zero;
        }
    }


}
