using UnityEngine;

namespace PsycthoMothers.Battle.Kids
{
    public class KidsCore : MonoBehaviour
    {
        [SerializeField] private KidsType _kidsType = KidsType.Normal;
        public KidsType KidsType => _kidsType;
    }
}
