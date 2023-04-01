using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DragonController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;
    public float fireballCooldown = 0.5f;

    public float speedLight = 2f;

    public GameObject LightPrefab;


    private bool canShoot = true;

    private Transform projectail;

    private Light2D light;

    void Update()
    {
        if (canShoot && Input.GetMouseButton(0))
        {
            ShootFireball();
            StartCoroutine(StartFireballCooldown());
        }
        if (projectail!= null)
        {
            light.pointLightOuterRadius = Vector2.Distance(transform.position, projectail.position) * 2;
        } else
        {
            if (light != null)
            {
                if (light.pointLightOuterRadius > 0)
                {
                    light.pointLightOuterRadius -= Time.deltaTime * speedLight;
                }
                else
                {
                    light.pointLightOuterRadius = 0;
                }
            }
            
        }
    }

    void ShootFireball()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject fireball = Instantiate(fireballPrefab, transform.position, rotation);
        fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
        fireball.GetComponent<FireBall>().owner = gameObject;
        projectail = fireball.transform;
        GameObject lightHolder = Instantiate(LightPrefab, transform.position, rotation);
        light = lightHolder.transform.GetChild(0).GetComponent<Light2D>();
        //   Destroy(fireball, fireballDuration);
    }
    IEnumerator StartFireballCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireballCooldown);
        canShoot = true;
    }
}
