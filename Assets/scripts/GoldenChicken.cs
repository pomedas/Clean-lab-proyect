using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenChicken : MonoBehaviour
{
    public float speed_chicken;

    private float Radius = 10.1f;

    private Vector3 _centre;
    private float _angle;

    private void Start()
    {
        _centre = transform.position;
    }

    private void Update()
    {
        if ((transform.position.x > 25) && (transform.position.x < 75))
        {
            _angle += speed_chicken * Time.deltaTime;

            var offset = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)) * Radius;
            transform.position = _centre + offset;
        }
    }
}
