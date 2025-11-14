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
    public Color[] allColors;
    public Texture2dData originalTextuen;
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

    public float movementThreshold = 0.1f;
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
    public void Pixelate()
    {
        mainGiltch.PixelatedMethod();
    }
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
        SetUpWapper setUpWapper = new SetUpWapper()
        {
            dividePixels = _dividePixels,
            mouseDragRadius = _mouseDragRadius,
            fadeSpeed = _fadeSpeed,
            pixelatePatturn_String = pix,
            allColors = _allColors,
            originalTextuen = texture2DData
        };

        var setUpWapperJson = JsonUtility.ToJson(setUpWapper);
        Debug.Log($"Setup Data {setUpWapperJson}");
        photonView.RPC("RPC_ReciveSetUp", RpcTarget.Others, setUpWapperJson);
    }
    [PunRPC]
    public void RPC_ReciveSetUp(string _json)
    {
        Debug.Log($"R {_json}");
        SetUpWapper setUpWapper = JsonUtility.FromJson<SetUpWapper>(_json);
        int dividePixels = setUpWapper.dividePixels;
        float mouseDragRadius = setUpWapper.mouseDragRadius;
        float fadeSpeed = setUpWapper.fadeSpeed;
        Color[,] pixelatePatturn = mainGiltch.ConvertJsonToColorArray(setUpWapper.pixelatePatturn_String);
        Color[] allColors = setUpWapper.allColors;

        Texture2D originalTextuen = new Texture2D(setUpWapper.originalTextuen.width, setUpWapper.originalTextuen.height);
        originalTextuen.SetPixels(setUpWapper.originalTextuen.colors);
        originalTextuen.Apply();
        originalImage.name = setUpWapper.originalTextuen.textureName;

        mainGiltch.ReciveSetUp(dividePixels, mouseDragRadius, fadeSpeed, pixelatePatturn, allColors, originalTextuen);


    }


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
