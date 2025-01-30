using UnityEngine;

public class SpellScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 direction;

    private void Start()
    {
        // Convertir l'angle de rotation en direction
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
        Destroy(gameObject,10);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}