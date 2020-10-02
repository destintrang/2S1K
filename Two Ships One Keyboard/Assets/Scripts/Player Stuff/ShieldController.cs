using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{


    [SerializeField] protected Transform shield;

    [SerializeField] protected Transform waveA;
    [SerializeField] protected Transform waveB;


    [SerializeField] protected float shieldX;
    [SerializeField] protected float shieldY;
    [SerializeField] protected float shieldZ;
    [SerializeField] protected Vector3 upgradedShield;

    //How big the wave will be
    [SerializeField] protected float waveScale;
    [SerializeField] protected float upgradedWaveScale;
    //How fast the shockwave is animated
    [SerializeField] protected float waveTime;
    //Delay between waves
    [SerializeField] protected float waveDelay;





    //STUFF FOR CHANGING THE SIZE OF THE SHIELD AND TOGGLING IT
    Coroutine shieldAnimation = null;
    public void DeployShield (float time)
    {
        if (shieldAnimation != null) { StopCoroutine(shieldAnimation); }
        shieldAnimation = StartCoroutine(ShieldOn(time));
    }

    IEnumerator ShieldOn (float time)
    {
        shield.localScale = new Vector3(0, 0, shield.localScale.z);
        float counter = 0;
        shield.gameObject.SetActive(true);

        while (counter < time)
        {
            shield.localScale = new Vector3(Mathf.Lerp(shield.localScale.x, shieldX, counter/time), Mathf.Lerp(shield.localScale.y, shieldY, counter / time), Mathf.Lerp(shield.localScale.z, shieldZ, counter / time));
            counter++;
            yield return new WaitForFixedUpdate();
        }

        shield.localScale = new Vector3(shieldX, shieldY, shield.localScale.z);

    }

    public void EndShield (float time)
    {
        if (shieldAnimation != null) { StopCoroutine(shieldAnimation); }
        shieldAnimation = StartCoroutine(ShieldOff(time));
    }

    IEnumerator ShieldOff(float time)
    {

        float counter = 0;

        while (counter < time)
        {
            shield.localScale = new Vector3(Mathf.Lerp(shield.localScale.x, 0, counter / time), Mathf.Lerp(shield.localScale.y, 0, counter / time), Mathf.Lerp(shield.localScale.z, 0, counter / time));
            counter++;
            yield return new WaitForFixedUpdate();
        }

        shield.localScale = new Vector3(0, 0, shield.localScale.z);
        shield.gameObject.SetActive(false);

    }



    //STUFF FOR USING THE SHOCKWAVE
    public void Discharge ()
    {

        //Reset the shield
        shield.localScale = new Vector3(0, 0, shield.localScale.z);
        shield.gameObject.SetActive(false);

        //Play the discharge SFX
        FindObjectOfType<AudioManager>().PlaySoundEffect("Discharge");

        StartCoroutine(Shockwave());
    }
    IEnumerator Shockwave ()
    {

        StartCoroutine(AnimateWave(waveA));

        yield return new WaitForSeconds(waveDelay);

        StartCoroutine(AnimateWave(waveB));

    }
    IEnumerator AnimateWave (Transform wave)
    {
        wave.localScale = Vector3.zero;
        float counter = 0;
        wave.gameObject.SetActive(true);

        while (counter < waveTime)
        {
            wave.localScale = new Vector3(Mathf.Lerp(0, waveScale, counter / waveTime), Mathf.Lerp(1, 0, counter / waveTime), Mathf.Lerp(0, waveScale, counter / waveTime));
            counter++;
            yield return new WaitForFixedUpdate();
        }

        wave.localScale = new Vector3(waveScale, 0, waveScale);
        wave.gameObject.SetActive(false);
    }


    public void UpgradeShield ()
    {
        shieldX = upgradedShield.x;
        shieldY = upgradedShield.y;
        shieldZ = upgradedShield.z;
    }
    public void UpgradeDischarge ()
    {
        waveScale = upgradedWaveScale;
    }
    


}
