using UnityEngine;

namespace Sandbox
{
    public sealed class HashingAnimation
    {
        public int WalkBack { get => Animator.StringToHash("Walk_back"); }
        public int WalkStraight { get => Animator.StringToHash("Walk_straight"); }
    }
}
