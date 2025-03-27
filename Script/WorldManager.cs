using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public List<GameObject> starObjs = new List<GameObject>();
    public GameObject starPrefab;
    public const float G = 66743f; //0.66743f;  // 0.000000000066743f
    public float timeMult = 1.0f;
    bool pause = false;
    List<Star> stars;
    Color32 orange = new Color32(20, 255, 255, 255);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars = new List<Star>
        {
            new Star(starPrefab,"Soleil",      1.00f, new Vector3(0, 0, 0), new Vector3(0, 0, 0), Color.yellow),
            new Star(starPrefab,"Scurve 2398", 0.26f, new Vector3(68, -365, 631), new Vector3(-5.69f, 4.76f, 3.35f), Color.red),
            new Star(starPrefab,"Ross 248",    0.17f, new Vector3(464, -42, 450), new Vector3(-8.75f, 1.13f, -15.45f), Color.red),
            new Star(starPrefab,"61 Cygni", 0.69f, new Vector3(394, -377, 433), new Vector3(-2.78f, 22.03f, 0.002f),orange),
            new Star(starPrefab,"Lalande 21185", 0.39f, new Vector3(-404, 107, 307), new Vector3(7.32f, -0.47f, -20.11f), Color.red),
            new Star(starPrefab,"Procyon 5", 1.29f, new Vector3(-295, 658, 68), new Vector3(2.38f, 0.75f, -3.65f),Color.blue),
            new Star(starPrefab,"Barnard", 0.21f, new Vector3(-7, -371, 30), new Vector3(-0.87f, 24.2f, 16.78f),Color.red),
            new Star(starPrefab,"Epsilon eridami", 0.74f, new Vector3(408, 534, -114), new Vector3(4.6f, 0.69f, -0.5f), orange),
            new Star(starPrefab,"Wolf 259", 0.1f, new Vector3(-462, 136, 62), new Vector3(-0.82f, 9.86f, -5.94f), Color.red),
            new Star(starPrefab,"Sirius", 2.96f, new Vector3(-98, 514, -157), new Vector3(1.89f, -2.21f, -2.59f), Color.blue),
            new Star(starPrefab,"Luyten 726-8", 0.19f, new Vector3(487, 219, -175), new Vector3(2.08f, 10.8f, -0.41f), Color.red),
            new Star(starPrefab,"Ross 128", 0.21f, new Vector3(-683, 44, 13), new Vector3(2.51f, -2.32f, -4.09f), Color.red),
            new Star(starPrefab,"Tau ceti", 0.85f, new Vector3(646, 307, -208), new Vector3(0.52f, -6.62f, 3.92f), Color.yellow),
            new Star(starPrefab,"Alpha du centaure", 1.03f, new Vector3(-106, -86, -243), new Vector3(-1.95f, 4.68f, 4.51f), Color.yellow),
            new Star(starPrefab,"Luyten 789-6", 0.13f, new Vector3(608, -235, -182), new Vector3(-6.75f, 10.81f, 10.56f),Color.yellow),
            new Star(starPrefab,"Luyten 725-32", 0.21f, new Vector3(718, 227, -233), new Vector3(4.7f, 6.16f, 0.51f),Color.red),
            new Star(starPrefab,"Ross 154", 0.24f, new Vector3(111, -536, -241), new Vector3(1.79f, 1.36f, -0.11f), Color.red),
            new Star(starPrefab,"Epsilon indi", 0.69f, new Vector3(334, -194, -594), new Vector3(-3.54f, 17.71f, 2.28f), orange)
        };
        
        foreach (Star _star in stars)
        {
            GameObject starObj_ = Instantiate(starPrefab);
            starPrefab.GetComponent<Star>()._velocity = _star._velocity;
            starPrefab.GetComponent<Star>()._name = _star._name;
            starPrefab.GetComponent<Star>()._pos= _star._pos;
            starPrefab.GetComponent<Star>()._mass = _star._mass;
            starPrefab.GetComponent<Star>()._color = _star._color;

            starPrefab.name = _star._name;
            starPrefab.transform.position = _star._pos;
            starObjs.Add(starObj_);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (pause == false)
        {
            Time.timeScale = 0;
            pause = true;
        }
        else if (pause == true)
        {
            Time.timeScale = 1;
            pause = false;
        }
    }
}
