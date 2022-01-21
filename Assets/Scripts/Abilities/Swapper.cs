using UnityEngine;
using TG.Movement;

namespace TG.Abilities
{
    public class Swapper : MonoBehaviour
    {
        [SerializeField] Mover3D mover3D = null;
        [SerializeField] Mover2D mover2D = null;

        bool use3DMovement = true;

        public Mover GetActiveMover()
        {
            if (mover3D.IsRigidbodyEnabled()) { return mover3D; }
            return mover2D;
        }

        public void Swap()
        {
            use3DMovement = !use3DMovement;

            SetCorrectMoverBehavior();
            SetMoverParent();
        }

        private void SetCorrectMoverBehavior()
        {
            mover3D.SetBehaviorOnMovement(use3DMovement);
            mover2D.SetBehaviorOnMovement(use3DMovement);
        }

        private void SetMoverParent()
        {
            mover3D.transform.parent = null;
            mover2D.transform.parent = null;

            mover3D.transform.parent = use3DMovement ? transform : mover2D.transform;
            mover2D.transform.parent = use3DMovement ? mover3D.transform : transform;
        }
    }
}