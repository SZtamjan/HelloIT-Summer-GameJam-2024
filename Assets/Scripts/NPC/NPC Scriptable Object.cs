using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pacjent", menuName = "NPC`s/NPC", order = 100)]
public class NPCScriptableObject : ScriptableObject
{
    [SerializeField, Foldout("body")] private new string name = "Zbyszek";
    [SerializeField, Foldout("body")] private GameObject _body;
    [SerializeField, Foldout("body")] private Mesh _maskMesh;
    [SerializeField, Foldout("body")] private Material _maskMaterial;

    [SerializeField, Foldout("Chat")] private List<string> _EntryChat = new List<string> { "Elo Doktorku" };

    [SerializeField, Foldout("Chat")] private List<string> _NormalChat = new List<string> { "Mam horom horobe" };
    [SerializeField, Foldout("Chat")] private List<string> _ExitChat = new List<string> { "Adios Bia³asie" };
    [SerializeField, Foldout("Chat"), Tooltip("Piersza opcja klient zadowolony, druga nie")] private string[] GazetaStory = new string[] { "Klient ¿yje i tañczy", "Klient Umar³ i nie ¿yje" };

    [SerializeField, Foldout("Pieni¹dze"), Tooltip("Ile zap³aci")] private int pieniazkiZaWyleczenie;
    [SerializeField, Foldout("Pieni¹dze"), Tooltip("Ile zap³aci")] private int pieniazkiZaPora¿ke;


    private bool _wyleczonyPacjent;

    #region chat    
    public List<string> EntryChatList()
    {
        return _EntryChat;
    }
    public string EntryChatId(int id)
    {
        if (id > (_EntryChat.Count - 1))
        {
            return null;
        }
        return _EntryChat[id];
    }

    public List<string> NormalChatList()
    {
        return _NormalChat;
    }

    public string NormalChatId(int id)
    {
        if (id > (_NormalChat.Count - 1))
        {
            return null;
        }
        return _NormalChat[id];
    }

    public List<string> ExitChatList()
    {
        return _ExitChat;
    }

    public string ExitChatId(int id)
    {
        if (id > (_ExitChat.Count - 1))
        {
            return null;
        }
        return _ExitChat[id];
    }

    #endregion

    #region gazeta



    public string GetGazeta()
    {
        if (_wyleczonyPacjent)
        {
            return GazetaStory[0];
        }
        else
        {
            return GazetaStory[1];
        }
    }

    #endregion

    public string GetName()
    {
        return name;
    }

    public void SetWyleczonyPacjent(bool zadowolenie)
    {
        _wyleczonyPacjent = zadowolenie;
    }

    public int GetPieniazki()
    {
        if (_wyleczonyPacjent)
        {
            return pieniazkiZaWyleczenie;
        }
        else
        {
            return pieniazkiZaPora¿ke;
        }

    }



}
