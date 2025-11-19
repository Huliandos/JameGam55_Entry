using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name + "_Singelton");
                go.hideFlags = HideFlags.HideAndDontSave;
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }

    protected virtual void Awake() 
    { 
		//Singleton in scene -> Set ref
		if(_instance == null)
			_instance = GetComponent<T>();

		//Duplicate of singleton in scene -> Destroy
		if(_instance != GetComponent<T>() && _instance != null)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (_instance != this)
            return;

        _instance = null;
    }
}
