using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PsycthoMothers.Battle.Players;

namespace PsycthoMothers.Battle.Traps
{
    /// <summary>
    /// TrapEntity
    /// 配置される罠の実態
    /// </summary>
    public abstract class TrapEntity : MonoBehaviour
    {
        /// <summary>
        /// Type トラップタイプ
        /// </summary>
        public abstract TrapType Type { get;}

        /// <summary>
        /// Attacker　プレイヤーID
        /// </summary>
        public PlayerId Attacker { get; private set; }

        public void SetParams( PlayerId attacker)
        {
            Attacker = attacker;
        }
    }
}
