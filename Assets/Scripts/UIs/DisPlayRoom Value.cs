using TMPro;
using UnityEngine;

public class DisPlayRoomValue : MonoBehaviour
{
    [SerializeField] private TMP_Text tMP_Text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (tMP_Text.text != RoomData.Instance.roomCode)
        {
            UpdateText();
        }
    }


    public void UpdateText()
    {
        tMP_Text.text = RoomData.Instance.roomCode;
    }
}
