#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MyPlayerController : MonoBehaviour
{
    Camera myCamera;
    public List<ControllableNavAgent> controllableNavAgents;
    public List<ControllableNavAgent> controllableNavAgentLeaders;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit) && myRaycastHit.transform.gameObject.GetComponent<NavMeshSurface>())
            {
                foreach (ControllableNavAgent agent in controllableNavAgentLeaders)
                {
                    agent.SetDestination((ControllableNavAgent)null);
                    agent.SetDestination(myRaycastHit.point);
                }

                foreach (ControllableNavAgent agent in controllableNavAgents)
                    if (!controllableNavAgentLeaders.Contains(agent)) //(agent != controllableNavAgentLeaders)
                        agent.SetDestination(controllableNavAgentLeaders[0]);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Application.Quit();
        }
    }

    public void AddLeader(ControllableNavAgent toAdd)
    {
        foreach (ControllableNavAgent agent in controllableNavAgentLeaders)
            if (toAdd == agent)
                return;

        controllableNavAgentLeaders.Add(toAdd);
    }

    public void RemoveLeader(ControllableNavAgent toRemove)
    {
        controllableNavAgentLeaders.Remove(toRemove);
    }
}
