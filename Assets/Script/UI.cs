using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UI : MonoBehaviour
{
    public Canvas canvas;
    public Button timeMult_one, timeMult_two, timeMult_three, timeMult_four;
    public TMP_InputField nameInput;
    public TMP_InputField massInput;
    public TMP_InputField posx, posy, posz;
    public TMP_InputField velx, vely, velz;
    bool pause = false;
    public GameObject _prefab;
    public Button createStar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _prefab = GetComponent<GameObject>();
        canvas.enabled = false;

        timeMult_one.onClick.AddListener(() => TimeMult(1));
        timeMult_two.onClick.AddListener(() => TimeMult(2));
        timeMult_three.onClick.AddListener(() => TimeMult(4));
        timeMult_four.onClick.AddListener(() => TimeMult(8));
        var _starName = nameInput.text;
        var _starMass = float.Parse(massInput.text);
        var _starPos = new Vector3();
        _starPos.x = int.Parse(posx.text);
        _starPos.y = int.Parse(posy.text);
        _starPos.z = int.Parse(posz.text);
        var _starVelocity = new Vector3();
        _starVelocity.x = int.Parse(velx.text);
        _starVelocity.y = int.Parse(vely.text);
        _starVelocity.z = int.Parse(velz.text);

        var _starColor = Color.yellow;

        createStar.onClick.AddListener(() => CreateStar(_starName, _starMass, _starPos, _starVelocity, _starColor));
        createStar.onClick.AddListener(() => ResetData());
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
            canvas.enabled = true;
            pause = true;
        }
        else if (pause == true)
        {
            Time.timeScale = 1;
            canvas.enabled = false;
            pause = false;
        }
    }
    void TimeMult(int _timeScale)
    {
        Time.timeScale = _timeScale;
    }

    void SubmitData()
    {
        
    }
    void ResetData()
    {
        
    }
    void CreateStar(string name, float mass, Vector3 pos, Vector3 velocity, Color color)
    {
        GameObject gm = GameObject.Find("Manager");
        WorldManager wm = gm.GetComponent<WorldManager>();
        
        
        wm.AddStar(new Star(_prefab, name, mass, pos, velocity, color));

    }
}
