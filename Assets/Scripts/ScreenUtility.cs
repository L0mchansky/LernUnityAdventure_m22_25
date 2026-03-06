using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class ScreenUtility
    {        
        public Ray GetRayFromScreen() => Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
