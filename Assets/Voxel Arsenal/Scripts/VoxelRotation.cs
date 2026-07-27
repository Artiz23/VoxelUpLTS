using UnityEngine;
using System.Collections;

namespace VoxelArsenal
{
    public class VoxelRotation : MonoBehaviour
    {
        public ParticleSystem ps_Tornado;
        public float speedPS;
        public float speedColider;
        public float high;

        public Transform tornadoCollider;

        [Header("Rotate axises by degrees per second")]
        public Vector3 rotateVector = Vector3.zero;

        public enum spaceEnum { Local, World };
        public spaceEnum rotateSpace;

        private Vector3 targetPosition;
        private bool hasReachedTop = false;
        private bool shouldLift = false;

        private void Start()
        {
            speedPS = 0.4f;

            var main = ps_Tornado.main;
            main.simulationSpeed = speedPS;

            if (Random.Range(0f, 100f) <= 30f)
            {
                ps_Tornado.Play();
                shouldLift = true;

                targetPosition = new Vector3(
                    tornadoCollider.position.x,
                    tornadoCollider.position.y + high,
                    tornadoCollider.position.z
                );
            }
        }

        private void Update()
        {
            if (rotateSpace == spaceEnum.Local)
                transform.Rotate(rotateVector * Time.deltaTime);
            if (rotateSpace == spaceEnum.World)
                transform.Rotate(rotateVector * Time.deltaTime, Space.World);

            if (shouldLift && !hasReachedTop)
            {
                Vector3 pos = tornadoCollider.position;
                pos.y += speedColider * Time.deltaTime;

                if (pos.y >= targetPosition.y)
                {
                    pos.y = targetPosition.y;
                    hasReachedTop = true;
                }

                tornadoCollider.position = pos;
            }
        }
    }
}