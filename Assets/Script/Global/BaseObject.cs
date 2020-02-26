using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    Dictionary<string, UnityEngine.Component> DicComponent =
        new Dictionary<string, Component>(); //컴포넌트에 연결할 딕셔너리

    BaseObject TargetComponent = null; //자신의 정보를 가져올 타겟 컴포넌트

    public BaseObject Target
    {
        get { return TargetComponent; }
        set { TargetComponent = value; }
    }

    eBaseObjectState ObjectState = eBaseObjectState.STATE_NORMAL;
    public eBaseObjectState OBJECT_STATE
    {
        get
        {
            if (Target == null)
                return ObjectState;
            else
                return Target.OBJECT_STATE;
        }

        set
        {
            if (Target == null)
                ObjectState = value;
            else
                Target.OBJECT_STATE = value;
        }
    }

    public GameObject SelfObject
    {
        get
        {
            if (Target == null)
                return this.gameObject;
            else
                return Target.gameObject;
        }
    }

    public Transform SelfTransform
    {
        get
        {
            if (Target == null)
                return this.transform;
            else
                return Target.transform;
        }
    }

    public virtual object GetData(string keyData, params object[] datas)
    {
        return null;
    }

    public virtual void ThrowEvent(string keyData, params object[] datas)
    {

    }

    public Transform FindInChild(string strName)
    {
        return _FindInChild(strName, SelfTransform);
    }

    Transform _FindInChild(string strName, Transform trans)
    {
        if (trans.name == strName)
            return trans;

        for(int i = 0; i< trans.childCount; i++)
        {
            Transform returnTrans = _FindInChild(strName, trans.GetChild(i));
            if (returnTrans != null)
                return returnTrans;
        }
        return null;
    }

    public T SelfComponent<T>() where T : UnityEngine.Component
    {
        string objectName = string.Empty;
        string typeName = typeof(T).ToString();
        T tempComponent = default(T);

        if(Target == null)
        {
            objectName = this.gameObject.name;

            if(DicComponent.ContainsKey(typeName))
            {
                tempComponent = DicComponent[typeName] as T;
            }
            else
            {
                tempComponent = this.GetComponent<T>();

                if(tempComponent == null)
                {
                    Debug.LogError("ObjectName : " + objectName
                        + ", Missing Component : " + typeName);
                    tempComponent = this.gameObject.AddComponent<T>();
                }
                else
                {
                    DicComponent.Add(typeName, tempComponent);
                }
            }
        }
        else
        {
            objectName = Target.SelfObject.name;
            tempComponent = TargetComponent.SelfComponent<T>();
        }

        return tempComponent;
    }
}
