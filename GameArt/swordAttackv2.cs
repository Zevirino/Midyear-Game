using System.Collections;
using UnityEngine;

public class swordAttackv2 : MonoBehaviour
{
    private float time_elapsed;
    public float reset_time;
    public float attack_range;
    public Transform sword_position;
    public LayerMask enemy_types;
    public float damage;
    public GameObject smite_particles;
    public Transform spriteTransform; // Assign the Transform of the sprite in the Inspector
    private static bool isFlipped = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            isFlipped = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            isFlipped = true;
        }
        
        if (time_elapsed <= 0)
        {
            if (Input.GetKeyUp(KeyCode.Z))
            {
                StartCoroutine(RotateSword());
                Instantiate(smite_particles, transform.position, Quaternion.identity);
                print("DEALING DAMAGE");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(sword_position.position, attack_range, enemy_types);
                foreach (Collider2D enemy in enemies)
                {
                    Gross grossComponent = enemy.GetComponent<Gross>();
                    if (grossComponent != null)
                    {
                        grossComponent.takeDamage(damage);
                    }
                }
                time_elapsed = reset_time;
            }
        }
        else
        {
            time_elapsed -= Time.deltaTime;
        }
    }

IEnumerator RotateSword()
{
    float rotationDuration = 0.2f; // Duration for the rotation animation
    float elapsedTime = 0; // Time elapsed since the start of the animation
    float startAngle = transform.eulerAngles.z; // Starting angle
    float endAngle = isFlipped ? startAngle - 180f : startAngle + 180f; // Target angle for the rotation based on isFlipped

    // Rotate towards the target angle
    while (elapsedTime < rotationDuration)
    {
        float angle = Mathf.Lerp(startAngle, endAngle, (elapsedTime / rotationDuration));
        transform.eulerAngles = new Vector3(0, 0, angle);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the rotation exactly reaches the target angle
    transform.eulerAngles = new Vector3(0, 0, endAngle);

    // Wait a moment before rotating back
    yield return new WaitForSeconds(0.1f);

    // Reset elapsed time for the rotation back
    elapsedTime = 0;

    // Rotate back to the original orientation
    while (elapsedTime < rotationDuration)
    {
        float angle = Mathf.Lerp(endAngle, startAngle, (elapsedTime / rotationDuration));
        transform.eulerAngles = new Vector3(0, 0, angle);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the rotation exactly reaches the original orientation
    transform.eulerAngles = new Vector3(0, 0, startAngle);
}



    void OnDrawGizmosSelected()
    {
        if (sword_position != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sword_position.position, attack_range);
        }
    }
}
