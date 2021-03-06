using UnityEngine;
public class PlayerInfo
{
    public bool racing = false;
    public bool AIRacer = false;
    public int CarID;
    public int MaterialID;
    public GameObject PlayerObject;
}

public class PlayersKeeper : MonoBehaviour
{
    private PlayerInfo[] Infos;

    private void Awake()
    {
        Infos = new PlayerInfo[4];
        for (int i = 0; i < Infos.Length; i++)
        {
            Infos[i] = new PlayerInfo();
        }
    }

    public PlayerInfo[] ExportPlayers()
    {
        return Infos;
    }

    public bool IsPlayerRacing(int PlayerID)
    {
        return Infos[PlayerID].racing;
    }

    public bool IsPlayerAI(int PlayerID)
    {
        return Infos[PlayerID].AIRacer;
    }

    public int ReturnCarID(int PlayerID)
    {
        return Infos[PlayerID].CarID;
    }

    public int ReturnMaterialID(int PlayerID)
    {
        return Infos[PlayerID].MaterialID;
    }

    public void SetPlayerInfo(int PlayerID, int CarID, int MaterialID, bool AI)
    {
        Infos[PlayerID].racing = true;
        Infos[PlayerID].CarID = CarID;
        Infos[PlayerID].MaterialID = MaterialID;
        Infos[PlayerID].AIRacer = AI;
    }

    public void UnSetPlayerInfo(int PlayerID)
    {
        Infos[PlayerID].racing = false;
    }

    public int GetPlayerCount()
    {
        int count = 0;
        for (int i = 0; i < Infos.Length; i++)
        {
            if (Infos[i].racing)
            {
                count++;
            }
        }
        return count;
    }

    public void UnSetAllPlayers()
    {
        for (int i = 0; i < Infos.Length; i++)
        {
            Infos[i].racing = false;
        }
    }
}
