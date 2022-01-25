using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataLoader
{
    public IEnumerator Init();
}

public abstract class BaseDataLoader : ScriptableObject
{
    public abstract IEnumerator Init();
}

public class DataLoadSystem : MonoBehaviour
{
    [SerializeField] private List<BaseDataLoader> _loaders;

    private static readonly Dictionary<string, BaseDataLoader> StaticDictionary = new Dictionary<string, BaseDataLoader>();

    public static T GetLoader<T>(string loader) where T : BaseDataLoader
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
            StaticDictionary.Add(i.ToString(), loader); //временное решение
        }
    }
}


