using PsycthoMothers.Battle.Items;
using PsycthoMothers.Battle.Traps;
using UniRx;
using UnityEngine;


namespace PsycthoMothers.Battle.Players
{
    public class ItemHolder : MonoBehaviour
    {
        public ReactiveProperty<bool> IsItemHold = new ReactiveProperty<bool>();
        private TrapType? holdItem;
        public TrapType? TrapType => holdItem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var item = collision.gameObject.GetComponent<Item>();
            if (item == null) return;
            holdItem = item.TrapType;
            IsItemHold.SetValueAndForceNotify(true);
            Destroy(collision.gameObject);
        }
    }
}
