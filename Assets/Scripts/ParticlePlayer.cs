using UnityEngine;
using TG.Movement;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem landDust = null;
    [SerializeField] float distanceTraveled = 0.7f;

    Mover mover;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    public void PlayDustParticles(bool isGrounded)
    {
        int clampYVelocity = (int)mover.GetYVelocity();
        if (isGrounded && clampYVelocity == 0 && mover.HasYFallPointSet)
        {
            if (Mathf.Abs(mover.GetStartFallPoint() - transform.position.y) > distanceTraveled)
            {
                landDust.Play();
            }
        }
    }
}
