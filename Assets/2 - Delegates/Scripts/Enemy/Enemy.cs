using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Delegates
{
    public abstract class Enemy : MonoBehaviour
    {

        public enum Behaviour
        {
            IDLE = 0,
            SEEK = 1
        }

        delegate void BehaviourFunc();

        public Transform target;
        public Behaviour behaviourIndex = Behaviour.SEEK;

        private List<BehaviourFunc> behaviourFuncs = new List<BehaviourFunc>();
        private NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            //Assign delegates to lilst here
            behaviourFuncs.Add(Idle);
            behaviourFuncs.Add(Seek);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            behaviourFuncs[(int)behaviourIndex]();
        }

        void Idle()
        {
            //Stop the nav agent
            agent.Stop();
        }

        void Seek()
        {
            //Resume nav agent
            agent.Resume();
            if (target != null)
            {
                //Move agent to target
                agent.SetDestination(target.position);
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public bool IsCloseToTarget(float distance)
        {
            if (target != null)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (distToTarget <= distance)
                {
                    return true;
                }
            }
            return false;
        }
    }
}