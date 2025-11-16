using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class SetUpWapper
{
    public int dividePixels;
    public float mouseDragRadius;
    public float fadeSpeed;
    public string pixelatePatturn_String;
    public string allColors_string;

    public int a;
    public int b;
}

[System.Serializable]
public class ChangePixelsWapper
{











}
public class PixelatedHandle : MonoBehaviourPunCallbacks
{
    private static PixelatedHandle _instance;
    public static PixelatedHandle Instance => _instance;
    public GlitchPixelated mainGiltch;
    public GlitchPixelated otherGlitch;
    public Sprite originalImage;
    public Texture2D texture2D;
    public float delay;
    public bool showDebug;

    private bool mouseMovedThisFrame = false;
    private Vector3 lastMousePosition;

    [SerializeField] private Image image;
    private int a;
    private int b;

    public int myIndex;
    public float movementThreshold = 0.1f;
    private Coroutine IE_SendFadeData;
    public int[,] num =
    {
        {1,2,3,4,5},
        {6,7,8,9,10}
    };

    public float timeToSendFadeDataToMaster = 0.5f;     // public int[,] num2;
    // public int[] nt;
    // public int w;
    // public int h;

    // [ContextMenu("TestConvertArray")]
    // public void TestConvertArray()
    // {
    //     nt = mainGiltch.ConverArray2DTo1D<int>(num, out var _width, out var _height);
    //     w = _width;
    //     h = _height;

    // }

    // [ContextMenu("DebugArray2d")]
    // public void DebugArray2d()
    // {
    //     for (int i = 0; i < w; i++)
    //     {
    //         for (int j = 0; j < w; j++)
    //         {
    //             Debug.Log($" {i} {j} {num2[i, j]}");
    //         }
    //     }
    // }

    public GameEvent finushSetUp;
    void OnEnable()
    {
        _instance = this;
    }
    void Start()
    {
        //SetOriginalTexturn();

    }
    [ContextMenu("SetOriginalTexturn")]
    public void SetOriginalTexturn()
    {
        SetOriginalTexturn(originalImage.texture);
    }
    public void SetOriginalTexturn(Texture2D _originalTexturn)
    {
        mainGiltch.SetOriginalTexturn(_originalTexturn);
        texture2D = _originalTexturn;
    }
    [ContextMenu("Pixelate")]
    public void Pixelate(int _a, int _b)
    {
        mainGiltch.PixelatedMethod();
        a = _a;
        b = _b;
    }
    #region SendAndRecive Setup
    [ContextMenu("SendSetUp")]
    public void SendSetUp()
    {

        //  otherGlitch.ReciveSetUp(mainGiltch.GetSetUp());
        mainGiltch.GetSetUp(out int _dividePixels, out float _mouseDragRadius, out float _fadeSpeed, out Color[,] _pixelatePatturn, out Color[] _allColors, out Texture2D _originalTextuen);


        Texture2dData texture2DData = new Texture2dData()
        {
            textureName = _originalTextuen.name,
            width = _originalTextuen.width,
            height = _originalTextuen.height,
            colors = _originalTextuen.GetPixels()
        };
        var pix = mainGiltch.ConvertColorArrayToJson(_pixelatePatturn);
        var al = mainGiltch.ConvertTextureToJson(_originalTextuen);
        SetUpWapper setUpWapper = new SetUpWapper()
        {
            dividePixels = _dividePixels,
            mouseDragRadius = _mouseDragRadius,
            fadeSpeed = _fadeSpeed,
            pixelatePatturn_String = pix,
            allColors_string = "al",
            a = this.a,
            b = this.b
        };

        var setUpWapperJson = JsonUtility.ToJson(setUpWapper);
        Debug.Log($"Setup Data {setUpWapperJson}");
        photonView.RPC("RPC_ReciveSetUp", RpcTarget.Others, setUpWapperJson);
    }
    [PunRPC]
    public void RPC_ReciveSetUp(string _json)
    {
        //     Debug.Log($"R {_json}");
        SetUpWapper setUpWapper = JsonUtility.FromJson<SetUpWapper>(_json);
        int dividePixels = setUpWapper.dividePixels;
        float mouseDragRadius = setUpWapper.mouseDragRadius;
        float fadeSpeed = setUpWapper.fadeSpeed;
        Color[,] pixelatePatturn = mainGiltch.ConvertJsonToColorArray(setUpWapper.pixelatePatturn_String);
        string allColorsSTR = setUpWapper.allColors_string;
        //  Color[] allColors = mainGiltch.ConvertJsonToTexture(allColorsSTR);
        int a = setUpWapper.a;
        int b = setUpWapper.b;

        SpriteShow spriteShow = null;
        foreach (var T in SpriteShow.allSpriteShow)
        {
            if (T.index == a)
            {
                spriteShow = T;
                break;
            }
        }


        Texture2D originalTextuen = GameManager.Instance.spriteShows[a].sprites[b].texture;

        // Texture2D originalTextuen = new Texture2D(setUpWapper.originalTextuen.width, setUpWapper.originalTextuen.height);
        // originalTextuen.SetPixels(setUpWapper.originalTextuen.colors);
        // originalTextuen.Apply();
        // originalImage.name = setUpWapper.originalTextuen.textureName;

        mainGiltch.ReciveSetUp(dividePixels, mouseDragRadius, fadeSpeed, pixelatePatturn, originalTextuen);

        finushSetUp.Raise(this, -999);
    }
    #endregion



