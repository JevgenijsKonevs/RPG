using UnityEngine;
using RPG.Core;
namespace RPG.Combat
{   // when we place CombatTarget on the enemy then Health component is placed automatically
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {

    }
}