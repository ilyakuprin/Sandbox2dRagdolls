using System;
using UnityEngine;

namespace Sandbox
{
    public class JoystickDirection : JoystickHandler
    {
        public event Action<Vector2> DirectionSet;

        public void Update() => DirectionSet?.Invoke(GetInputVector);
    }
}
