using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public List<Sprite> enemySprites;

    void Start()
    {
        // Вибрати рандомний спрайт зі списку
        int randomIndex = Random.Range(0, enemySprites.Count);
        Sprite randomSprite = enemySprites[randomIndex];

        // Застосувати спрайт до ворога
        GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
