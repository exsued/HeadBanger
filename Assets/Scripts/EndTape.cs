using System.Collections;
using UnityEngine;

public class EndTape : MonoBehaviour
{
    public Animator cam;
    public Animator player;

    public Animator enemy;

    public Transform point;
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
            StartCoroutine(LerpPosition());
            enemyRot.rotation = Quaternion.Euler(0f, 70f, 0f);
            enemyRot.position = enemyPoint.position;
            playerRot.rotation = Quaternion.Euler(0f, 114.061f, 0f);
            Player.instance.enabled = false;
        }
    }
    IEnumerator LerpPosition()
    {
        enemy.Play("walk");

        while (Vector3.Distance(enemy.transform.position, point.position) > 0.5f)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, point.position, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        enemy.Play("defeated");
        yield return new WaitForSeconds(2f);
        Player.instance.OnGameProceed();
    }
}
