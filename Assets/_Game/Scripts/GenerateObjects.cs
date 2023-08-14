using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class GenerateObjects : MonoBehaviour
{
  
    private GameObject currentPlatform;
    public GameObject bg;
    public List<GameObject> bgs;
    public List<GameObject> powerUps;
 
 
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
        
      
        bgsCurrentCount = 0;
        spawnHeight = 59.38f;
     
    }

    
    public void GenerateBg()
    {
        int randomChild = Random.Range(0, 3);
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
                BGs.transform.GetChild(0).gameObject.SetActive(false);
                BGs.transform.GetChild(randomChild).gameObject.SetActive(true);
             
            }
        }
            
        else
        {
            GameObject BG = Instantiate(bg, new Vector3(bg.transform.position.x, bg.transform.position.y + spawnHeight, bg.transform.position.z), Quaternion.identity);
            BG.transform.GetChild(0).gameObject.SetActive(false);
            BG.transform.GetChild(randomChild).gameObject.SetActive(true);
          
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
   

}

