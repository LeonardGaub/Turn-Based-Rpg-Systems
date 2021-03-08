using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.SkillTree
{
    [Serializable]
    public class Skill : MonoBehaviour
    {
        public int id_Skill;
        public int[] skill_Dependencies;
        public bool unlocked;
        public int cost = 0;
    }
}
