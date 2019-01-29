using System;
using PsycthoMothers.Battle.Traps;
using UnityEngine;
using UniRx;

namespace PsycthoMothers.Battle.Players
{
    public class DrawItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _itemImageSpriteRenderer;

        [SerializeField] private Sprite _bomb;
        [SerializeField] private Sprite _bumper;
        [SerializeField] private Sprite _sandbox;
        [SerializeField] private Sprite _blackhole;

        private void Start()
        {
            var itemHolder = GetComponent<ItemHolder>();

            itemHolder.IsItemHold
                .Subscribe(x =>
                {
                    if (!x)
                    {
                        _itemImageSpriteRenderer.enabled = false;
                    }
                    else
                    {
                        _itemImageSpriteRenderer.enabled = true;
                        switch (itemHolder.TrapType)
                        {
                            case TrapType.Bomb:
                                _itemImageSpriteRenderer.sprite = _bomb;
                                break;
                            case TrapType.Bumper:
                                _itemImageSpriteRenderer.sprite = _bumper;
                                break;
                            case TrapType.BigArm:
                                break;
                            case TrapType.SandBox:
                                _itemImageSpriteRenderer.sprite = _sandbox;
                                break;
                            case TrapType.BlackHole:
                                _itemImageSpriteRenderer.sprite = _blackhole;
                                break;
                            case null:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                });
        }
    }
}
