using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LernUnityAdventure_m23_24
{
    public class AgentJumper
    {
        private NavMeshAgent _navMeshAgent;

        private MonoBehaviour _runner;
        private Coroutine _jumpProcess;
        private float _speed;
        private AnimationCurve _yOffSetCurve;

        public AgentJumper(float speed, NavMeshAgent navMeshAgent, MonoBehaviour runner)
        {
            _navMeshAgent = navMeshAgent;
            _runner = runner;
            _speed = speed;
        }

        public void SetAnimationCurve(AnimationCurve yOffSetCurve)
        {
            _yOffSetCurve = yOffSetCurve;
        }

        public bool InProcess => _jumpProcess != null;

        public void Jump(OffMeshLinkData offMeshLinkData)
        {
            if (InProcess)
                return;

            _jumpProcess = _runner.StartCoroutine(JumpProcess(offMeshLinkData));
        }

        public IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
        {
            Vector3 startPosition = offMeshLinkData.startPos;
            Vector3 endPosition = offMeshLinkData.endPos;

            float duration = Vector3.Distance(startPosition, endPosition) / _speed;

            float progress = 0;

            while (progress < duration)
            {
                float yOffset = _yOffSetCurve.Evaluate(progress / duration);
                _navMeshAgent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration) + Vector3.up * yOffset;
                progress += Time.deltaTime;
                yield return null;
            }

            _navMeshAgent.CompleteOffMeshLink();
            _jumpProcess = null;
        }
    }
}
