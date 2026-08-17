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
        public float chanceRandom = 30f;

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
            if (!CubeJump.gameStarted) return;
            // ================================================

            float score = ScoreManager.score;

            if(score > 300)
            {
                speedColider = 15f;
                speedPS = 1.5f;
            }else if(score > 270)
            {
                speedColider = 13f;
                speedPS = 1.3f;
            }
            else if(score > 240)
            {
                speedColider = 12f;
                speedPS = 1.2f;
            }
            else if(score > 210)
            {
                speedColider = 11f;
                speedPS = 1.1f;
            }
            else if(score > 180)
            {
                speedColider = 10f;
                speedPS = 1.0f;
            }
            else if(score > 150)
            {
                speedColider = 9f;
                speedPS = 0.9f;
            }
            else if(score > 120)
            {
                speedColider = 8f;
                speedPS = 0.8f;
            }
            else if(score > 90)
            {
                speedColider = 7f;
                speedPS = 0.7f;
            }
            else if(score > 50)
            {
                speedColider = 6f;
                speedPS = 0.6f;
            }else if(score > 20)
            {
                speedColider = 5f;
                speedPS = 0.5f;
            }
            else
            {
                speedColider = 4f;
                speedPS = 0.4f;
            }


            var main = ps_Tornado.main;
            main.simulationSpeed = speedPS;

            if (Random.Range(0f, 100f) <= chanceRandom)
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