using System.Collections.Generic;
using UnityEngine;

namespace PsycthoMothers.Battle.Players
{
    /// <summary>
    /// PlayerDrawer
    /// 見た目：スプライトアニメーションコントローラーを切り替えます
    /// </summary>
    public class PlayerDrawer : MonoBehaviour
    {
        /// <summary>
        /// Animator
        /// </summary>
        [SerializeField] private Animator _anim;

        [SerializeField] private Sprite[] _namePlates;
        [SerializeField] private SpriteRenderer _namePlate;

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            var core = GetComponent<PlayerCore>();

            _namePlate.sprite = _namePlates[(int) core.PlayerId];

            switch (core.PlayerId)
            {
                case PlayerId.Player1:
                    _anim.runtimeAnimatorController = 
                        (RuntimeAnimatorController)Instantiate(UnityEngine.Resources.Load("Animator/RedMother"));
                    break;
                case PlayerId.Player2:
                    _anim.runtimeAnimatorController =
                        (RuntimeAnimatorController)Instantiate(UnityEngine.Resources.Load("Animator/YellowMother"));
                    break;
                case PlayerId.Player3:
                    _anim.runtimeAnimatorController =
                        (RuntimeAnimatorController)Instantiate(UnityEngine.Resources.Load("Animator/BlueMother"));
                    break;
                case PlayerId.Player4:
                    _anim.runtimeAnimatorController =
                        (RuntimeAnimatorController)Instantiate(UnityEngine.Resources.Load("Animator/GreenMother"));
                    break;
            }
        }
    }
}

