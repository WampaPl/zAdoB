#if UNITY_EDITOR
    using UnityEditor;
#endif
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;
using Color = UnityEngine.Color;

public class ControllableNavAgent : MonoBehaviour
{   
    NavMeshAgent agent;
    ThirdPersonController myThirdPersonController;
    NavMeshAgentScriptableObject myVariableStorage;
    [SerializeField] Color MaterialColor;

    Vector3 myDestination;
    ControllableNavAgent myLeaderDestination;

    float predkosc;
    float zwrotnosc;
    float wytrzmyalosc;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myThirdPersonController = GetComponent<ThirdPersonController>();
        myVariableStorage = (NavMeshAgentScriptableObject) Resources.Load<NavMeshAgentScriptableObject>("NavAgentScriptableObject");

/*#if UNITY_EDITOR
        myVariableStorage = (NavMeshAgentScriptableObject) AssetDatabase.LoadAssetAtPath("Assets/NavAgentScriptableObject.asset", typeof(NavMeshAgentScriptableObject));
#endif*/
       

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            foreach (Material mat in renderer.materials)
            {
                mat.SetColor("_Color", MaterialColor);
            }
        }

        RandomiseStats();
    }

    void Update()
    {
        myThirdPersonController.NavAgentAnimation(agent.velocity.magnitude);

        if (myLeaderDestination == null)
        {
            if (Vector3.Distance(this.transform.position, myDestination) <= myVariableStorage.myDestinationCloseEnaught)
                agent.ResetPath();//Jestem wystarczaj¹co blisko celu: zadanie wykonane
        }
        else
        {
            if (Vector3.Distance(this.transform.position, myLeaderDestination.transform.position) <= myVariableStorage.myLeaderDestinationCloseEnaught)
                agent.ResetPath();//Jestem wystarczaj¹co blisko lidera, nie bede sie bardziej pchal
            else
                this.SetDestination(myLeaderDestination.transform.position);//Ide w kierunku lidera
        }
    }

    public void SetDestination(Vector3 dest)
    {
        //Debug.Log("vector3 set dest");
        myDestination = dest;
        agent.destination = myDestination;
    }

    public void SetDestination(ControllableNavAgent dest)
    {
        //Debug.Log("controllableNavAgent set dest");
        myLeaderDestination = dest;
    }

    void RandomiseStats()
    {
        predkosc = Random.Range(2.0f, 5.0f);
        zwrotnosc = Random.Range(90.0f, 360.0f);
        wytrzmyalosc = Random.Range(10.0f, 100.0f);

        SetAgentSpeed();
        SetAgentAngularSpeed();
    }

    void SetAgentSpeed()
    {
        agent.speed = predkosc;
    }

    void SetAgentAngularSpeed()
    {
        agent.angularSpeed = zwrotnosc;
    }
}
