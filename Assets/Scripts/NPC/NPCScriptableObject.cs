using Crafting;
using NaughtyAttributes;
using System.Collections.Generic;
using Gameplay.DayCycle;
using UnityEngine;
using static Class.MaskInfoClass;
using static Class.ObjawyClass;

namespace NPC
{
    [CreateAssetMenu(fileName = "Pacjent", menuName = "NPC`s/NPC", order = 100)]
    public class NPCScriptableObject : ScriptableObject
    {
        [SerializeField, Foldout("body")] private string imie = "Zbyszek";
        [SerializeField, Foldout("body")] private Mesh _maskMesh;
        [SerializeField, Foldout("body")] private Material _maskMaterial;

        [SerializeField, Foldout("choroba"), Required] private ChorobaScriptableObject _Choroba;

        [SerializeField, Foldout("Chat"), ResizableTextArea] private List<string> _EntryChat = new() { "Elo Doktorku" };

        [SerializeField, Foldout("Chat"), ResizableTextArea] private List<string> _NormalChat = new() { "Mam horom horobe" };
        [SerializeField, Foldout("Chat"), ResizableTextArea] private List<string> _ExitChat = new() { "Adios Bia³asie" };
        [SerializeField, Foldout("Chat"), Tooltip("Piersza opcja klient zadowolony, druga nie")] private string[] GazetaStory = new string[] { "Klient ¿yje i tañczy", "Klient Umar³ i nie ¿yje" };

        [SerializeField, Foldout("Pieni¹dze"), Tooltip("Ile zap³aci")] private int pieniazkiZaWyleczenie;
        [SerializeField, Foldout("Pieni¹dze"), Tooltip("Ile zap³aci")] private int pieniazkiZaPora¿ke;

        [SerializeField, Foldout("Envioroment"), Tooltip("Zmien pore dnia na wybrany, gdy ten npc sie pojawi")] public DayTime setDayTime;

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

        #endregion chat

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

        #endregion gazeta

        public DayTime GetDayTime()
        {
            return setDayTime;
        }

        public string GetName()
        {
            return imie;
        }

        public void SetWyleczonyPacjent(bool zadowolenie)
        {
            _wyleczonyPacjent = zadowolenie;
        }

        public bool CzyWyleczony()
        {
            return _wyleczonyPacjent;
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

        public MaskInfo GetMask()
        {
            MaskInfo maskInfo = new() { mesh = _maskMesh, material = _maskMaterial };
            return maskInfo;
        }

        public List<Objaw> GetObjawy()
        {
            return _Choroba.objawy;
        }

        public void DejLek(Lek lekarstwo)
        {
            bool wszytskieObjawy = true;
            foreach (var objaw in GetObjawy())
            {
                if (!lekarstwo.GetObjawy().Contains(objaw))
                {
                    wszytskieObjawy = false;
                }
            }

            SetWyleczonyPacjent(wszytskieObjawy);

            if (wszytskieObjawy)
            {
                Debug.Log("Sukces");
            }
            else
            {
                Debug.Log("Œmieræ");
            }
        }
    }
}