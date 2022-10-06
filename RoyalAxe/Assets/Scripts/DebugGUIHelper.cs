using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;


public class DebugGUIHelper : MonoBehaviour
{
    public string AppBundleVErsion = "Enter Version";

    //стиль дебага
    public string debuger;

    //
    private bool showDebug = false;

    private bool showguiDebug = false;

    //отображение дебага
    public bool fullDebug = false;

    //отображение полногодебага

    private float fps = 0;

    [ContextMenu("ClearPrefs")]
    public void ClearPreffs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Caching.ClearCache();
    }


    private StreamWriter _textFile;

    private string TestPrefs;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    private void OnGUI()
    {
        //кнопка RESET - загружает уровень 4
        if (GUI.Button(new Rect(0, 110, 50, Screen.height / 10), "Clear\n Saves"))
        {
            ClearPreffs();
            //SYS_Info.loadingStage = ServerInfo.LoadingStage.error;
            //kiiroGamecall.sceneToLoad = 4;
        }
        //FORCE EXIT
        if (GUI.Button(new Rect(0, 0, 50, 40), "Force\nQuit"))
        {
            Application.Quit();
            //SYS_Info.loadingStage = ServerInfo.LoadingStage.error;
            //kiiroGamecall.sceneToLoad = 4;
        }
        //кнопка DEBUG
        if (GUI.Button(new Rect(50, Screen.height - Screen.height / 10, 50, Screen.height / 10), "Log"))
        {
            showDebug = !showDebug;
        }

        ////кнопка DEBUG
        //if (GUI.Button(new Rect(50, Screen.height - Screen.height / 10, 100, Screen.height / 10), "gui"))
        //{
        //    showguiDebug = !showguiDebug;
        //}

        if (GUI.Button(new Rect(120, Screen.height - Screen.height / 10, 70, Screen.height / 10), "CLEAR"))
        {
            debuger = "";
        }



        //if (GUI.Button (new Rect (190, Screen.height - Screen.height / 10, 70, Screen.height / 10), "Send log"))
        //{
        //	GameCore.SendMail (CopyToClipboard (Application.persistentDataPath + "/" + _fileName));
        //}

        if (showDebug)
        {
            GUI.Box(new Rect(15, 20, (Screen.width - 30), (Screen.height - 40)), new GUIContent(debuger));
            //GUI.Box(new Rect(15, 20, (Screen.width - 30), (Screen.height - 40)), debuger, "Debuger");
        }

        if (showguiDebug)
        {
            GUI.Box(new Rect(15, 20, (Screen.width - 30), (Screen.height - 40)), new GUIContent(EventSystem.current.ToString()));
            //GUI.Box(new Rect(15, 20, (Screen.width - 30), (Screen.height - 40)), debuger, "Debuger");
        }
        GUI.Box(new Rect(Screen.width - 460, Screen.height - 20, 100, 20), "Ver: " + TestPrefs);
        GUI.Box(new Rect(Screen.width - 360, Screen.height - 20, 100, 20), "FPS: " + fps);
    }



    //считаем fps. Запускаем в Start()
    private IEnumerator FPSCheck()
    {
        fps = (1 / Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FPSCheck());
    }

    private void Start()
    {
        Application.targetFrameRate = 60; //нужный fps
                                          // Application.logMessageReceived += OnLog;
        Application.logMessageReceivedThreaded += OnLog;
        StartCoroutine(FPSCheck());
    }

    private void OnLog(string condition, string stackTrace, LogType type)
    {
        DebugMSG(type.ToString(), condition);
        if (type == LogType.Exception)
            DebugMSG("Stack ", stackTrace);
    }


    //вывод влог делать с использованием этого метода, а не Debug.Log().
    //Вывод будет и на устройстве и в логах
    public void DebugMSG(string plugin, object msg)
    {
        string time = System.DateTime.Now.ToString("HH:mm:ss");

        string toShow = "(" + time + ") " + plugin + ": " + msg.ToString();
        ;
        debuger = toShow + "\n\n" + debuger;

        if (debuger.Length > 5000)
        {
            debuger = "";
        }
        if (_textFile != null)
            _textFile.WriteLine(toShow);
        // Debug.Log(toShow);
    }

    private void OnDestroy()
    {
        if (_textFile != null)
        {
            _textFile.Close();
            _textFile.Dispose();
        }
    }

    public void Log(string message, params object[] formatArgs)
    {
        string mes = "";

        foreach (var sss in formatArgs)
            mes = mes + sss.ToString() + " ";
        DebugMSG(message, mes);
    }

    public void LogWarning(string message, params object[] formatArgs)
    {
        DebugMSG("Warning", "");
        foreach (var sss in formatArgs)
            DebugMSG("", sss);
    }

    public void LogError(string message, params object[] formatArgs)
    {
        DebugMSG("Error", "");
        foreach (var sss in formatArgs)
            DebugMSG("", sss);
    }

    public string prefix { get; set; }
}