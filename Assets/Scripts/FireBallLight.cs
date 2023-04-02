using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBallLight : MonoBehaviour
{

    public Transform projectail;
    public Light2D light;

    public float speedLight;

    public bool fireBallDead = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (projectail != null)
        {
            light.pointLightOuterRadius = Vector2.Distance(transform.position, projectail.position) * 2;
        }
        else 
        {
            if (light.intensity > 0.1f)
            {
                light.intensity -= Time.deltaTime * speedLight;
            }
            else
            {
                light.intensity = 0;
                Destroy(gameObject);
            }
        }
    }
}

