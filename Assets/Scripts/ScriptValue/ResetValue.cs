using System.Collections.Generic;
using UnityEngine;

public class ResetValue : MonoBehaviour
{
   [SerializeField] private List<ScriptableValueBase> scriptableObject;

    void Awake()
    {
        foreach(var T in scriptableObject)
        {
            T.ResetValue();
        }
    }

}
