using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UICanvas> canvaActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasPrefabs = new Dictionary<System.Type, UICanvas>();
    [SerializeField] Transform parent;

    private void Awake()
    {
        //load UI prefab from resources
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/");

        for(int i = 0; i < prefabs.Length; i++)
        {
            canvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);

        }
    }

    //open canvas
    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();   
        canvas.SetUp();
        canvas.Open();
        return canvas;
    }

    //close canvas after time
    public void CloseUI<T>(float time) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvaActives[typeof(T)].Close(time);
        }
    }

    //close canvas now
    public void CloseUIDirectly<T>(float time) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvaActives[typeof(T)].CloseDirectly();
        }
    }

    //check spawn canvas
    public bool IsLoaded<T>() where T : UICanvas
    {
        return canvaActives.ContainsKey(typeof(T)) && canvaActives[typeof(T)] != null;
    }

    //check active canvas
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && canvaActives[typeof(T)].gameObject.activeSelf;
    }

    //get active canvas
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab, parent);

            canvaActives[typeof(T)] = canvas;
        }

        return canvaActives[typeof(T)] as T;
    }

    //get canvas
    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)] as T;
    }

    //close all canvas
    public void CloseAllUI()
    {
        foreach (var canvas in canvaActives)
        {
            if (canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
}
