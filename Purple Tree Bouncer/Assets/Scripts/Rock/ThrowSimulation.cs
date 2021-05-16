using UnityEngine;
using System.Collections;

public class ThrowSimulation : MonoBehaviour
{
    public Transform Target;
    Vector3 targetSavedPosition;
    public float minFiringAngle = 60.0f;
    public float maxFiringAngle = 30.0f;
    float gravity = 9.8f;
    float firingAngle;
    bool moving;
    bool interrupted;
    Coroutine coroutineReference;
    [TextArea]
    [Tooltip("Comentario")]
    public string Notes = "El proyectil siempre termina en la zona de target, la velocidad se modifica aumentando la gravedad y/o alejando el objetivo. La altura se ajusta modificando el angulo de lanzamiento. En NINGUN momento del calculo se usa RigidBody.";

    public Transform Projectile;
    private Transform origin;

    void Awake()
    {
        origin = transform;
        moving = false;
        interrupted = false;
    }

    public void SetProjectile(Transform _projectile) { Projectile = _projectile; }
    public bool GetMoving() { return moving;}
    public void SetGravity(float _gravity) { gravity = _gravity; }
    public void StopProjectile()
    {
        if(coroutineReference != null)
            StopCoroutine(coroutineReference);
    }
    public void ThrowProjectile(Transform _origin, Transform _target)
    {
        origin = _origin;
        Target = _target;
        targetSavedPosition = Target.position;
        if(coroutineReference != null)
            StopCoroutine(coroutineReference);
        coroutineReference = StartCoroutine(SimulateProjectile());
    }
    public void ThrowProjectile(Transform _origin, Transform _target, float minAngle, float maxAngle)
    {
        origin = _origin;
        Target = _target;
        minFiringAngle = minAngle;
        maxFiringAngle = maxAngle;
        targetSavedPosition = Target.position;
        if (coroutineReference != null)
            StopCoroutine(coroutineReference);
        coroutineReference = StartCoroutine(SimulateProjectile());
    }
    IEnumerator SimulateProjectile()
    {
        if(Projectile != null)
        {
            moving = true;
            targetSavedPosition = Target.position;

            firingAngle = Random.Range(minFiringAngle, maxFiringAngle);

            Projectile.gameObject.SetActive(true);

            if (Projectile.GetComponent<DissappearOnCollider>() != null)
                Projectile.GetComponent<DissappearOnCollider>().CancelDissappear();

            // Move projectile to the position of throwing object + add some offset if needed.
            Projectile.position = origin.position + new Vector3(0, 0.0f, 0);

            // Calculate distance to target
            float target_Distance = Vector3.Distance(Projectile.position, targetSavedPosition);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            // Rotate projectile to face the target.
            Projectile.rotation = Quaternion.LookRotation(targetSavedPosition - Projectile.position);

            float elapse_time = 0;

            while (elapse_time < flightDuration)
            {
                Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
                elapse_time += Time.deltaTime;
                yield return null;
            }
            moving = false;
        }
       else
            Projectile = null;
    }
}