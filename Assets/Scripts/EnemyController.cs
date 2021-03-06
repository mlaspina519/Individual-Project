using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {
	private Rigidbody2D rigid;
	public int health = 3;
	public Vector2 movement = new Vector2(300, 100);
    public GameObject bulletBill;

	public int Health { get { return health; } }

	// Initialization
	void Start() {
		rigid = GetComponent<Rigidbody2D>();
        InvokeRepeating("ShootBullet", 0.5f, 1.0f);
	}

	// Physics - fixed rate
	void FixedUpdate() {
		rigid.velocity = movement * Time.fixedDeltaTime;
	}

	// On collision with wall
	void OnTriggerEnter2D(Collider2D other) {

        // When bouncing off a wall, randomly changes speed
		if(other.CompareTag("Vertical Wall")) {
			movement = new Vector2 (-movement.x, movement.y);
			transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
		} else if(other.CompareTag("Horizontal Wall")) {
			movement = new Vector2 (movement.x, -movement.y);
		}
	}

    // Reduces health
	public void Hit() {
		health--;
	}

    // Fires bullet
    void ShootBullet() {
        if(movement.x > 0) {
            GameObject bullet = (GameObject)Instantiate (bulletBill, transform.position, Quaternion.identity);
            bullet.transform.localScale = new Vector2(-bullet.transform.localScale.x, bullet.transform.localScale.y);
            bullet.GetComponent<EnemyBullet>().ChangeDirection();
        } else {
            Instantiate(bulletBill, transform.position, Quaternion.identity);
        }
    }
}