using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float force = 10f;

    public float fireRate = 5f; // segundos entre disparos para no acumular en el escenario 
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f; // reinicia el timer
        }
    }

    void Shoot()
    {
        GameObject obj = Instantiate(projectile, firePoint.position, Quaternion.identity);

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }
        Destroy(obj, 5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 dir = transform.forward * force;
        Gizmos.DrawLine(transform.position, transform.position + dir);

        Gizmos.color = Color.cyan;

        Vector3 startPos = transform.position;
        Vector3 velocity = transform.forward * force;

        for (int i = 0; i < 30; i++)
        {
            float t = i * 0.1f;

            Vector3 point = startPos +
                            velocity * t +
                            0.5f * Physics.gravity * t * t;

            Gizmos.DrawSphere(point, 0.05f);
        }
    }
}