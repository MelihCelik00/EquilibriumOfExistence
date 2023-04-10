using UnityEngine;

namespace Managers
{
    public class CursorLock : MonoBehaviour
    {

        private void Start()
        {
            LockCursor();   
        }
        public static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        
        }

        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }       

    }
}