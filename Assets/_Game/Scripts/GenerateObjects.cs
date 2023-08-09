using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class GenerateObjects : MonoBehaviour
{
    public List<GameObject> platforms;
    private GameObject currentPlatform;
    public GameObject bg;
    public List<GameObject> bgs;
    public List<GameObject> sugars;
    public List<GameObject> powerUps;
    public List<GameObject> gifs;
 
    public float randomMinX;
    public float randomMaxX;
    public float randomMinY;
    public float randomMaxY;
    private int platformLevel;
    private float spawnHeight;
    public int spawnCount;
    public int bgsCurrentCount;
    public List<GameObject> currentCoins;
    public int platformCount;
    public int sugarCount;
    public int gifCount;
    void Start()
    {
        currentPlatform = platforms[platformCount];
        GeneratePlatform(platforms[platformCount]);
        bgsCurrentCount = 0;
        gifCount = 0;
        platformCount = 0;
        sugarCount = 0;
      
    }

    
    public void GeneratePlatform(GameObject Platform)
    {

        for (int i = 0; i < 11; i++)
        {
            float randomPosX = Random.Range(randomMinX, randomMaxX);
            float randomSugarPosX = Random.Range(randomMinX, randomMaxX);
            float randomPosY = Random.Range(randomMinY, randomMaxY);
            Platform = Instantiate(platforms[platformCount], new Vector3(randomPosX, currentPlatform.transform.position.y + randomPosY, platforms[platformCount].transform.position.z), transform.rotation);
            GameObject Sugar = Instantiate(sugars[sugarCount], new Vector3(randomSugarPosX, Platform.transform.position.y + 5f, Platform.transform.position.z), transform.rotation);
            currentCoins.Add(Sugar);
        
            currentPlatform = Platform;

        }
           
    }
    public void GenerateBg()
    {
        if (spawnCount >= 2)
        {
            if (bgsCurrentCount == 1|| bgsCurrentCount == 3 || bgsCurrentCount == 5 || bgsCurrentCount == 7)
            {
                GameObject BGs = Instantiate(bgs[bgsCurrentCount], new Vector3(bg.transform.position.x, bg.transform.position.y + spawnHeight, bg.transform.position.z), Quaternion.identity);
                bgsCurrentCount++;
            }
            else
            {
                GameObject BGs = Instantiate(bgs[bgsCurrentCount], new Vector3(bg.transform.position.x, bg.transform.position.y + spawnHeight, bg.transform.position.z), Quaternion.identity);
            }
        }
            
        else
        {
            GameObject BG = Instantiate(bg, new Vector3(bg.transform.position.x, bg.transform.position.y + spawnHeight, bg.transform.position.z), Quaternion.identity);
        }
        spawnCount++; 
        spawnHeight += 59.38f;
        
    }
    public void GeneratePowerUp()
    {
        int randomObject=Random.Range(0,2);
        float randomPosX = Random.Range(randomMinX, randomMaxX);
    
        switch (randomObject)
        {
            case 0:
                Instantiate(powerUps[0], new Vector3(randomPosX, GameManager.instance.player.transform.position.y + 20f, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(powerUps[1], new Vector3(randomPosX, GameManager.instance.player.transform.position.y + 20f, 0), Quaternion.identity);
                break;
        }
      
    }
    public void GenerateGif()

    {
        float randomPosX = Random.Range(randomMinX, randomMaxX);
        GameObject Gif = Instantiate(gifs[gifCount], new Vector3(randomPosX, GameManager.instance.player.transform.position.y + 20f, 0), Quaternion.identity);

    }


}

