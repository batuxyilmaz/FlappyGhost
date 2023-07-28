using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class GenerateObjects : MonoBehaviour
{
    public GameObject platform;
    private GameObject currentPlatform;
    public GameObject bg;
    public List<GameObject> sugars;
    public List<GameObject> powerUps;
 
    public float randomMinX;
    public float randomMaxX;
    public float randomMinY;
    public float randomMaxY;
    private int platformLevel;
    private float spawnHeight;

    public List<GameObject> currentCoins;

    void Start()
    {
        currentPlatform = platform;
        GeneratePlatform(platform);
      
    }

    
    public void GeneratePlatform(GameObject Platform)
    {

        for (int i = 0; i < 10; i++)
        {
            float randomPosX = Random.Range(randomMinX, randomMaxX);
            float randomPosY = Random.Range(randomMinY, randomMaxY);
            Platform = Instantiate(platform, new Vector3(randomPosX, currentPlatform.transform.position.y + randomPosY, platform.transform.position.z), transform.rotation);

            int randomCount = Random.Range(0, 2);
            if(randomCount == 0) 
            {
               
                GameObject Sugar = Instantiate(sugars[0], new Vector3(Platform.transform.position.x, Platform.transform.position.y + 5f, Platform.transform.position.z), transform.rotation);
                currentCoins.Add(Sugar);
                Sugar.GetComponent<MoveCoin>().speed += 0.2f;
            }
          
          
            Platform.GetComponent<PlatformMove>().speed += 0.2f;
            currentPlatform = Platform;

        }
           
    }
    public void GenerateBg()
    {  
     
        GameObject BG=Instantiate(bg,new Vector3(bg.transform.position.x,bg.transform.position.y+spawnHeight,bg.transform.position.z),Quaternion.identity);
        spawnHeight += 48f;
        
    }
    public void GeneratePowerUp()
    {
        int randomObject=Random.Range(0,2);
        float randomPosX = Random.Range(randomMinX, randomMaxX);
        Debug.Log(randomObject);
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
  
}

