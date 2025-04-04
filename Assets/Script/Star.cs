using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class Star : MonoBehaviour
{
    public GameObject starObj;
    public Rigidbody _starRb;
    public Vector3 _pos;
    public Vector3 _velocity;
    public Color _color;

    public string _name;
    public float _mass;

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
        starObj.layer = 6;
        starObj.SetActive(true);
        var starRendrer = starObj.GetComponent<Renderer>();
        foreach (Material r in starRendrer.materials) 
        {
            r.SetColor("_BaseColor", _color);
            r.SetColor("_EmissionColor", _color);
        }

        TrailRenderer trail = GetComponent<TrailRenderer>();
        
        trail.material.SetColor("_BaseColor", _color);
        trail.material.SetColor("_EmissionColor", _color);

        _starRb = starObj.GetComponent<Rigidbody>();
        _starRb.mass = _mass;
        _starRb.linearVelocity = _velocity;
    }

    private void FixedUpdate()
    {
        GameObject gameObject = GameObject.Find("Manager");
        WorldManager worldManager = gameObject.GetComponent<WorldManager>();
        starObj.transform.position += Time.deltaTime * _starRb.linearVelocity;
        
        foreach(GameObject currStar in worldManager.starObjs)
        {
            if (currStar.name != starObj.name)
            {
                _starRb.AddForce(Gravity(currStar, starObj));
            }
        }
    }

    Vector3 Gravity(GameObject currStar_, GameObject star_)
    {
        var currStar = currStar_.GetComponent<Star>();
        var star = star_.GetComponent<Star>();
        var starMass_ = star._mass;
        var currMass_ = currStar._mass;

        float dist = Vector3.Distance(currStar.transform.position, star.transform.position);
        //Debug.Log(currStar_.name + " est  " + dist + " de " + star_.name);

        float forceMagnitude = WorldManager.G * currMass_ * starMass_ / Mathf.Pow(dist, 2);
        //Debug.Log(star_.name + " " + forceMagnitude);

        Vector3 force = (currStar.transform.position - star.transform.position).normalized * forceMagnitude;
        //Debug.Log(star_.name + " " + force);
        return force;
    }
}
