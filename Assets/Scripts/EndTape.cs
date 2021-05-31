using UnityEngine;

public class EndTape : MonoBehaviour
{
    public Animator cam;
    public Animator player;

    public Animator enemy;
    public Transform enemyRot;
    public Transform playerRot;

    public Transform enemyPoint;

    void OnTriggerEnter(Collider other)
    {
        print("Triggered");
        if(other.tag == "Player")
        {
            cam.Play("CamAnim");
            player.Play("winWalk");
            enemy.Play("defeated");
            enemyRot.rotation = Quaternion.Euler(0f, 70f, 0f);
            enemyRot.position = enemyPoint.position;
            playerRot.rotation = Quaternion.Euler(0f, 114.061f, 0f);
            Player.instance.enabled = false;
        }
    }
}
