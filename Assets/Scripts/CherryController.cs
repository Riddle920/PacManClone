using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherry;

    private Vector3 startPos;
    private Vector3 endPos;
    private float offset = 20f;
    
    // Start is called 5efore the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCherry", 0, 10);
    }

    private void SpawnCherry()
    {
        RandomPos();
        GameObject spawnedCherry = Instantiate(cherry, startPos, cherry.transform.rotation);
        StartCoroutine(LerpCherry(spawnedCherry));
    }

    private void RandomPos()
    {
        Camera cam = Camera.main;
        // Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 botLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        // Vector3 botRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 centre = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        // Vector3 leftSide = new Vector3(botLeft.x + offset, Random.Range(botLeft.y, topLeft.y));

        float angleRadians = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 unitOffset = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0);

        startPos = centre + unitOffset * (Mathf.Min(topRight.x - botLeft.x, topRight.y - botLeft.y) / 2 + offset);
        endPos = centre - unitOffset * (Mathf.Min(topRight.x - botLeft.x, topRight.y - botLeft.y) / 2 + offset);

        startPos = new Vector3(startPos.x, startPos.y, 0);
        endPos = new Vector3(endPos.x, endPos.y, 0);
        // int variation = Random.Range(0, 4);
        //
        // switch (variation)
        // {
        //     case 0: // Left -> Right
        //         
        //         break;
        //         
        //     case 1: // Right -> Left
        //         break;
        //     
        //     case 2: // Top -> Bot
        //         break;
        //     
        //     case 3: // Bot -> Top
        //         break;
        // }
    }
    
    private IEnumerator LerpCherry(GameObject spawnedCherry)
    {
        float currentTime = 0.0f;
        float duration = 8f;
        while (currentTime < duration)
        {
            spawnedCherry.transform.position = Vector3.Lerp(startPos, endPos, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        spawnedCherry.transform.position = endPos;
        Destroy(spawnedCherry);
    }
}
