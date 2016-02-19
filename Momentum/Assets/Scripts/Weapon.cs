using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
namespace Assets
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract float getRange();
        public abstract float getCooldown();
    }
}