    #region SendAndRecive FadeData

    public void StartSendFadeData()
    {
        Debug.Log("StartSendFadeData ");
        StopAllCoroutines();
        StartCoroutine(SendFaerDataToMaster());

    }
    public void StopSendFadeData()
    {
          Debug.Log("StopSendFadeData ");
        StopAllCoroutines();
    }
    IEnumerator SendFaerDataToMaster()
    {
        while (RoomData.Instance.gameStart)
        {
                   Debug.Log("SendFaerDataToMaster ");
            yield return new WaitForSeconds(timeToSendFadeDataToMaster);
            RequateFadeData();
        }
    }

    public void RequateFadeData()
    {
        Debug.Log("RequateFadeData ");
        photonView.RPC("RPC_SendFadeData", RpcTarget.Others);
    }

    [PunRPC]

    public void RPC_SendFadeData()
    {
        FadeDataWapper fadeDataWapper = new FadeDataWapper();
        mainGiltch.GetFadeData(out var _colorFadeValue, out var _changedPixels);
        var array1d = mainGiltch.ConverArray2DTo1D<float>(_colorFadeValue, out var _width, out var _height);

        fadeDataWapper.width = _width;
        fadeDataWapper.height = _height;
        fadeDataWapper.colorFadeValue = array1d;
        fadeDataWapper.changedPixels = _changedPixels;
        fadeDataWapper.indexNotRevice = myIndex;

        var jsonData = JsonUtility.ToJson(fadeDataWapper);
        Debug.Log($"RPC_SendFadeData {jsonData}");
        photonView.RPC("RPC_ReciveFadeData", RpcTarget.MasterClient, jsonData);
        mainGiltch.ClearFadeData();

    }

    [PunRPC]
    private void RPC_ReciveFadeData(string _jsonData)
    {
        Debug.Log($"RPC_ReciveFadeData {_jsonData}");
        FadeDataWapper fadeDataWapper = JsonUtility.FromJson<FadeDataWapper>(_jsonData);

        var array2d = mainGiltch.ConvertArray1DTo2D<float>(fadeDataWapper.colorFadeValue, fadeDataWapper.width, fadeDataWapper.height);


        mainGiltch.ReciveData(array2d, fadeDataWapper.changedPixels);

        photonView.RPC("RPC_ReciveFadeDataFormMaster", RpcTarget.Others, _jsonData);
    }


    [PunRPC]
    private void RPC_ReciveFadeDataFormMaster(string _jsonData)
    {
        Debug.Log($"RPC_ReciveFadeDataFormMaster {_jsonData}");
        FadeDataWapper fadeDataWapper = JsonUtility.FromJson<FadeDataWapper>(_jsonData);
        if (fadeDataWapper.indexNotRevice == myIndex) return;

        var array2d = mainGiltch.ConvertArray1DTo2D<float>(fadeDataWapper.colorFadeValue, fadeDataWapper.width, fadeDataWapper.height);
        mainGiltch.ReciveData(array2d, fadeDataWapper.changedPixels);
    }

    #endregion























    [ContextMenu("Start Loop")]
    public void StartLoopTime()
    {
        StartCoroutine(LoopTime());
    }
    [ContextMenu("Stop Loop")]
    public void StopLoopTime()
    {
        StopAllCoroutines();
    }
    private IEnumerator LoopTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (showDebug) Debug.Log($"Update {gameObject.name}");
            mainGiltch.canFade = false;
            float[,] colorFadeValue;
            List<Vector2Int> changedPixelsData;
            mainGiltch.GetFadeData(out colorFadeValue, out var _changedPixelsData);
            changedPixelsData = new List<Vector2Int>(_changedPixelsData);


            //   Debug.Log($"{colorFadeValue.Length}");
            //   Debug.Log($"{changedPixelsData.Count}");
            otherGlitch.ReciveData(colorFadeValue, changedPixelsData);
            mainGiltch.ClearFadeData();
            mainGiltch.canFade = true;
        }

    }


















    void OnMouseDrag()
    {
        if (CheckForMouseMovement())
        {
            mainGiltch.MouseDrag();
        }
    }
    public void OnMouseDragForOther()
    {
        if (CheckForMouseMovement())
        {
            mainGiltch.MouseDrag();
        }
    }

    public bool CheckForMouseMovement()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        float distanceMoved = Vector3.Distance(currentMousePosition, lastMousePosition);

        lastMousePosition = currentMousePosition;

        if (distanceMoved > movementThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
