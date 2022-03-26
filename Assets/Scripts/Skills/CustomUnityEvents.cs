using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntUnityEvent : UnityEvent<int> { } 
public class MassDeathUnityEvent : UnityEvent<bool> { }

public class StopSpawnUnityEvent : UnityEvent<bool> { }