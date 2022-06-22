using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorDatabase : MonoBehaviour
{
    #region Singleton Init
    private static EditorDatabase _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }

    public static EditorDatabase Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<EditorDatabase>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion

    public static bool IsInited = false;

#if UNITY_EDITOR
    [Header("Auto")]
    [NaughtyAttributes.Required]
    public Object levelsFolder;
#endif

    [Header("Data")]
    public List<GameObject> levelDatas;

    void Initialize()
    {
        // Init data here
        enabled = true;
    }

#if UNITY_EDITOR
    [NaughtyAttributes.Button]
    void FillData()
    {
        //levelDatas = AssetLoader<GameObject>.GetItems(levelsFolder);


#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif

    }
#endif
}
