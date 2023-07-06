using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class GenerateObjects : MonoBehaviour
{
    public GameObject platform;
    private GameObject currentPlatform;
    public GameObject spawnBorder;
    //public GameObject deleteBorder;
    public GameObject coin;
    //VALUES
    public float randomMinX;
    public float randomMaxX;
    public float randomMinY;
    public float randomMaxY;
    private int platformLevel;
    

    //ARRAYS

    public List<GameObject> currentPlatforms;
    public List<GameObject> currentCoins;

    void Start()
    {
        currentPlatform = platform;
        GeneratePlatform(platform);     
    }

    
    void Update()
    {
        

    }
    public void GeneratePlatform(GameObject Platform)
    {

        for (int i = 0; i < 20; i++)
        {
            float randomPosX = Random.Range(randomMinX, randomMaxX);
            float randomPosY = Random.Range(randomMinY, randomMaxY);
            Platform = Instantiate(platform, new Vector3(randomPosX, currentPlatform.transform.position.y + randomPosY, platform.transform.position.z), transform.rotation);

            int randomCount = Random.Range(0, 2);
            if(randomCount == 0) 
            {
                GameObject Coin = Instantiate(coin, new Vector3(Platform.transform.position.x, Platform.transform.position.y + 1f, Platform.transform.position.z), transform.rotation);
                currentCoins.Add(Coin);
                Coin.GetComponent<MoveCoin>().speed += 0.2f;
            }
          
          
            Platform.GetComponent<PlatformMove>().speed += 0.2f;
            currentPlatforms.Add(Platform);
            currentPlatform = Platform;

        }
        Instantiate(spawnBorder, new Vector3(0, currentPlatforms[16].transform.position.y, currentPlatforms[16].transform.position.z), transform.rotation);
        //Instantiate(deleteBorder, new Vector3(0, currentPlatforms[19].transform.position.y+2f, currentPlatforms[19].transform.position.z), transform.rotation);

    }
    public void GenerateCoin()
    {
        for (int i = 0; i < 20; i++)
        {
            
       

        }
    }
    private void OnBecameInvisible()
    {
        {
            Destroy(platform);
        }
    }

}

