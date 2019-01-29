using System.Net.Mime;
using PsycthoMothers.Battle.Players;
using PsycthoMothers.Battle.Traps;
using UnityEngine;
using UnityEngine.UI;

namespace PsycthoMothers.Battle.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField]private TrapType type;
        public TrapType TrapType => type;
    }
}
