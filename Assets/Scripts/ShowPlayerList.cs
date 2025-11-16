using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerList : MonoBehaviour
{

    [SerializeField] private StringValue[] playerNames;
    [SerializeField] private StringValue[] playerIds;

    private bool loopPlayerList;
    void Start()
    {

    }


    void Update()
    {

    }

    public void SetLoopPlayerList(bool _bool)
    {
        loopPlayerList  = _bool;
    }
    void FixedUpdate()
    {
        if (loopPlayerList)
        {
            UpdatePlayerList();
        }
    }

    public void UpdatePlayerList()
    {
        var allPlayer = TeamManager.Instance.Team_Script.GetAllPlayer();


        for (int i = 0; i < 3; i++)

        {
            if (i < allPlayer.Count)
            {
                playerNames[i].Value = allPlayer[i].playerName;
                playerIds[i].Value = allPlayer[i].playerID;
            }
            else
            {
                playerNames[i].Value = "";
                playerIds[i].Value = "";
            }
        }
    }
}
