using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TG.SceneControl
{
    public class SceneReseter : MonoBehaviour
    {
        [SerializeField] Transform[] objectsToReset = null;

        List<Vector3> startingPositions = new List<Vector3>();

        private void Awake() { CreatStartPositionList(); }

        private void CreatStartPositionList()
        {
            foreach (Transform objectToReset in objectsToReset)
            {
                startingPositions.Add(objectToReset.position);
            }
        }

        public void ResetScene()
        {
            int positionIndex = 0;

            foreach (Transform objectToReset in objectsToReset)
            {
                Rigidbody rb = objectToReset.GetComponent<Rigidbody>();

                if(rb != null) { rb.velocity = Vector3.zero; }

                objectToReset.position = startingPositions[positionIndex];

                positionIndex++;
            }
        }
    }
}