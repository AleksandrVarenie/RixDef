using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public List<Sprite> enemySprites;

    void Start()
    {
        // ������� ��������� ������ � ������
        int randomIndex = Random.Range(0, enemySprites.Count);
        Sprite randomSprite = enemySprites[randomIndex];

        // ����������� ������ �� ������
        GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
