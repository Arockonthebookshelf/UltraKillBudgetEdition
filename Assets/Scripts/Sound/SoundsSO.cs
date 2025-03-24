using UnityEngine;

namespace PrometheanUprising.SoundManager
{
    [CreateAssetMenu(menuName = "Sounds/Sounds SO", fileName = "Sounds SO")]
    public class SoundsSO : ScriptableObject
    {
        public SoundList[] sounds;
    }
}