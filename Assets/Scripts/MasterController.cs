using TMPro;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    public static MasterController Instance;
     private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    [Header("UI")]
    [SerializeField] private TMP_InputField gameTime_input;
    //Room Code
    [SerializeField] private TMP_Text roomCode_text;

    [Header("Value")]
    [SerializeField] private StringValue roomCode;
    [SerializeField] private FloatValue gameTime_value;
    [SerializeField] private FloatValue dust_wipe;
    public string GenerateRandomCode(int length = 4)
    {
        string result = "";

        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, chars.Length);
            result += chars[index];
        }

        return result;
    }


    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        gameTime_input.text = gameTime_value.Value.ToString();
        gameTime_input.onValueChanged.AddListener(SetGameTime);
    }
    void FixedUpdate()
    {
        ShowRoomCode();
    }
    public string RandomRoomCode()
    {
        return roomCode.Value = GenerateRandomCode().ToLower();
    }

    #region Room Code
    public void ShowRoomCode()
    {
        roomCode_text.text = roomCode.Value.ToUpper();
    }

    public void NewRoom()
    {

    }
    #endregion

    #region Set Time

    public void SetGameTime(string _text)
    {
        if (IsAllDigits(gameTime_input.text))
            gameTime_value.Value = int.Parse(gameTime_input.text);
    }
    public bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
    #endregion

}
