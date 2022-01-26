using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDataLoader : ScriptableObject
{
    public DataLoaders Key;

    public abstract IEnumerator Init();
}

public class DataLoadSystem : MonoBehaviour
{
    [SerializeField] private List<BaseDataLoader> _loaders;

    private static readonly Dictionary<DataLoaders, BaseDataLoader> StaticDictionary = new Dictionary<DataLoaders, BaseDataLoader>();

    public static T GetLoader<T>(DataLoaders loader) where T : BaseDataLoader
    {
        return StaticDictionary[loader] as T;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _loaders.Count; i++)
        {
            var loader = _loaders[i];
            StartCoroutine(_loaders[i].Init());
            StaticDictionary.Add(loader.Key, loader); 
        }
    }
}


