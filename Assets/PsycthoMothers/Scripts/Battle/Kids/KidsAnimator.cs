using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;


namespace PsycthoMothers.Battle.Kids
{
    /// <summary>
    /// KidsAnimator
    /// 子供のアニメーションクラス
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class KidsAnimator : MonoBehaviour
    {
        /// <summary>
        /// Rigidbody2D
        /// </summary>
        [SerializeField] private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// Animator
        /// </summary>
        [SerializeField] private Animator _anim;

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            this.UpdateAsObservable()  
                .Subscribe(_ =>
                {
                    _anim.SetFloat ("x", _rigidbody2D.velocity.x); 
                    _anim.SetFloat ("y", _rigidbody2D.velocity.y); 
                });
        }
    }
}


