using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NavAgentScriptableObject", menuName = "ScriptableObjects/NavAgentScriptableObject", order = 1)]
public class NavMeshAgentScriptableObject : ScriptableObject
{
    public float myDestinationCloseEnaught = 0.1f;
    public float myLeaderDestinationCloseEnaught = 2.0f;
}
