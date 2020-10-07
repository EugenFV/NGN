using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ai
{
    public enum AgentState
    {
        Empty,
        Stay,
        Move
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveDestination : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Variables

        [Header("Components")]
        [SerializeField] private Transform _goal = null;

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Variables

        private NavMeshAgent _agent;
        private Animator _chAnimator;
        private AgentState _currentState;
        private float _rangeToPlayer = 0.0f;

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Interface

        public void SetAgentState(AgentState state)
        {
            if (_currentState == state)
                return;
            switch (state)
            {
                case AgentState.Empty:
                    SetEmptyState();
                    break;

                case AgentState.Stay:
                    SetStayState();
                    break;

                case AgentState.Move:
                    SetMoveState();
                    break;

                default:
                    SetEmptyState();
                    break;
            }
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region SetState

        private void SetEmptyState()
        {
            _agent.destination = _goal.position;
            _chAnimator.SetBool("Move", false);
            _currentState = AgentState.Empty;
        }

        private void SetStayState()
        {
            _agent.destination = _goal.position;
            _chAnimator.SetBool("Move", false);
            _currentState = AgentState.Stay;
        }

        private void SetMoveState()
        {
            _agent.destination = _goal.position;
            _chAnimator.SetBool("Move", true);
            _currentState = AgentState.Move;
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region MonoBehaviour Event Functions Implementation
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rangeToPlayer = _agent.stoppingDistance;
            _chAnimator = GetComponent<Animator>();
            SetAgentState(AgentState.Stay);
        }

        private void Update()
        {
            _agent.destination = _goal.position;
            switch (_currentState)
            {
                case AgentState.Empty:
                    AgentEmptyState();
                    break;

                case AgentState.Stay:
                    AgentStayState();                    
                    break;

                case AgentState.Move:
                    AgentMoveState(Time.deltaTime);
                    break;

                default:
                    break;
            }
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Behavior of the Crosshair

        private void AgentEmptyState()
        {
            if (_agent.remainingDistance > _rangeToPlayer)
            {
                SetAgentState(AgentState.Move);
                return;
            }
            else
            {
                SetAgentState(AgentState.Stay);
                return;
            }
        }

        private void AgentStayState()
        {
            if (_agent.remainingDistance > _rangeToPlayer)
            {
                SetAgentState(AgentState.Move);
                return;
            }
        }

        private void AgentMoveState(float deltaTime)
        {
            if (_agent.remainingDistance < _rangeToPlayer)
            {
                SetAgentState(AgentState.Stay);
                return;
            }
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////



    }

}