using UnityEngine;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    public const int IMAGE_COUNT = 30;
    private static SpriteManager _instance;
    public static SpriteManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpriteManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SpriteLoader");
                    _instance = go.AddComponent<SpriteManager>();
                }
            }
            return _instance;
        }
    }

    private const string IMAGE_PATH = "images/potrait/";
    private const string IMAGE_PREFIX = "puzzle_potrait_";

    private Dictionary<int, Sprite> spriteCache;

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }

        // 스프라이트 캐시 초기화
        spriteCache = new Dictionary<int, Sprite>();
    }

    // 인덱스로 스프라이트 로드
    public Sprite GetSpriteByIndex(int index)
    {
        if (spriteCache.ContainsKey(index))
        {
            return spriteCache[index];
        }

        string imagePath = $"{IMAGE_PATH}{IMAGE_PREFIX}{index:D4}";
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        if (sprite != null)
        {
            spriteCache[index] = sprite;
        }
        else
        {
            Debug.LogError($"Sprite not found at path: {imagePath}");
        }

        return sprite;
    }

    // 랜덤으로 스프라이트 로드
    public Sprite GetRandomSprite()
    {
        int randomIndex = Random.Range(1, IMAGE_COUNT);
        
        return GetSpriteByIndex(randomIndex);
    }
}