using UnityEngine;

namespace _Scripts.Core.UI.AndroidUI
{
    public class SwitchWeapon : MonoBehaviour
    {
        public void Switch()
        {
            Debug.Log("Switch Weapon"); 
        }
    }

    public class AttackWeapon : MonoBehaviour
    {
        public void Attack()
        {
           Debug.Log("Attacking!"); 
        }
    }
    
    public class AltAttackWeapon : MonoBehaviour
    {
        public void AltAttack()
        {
            Debug.Log("Alt Attacking!"); 
        }
    }

    public class ToggleCompanion : MonoBehaviour
    {
        public void Toggle()
        {
            Debug.Log("Toggle companion");
        }
    }

    public class SwitchCompanion : MonoBehaviour
    {
        public void Switch()
        {
            Debug.Log("Switch companion");
        }
    }
}