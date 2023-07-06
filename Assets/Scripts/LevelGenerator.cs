using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] public GameObject platform;
    [SerializeField] public GameObject levelEnd;
    [SerializeField] public GameObject background;
    [SerializeField] public GameObject sideWallLeft, sideWallRight;
    [SerializeField] public GameObject floor;
    [SerializeField] private int maxPlatform;
    [SerializeField] private float yAxisMultiplierMin;
    [SerializeField] private float yAxisMultiplierMax;
    [SerializeField] private int maxJumppadAmount;
    private float lastYAxis;
    [HideInInspector] public GameObject emptyGO;
    public GameObject coinPrefab;
    public static int level = 1;
    public static int difficulty = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        maxPlatform = 18 + (20 * (difficulty-1));
        ResetLevel();
        NewPrefabs();
        StartCoroutine(GenerateBackground());
        StartCoroutine(GeneratePlatform());
        StartCoroutine(GenerateSideWall());
    }

    private void ResetLevel()
    {
        Transform levelTransform = GameObject.Find("Level01").transform;
        for (int i = 1; i < GameObject.Find("Level01").transform.childCount; i++)
        {
            Destroy(levelTransform.GetChild(i).gameObject);
        }
        for (int i = 0; i < levelTransform.GetChild(0).childCount; i++)//platforms
        {
            Destroy(levelTransform.GetChild(0).GetChild(i).gameObject);
        }
        if(GameObject.FindWithTag("Floor") != null)
            Destroy(GameObject.FindWithTag("Floor"));
    }

    private IEnumerator GeneratePlatform()
    {
        float xAxis = 0;
        int jumpPad = 0;
        lastYAxis = 0;
        for (int i = 0; i <= maxPlatform; i++)
        {
            //Check Level And Get That Level Platform
            if(i == 0 )
                continue;
            if (i == 1)
                xAxis = Random.Range(-1.7f, 1);
            else
                xAxis = Random.Range(-1.7f, 1.4f);
            
            GameObject GO = Instantiate(platform, GameObject.Find("Platforms").transform);
            GO.transform.localPosition = new Vector3(xAxis, i == 0 ? 0 : lastYAxis + Random.Range(yAxisMultiplierMin, yAxisMultiplierMax));
            lastYAxis = GO.transform.localPosition.y;

            GameObject coin = Instantiate(coinPrefab, new Vector3(GO.transform.position.x, GO.transform.position.y + 0.5f),
                Quaternion.identity);

            if (Random.Range(0, 2) == 1)
            {
                if (jumpPad < maxJumppadAmount)
                {
                    GO.transform.GetChild(GO.transform.childCount-1).gameObject.SetActive(true);
                    jumpPad++;
                }
            }
            
            if (i == 0)
            {
                GameObject FloorGO = Instantiate(floor);
                FloorGO.transform.localPosition = new Vector3(0, -6,2.25f);
                FloorGO.tag = "Floor";
            }
            if (i == maxPlatform)
            {
                emptyGO = Instantiate(new GameObject("Level End Platform"), GameObject.Find("Platforms").transform);
                emptyGO.transform.localPosition = new Vector3(-0.125f, Random.Range(yAxisMultiplierMin, yAxisMultiplierMax) * (i + 1));
                emptyGO.AddComponent<BoxCollider2D>().size = new Vector2(1,0.5f);
                emptyGO.layer = GameObject.Find("Platforms").transform.GetChild(0).gameObject.layer;
                emptyGO.tag = GameObject.Find("Platforms").transform.GetChild(0).gameObject.tag;
            }
                
        }
        
        yield break;
    }

    private IEnumerator GenerateBackground()
    {
        int nextBGYAxis = Mathf.CeilToInt((maxPlatform * Random.Range(yAxisMultiplierMin, yAxisMultiplierMax)) / 10);
        for (int i = 1; i <= nextBGYAxis; i++)
        {
            //Check Level And Get That Level Background
            GameObject GO = Instantiate(background, GameObject.Find("Level01").transform);
            GO.transform.SetLocalPositionAndRotation(new Vector3(0, (i-1) * 10,2.29f), new Quaternion(0,0,i%2 == 0 ? 0 : 180,0));

            #region Canvas
            GameObject _canvas = new GameObject("Canvas");
            _canvas.transform.SetParent(GO.transform);
            _canvas.AddComponent<Canvas>();
            RectTransform wallRectTransform = _canvas.GetComponent<RectTransform>();
            wallRectTransform.anchoredPosition = new Vector3(0, 0,-4.5f);
            wallRectTransform.sizeDelta = new Vector2(100, 100);
            wallRectTransform.localRotation = new Quaternion(0, 0, 180, 0);
            wallRectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            #endregion

            #region HeightText
            GameObject _text = new GameObject("Height");
            _text.transform.SetParent(_canvas.transform);
            _text.AddComponent<CanvasRenderer>();
            TMP_Text _tmpText = _text.AddComponent<TextMeshProUGUI>();
            _tmpText.enableAutoSizing = true;
            _tmpText.fontSizeMax = 5;
            _tmpText.fontSizeMin = 1;
            _tmpText.alignment = TextAlignmentOptions.Midline;
            _tmpText.enableWordWrapping = false;
            RectTransform _textRectTransform = _text.GetComponent<RectTransform>();
            if(i % 2 == 0)
                _text.transform.localPosition = new Vector3(-17, -50, -110);
            else
                _text.transform.localPosition = new Vector3(17, 50, -110);
            _textRectTransform.sizeDelta = new Vector2(10, 5);
            _textRectTransform.localScale = new Vector3(1, 1,1);
            _tmpText.text = ((i - 1) * 10) + 10 + " m";
            #endregion
            
            if (i == nextBGYAxis-1)
            {
                GameObject gameObj = Instantiate(levelEnd, GameObject.Find("Level01").transform);
                gameObj.transform.localPosition = new Vector3(0, (i * 10) + (GO.transform.localScale.y/2), 3.31f);
            }
        }
        yield break;
    }
    
    private IEnumerator GenerateSideWall()
    {
        for (int i = -7; i <= (3 * maxPlatform)-1; i+=4)
        {
            //Check Level And Get That Level Background
            GameObject GOLeft = Instantiate(sideWallLeft);
            GOLeft.transform.localPosition = new Vector3(0, i);
            GameObject GORight = Instantiate(sideWallRight);
            GORight.transform.localPosition = new Vector3(0, i);
        }
        yield break;
    }
    
    //Levele göre BG-Wall-Platform
    public void NewPrefabs()
    {
        //Satın alınan item buraya gelicek
        GameObject[] levelendp = Resources.LoadAll<GameObject>("Prefabs/LevelEnd");
        levelEnd = levelendp[0];
        GameObject[] wallp = Resources.LoadAll<GameObject>("Prefabs/Wall");
        background = wallp[0];
        GameObject[] platformp = Resources.LoadAll<GameObject>("Prefabs/Platform");
        platform = platformp[0];
        GameObject[] floorp = Resources.LoadAll<GameObject>("Prefabs/Floor");
        floor = floorp[0];
        if (level == Resources.LoadAll<GameObject>("Prefabs/LevelEnd").Length)
            level = 0;
        difficulty++;
    }
}
