using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//오브젝트를 따로 할당할 필요가 없기위함
//싱글톤으로 받아 관리하기 쉽도록 한다.
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static bool bShutdown = false;
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //종료중이라면 만들지 않음.
                if (bShutdown == false)
                {
                    T instance = GameObject.FindObjectOfType<T>() as T;
                    if (instance == null)
                    {
                        //받아온 클래스 이름으로 오브젝트를 만듬
                        instance = new GameObject(typeof(T).ToString(),
                            typeof(T)).GetComponent<T>();
                    }
                    //초기화 작업
                    InstanceInit(instance);
                    Debug.Assert(_instance != null,
                        typeof(T).ToString()
                        + " 싱글턴 생성을 실패.");
                }
            }
            return _instance;
        }
    }

    private static void InstanceInit(Object instance)
    {
        _instance = instance as T;
        _instance.Init();
    }

    public virtual void Init()
    {
        DontDestroyOnLoad(_instance);// 디스트로이 하지 않는것이 있을수 있으므로 오버라이드로 가능하도록 함,
    }

    public virtual void OnDestroy()
    {
        _instance = null;
    }

    private void OnApplicationQuit()
    {
        _instance = null;
        bShutdown = true;
    }
}