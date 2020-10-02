using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : EnemyWave
{


    [SerializeField] protected GameObject boss;
    [SerializeField] protected Vector3 bossSpawnPosition;
    [SerializeField] protected Vector3 bossNewPosition;

    //How long it takes to bring the player ships to the center
    private static float repositionDuration = 250;
    //How long it takes for the boss to approach and the bar starts
    private static float entranceDuration = 250;

    [SerializeField] protected Vector3 playerPositionA;
    [SerializeField] protected Vector3 playerPositionB;

    [SerializeField] protected string bossTrack;

    public override void StartWave(float x, float z, List<Transform> p, float sD, float sC)
    {
        //Start boss track
        FindObjectOfType<AudioManager>().PlayTrack(bossTrack);
        StartCoroutine(BossEntrance());
    }

    public override void UpdateWave()
    {

    }

    IEnumerator BossEntrance ()
    {



        //Disable player inputs during now
        FindObjectOfType<InputManager>().ToggleInputsOff();

        float counter = 0;

        Transform redTransform = FindObjectOfType<AttackShip>().transform;
        Transform blueTransform = FindObjectOfType<DefenseShip>().transform;

        Vector3 origRedPosition = redTransform.position;
        Vector3 origBluePosition = blueTransform.position;
        Vector3 newRedPosition;
        Vector3 newBluePosition;

        if (Vector3.Distance(redTransform.position, playerPositionA) < Vector3.Distance(redTransform.position, playerPositionB))
        {
            newRedPosition = playerPositionA;
            newBluePosition = playerPositionB;
        }
        else
        {
            newRedPosition = playerPositionB;
            newBluePosition = playerPositionA;
        }


        while (counter < repositionDuration)
        {
            redTransform.position = Vector3.Lerp(origRedPosition, newRedPosition, counter / repositionDuration);
            blueTransform.position = Vector3.Lerp(origBluePosition, newBluePosition, counter / repositionDuration);
            counter++;
            yield return new WaitForFixedUpdate();
        }

        redTransform.position = newRedPosition;
        blueTransform.position = newBluePosition;


        counter = 0;

        //While the boss is spawning, we'll also load the boss' health bar at the same time somewhere else
        FindObjectOfType<HUDManager>().LoadBossHealthBar(entranceDuration);

        //Spawn the boss
        GameObject b = Instantiate(boss, bossSpawnPosition, Quaternion.identity);
        b.GetComponent<Enemy>().enabled = false;
        b.GetComponent<EnemyMovement>().enabled = false;
        //b.GetComponent<EnemyAttack>().enabled = false;

        while (counter < entranceDuration)
        {
            b.transform.position = Vector3.Lerp(bossSpawnPosition, bossNewPosition, counter / entranceDuration);
            counter++;
            yield return new WaitForFixedUpdate();
        }


        //Enable player inputs and the boss
        FindObjectOfType<InputManager>().ToggleInputsOn();
        b.GetComponent<Enemy>().enabled = true;
        b.GetComponent<EnemyMovement>().enabled = true;
        //b.GetComponent<EnemyAttack>().enabled = true;



    }


}
