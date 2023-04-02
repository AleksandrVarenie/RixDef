using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public List<GameObject> firePrefab;
    public float fireballSpeed = 10f;
    public float fireballCooldown = 0.5f;

    public float speedLight = 2f;

    public GameObject LightPrefab;
    public List<GameObject> FirePower;

    public float powerIncreaseInterval = 0.5f; // інтервал збільшення накопичення
    public int maxPower = 3; // максимальне значення накопичення
    private int power = 0; // поточне значення накопичення
    private float powerTimer = 0f; // таймер для збільшення накопичення

    private bool canShoot = true;

    private GameObject projectail;

    private OrbitController controller;

    void Start ()
    {
        controller = GetComponent<OrbitController>();
    }

    void Update()
    {
        if (canShoot && Input.GetMouseButton(0) && projectail == null && power != 0)
        {
            ShootFireball();
            StartCoroutine(StartFireballCooldown());
        }
        if (power < maxPower && projectail == null)
        {
            powerTimer += Time.deltaTime;
            if (powerTimer >= powerIncreaseInterval)
            {
                power++;
                powerTimer = 0f;
                FirePower[power].SetActive(true);
            }
        }
        Debug.Log(power);

        if (projectail != null)
        {
            controller.enabled = false;
        }
        else
        {
            controller.enabled = true;
        }
    }

    void ShootFireball()
    {
        Vector2 direction = transform.right;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject fireball = Instantiate(fireballPrefab, transform.position, rotation);
        float currentSpeed = fireballSpeed + (power*0.25f); // збільшуємо швидкість в залежності від накопичення сили
        fireball.GetComponent<Rigidbody2D>().velocity = direction * currentSpeed;
        fireball.GetComponent<FireBall>().owner = gameObject;
        GameObject lightHolder = Instantiate(LightPrefab, transform.position, rotation);
        projectail = lightHolder;
        lightHolder.GetComponent<FireBallLight>().projectail = fireball.transform;
        fireball.GetComponent<FireBall>().Light = lightHolder;
        GameObject fireHolder = Instantiate(firePrefab[power], transform.position, rotation);
        fireHolder.GetComponent<Fire>().projectail = fireball.transform;
        power = 0; // скидаємо накопичення сили до нуля
        for (int i = 0; i < 4; i++)
        {
            if (FirePower[i] != null)
            FirePower[i].SetActive(false);
        }
        // Destroy(fireball, fireballDuration);
    }
    IEnumerator StartFireballCooldown()
    {
        canShoot = false;
       // transform.GetComponent<OrbitController>().enabled = false;
        yield return new WaitForSeconds(fireballCooldown);
        canShoot = true;
    }
}
