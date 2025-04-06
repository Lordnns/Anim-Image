using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "Scenario", menuName = "ScriptableObjects/2D Anim/Scenario", order = 1)]
public class AnimationScenario : ScriptableObject
{
    [Serializable]
    public struct SequenceItem
    {
        public float time;
        public bool setAnim;
        public string newAnim;
        public Vector2 motion;
    }

    public SequenceItem[] sequence;
}
