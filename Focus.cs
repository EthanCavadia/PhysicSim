using UnityEngine;

public class Focus : MonoBehaviour
{
    Star star;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        star = GetComponentInParent<Star>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = star._velocity.normalized * Time.deltaTime;
        transform.position = star._pos + direction;
    }
}
