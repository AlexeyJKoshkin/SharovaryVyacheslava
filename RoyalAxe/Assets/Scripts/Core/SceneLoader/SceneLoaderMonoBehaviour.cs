using System.Collections;
using UnityEngine;

namespace Core.Launcher
{
    public interface ISceneLoaderUnityView
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}