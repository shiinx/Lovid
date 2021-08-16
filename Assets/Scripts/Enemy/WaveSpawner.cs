using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// yt you should sleep more
public class WaveSpawner : MonoBehaviour
{
    //spawn enemies in set time intervals(waves)
    //spawn enemies according to specified location
    //spawn enemies according to game event called
    public WaveConstants waveConstants1;
    public WaveConstants waveConstants2;
    public WaveConstants waveConstants3;
    public GameObject freshieEnemy;
    public GameObject epdEnemy;
    public GameObject istdEnemy;
    public GameStateVariable currentGameState;
    public float spawnPointX;
    public float spawnPointY;
    private int f1;
    private float f11;
    private float f1s;
    private int i1;
    private float i11;
    private float i1s;
    private int e1;
    private float e11;
    private float e1s;
    private int f2;
    private float f21;
    private float f2s;
    private int i2;
    private float i21;
    private float i2s;
    private int e2;
    private float e21;
    private float e2s;
    private int f3;
    private float f31;
    private float f3s;
    private int i3;
    private float i31;
    private float i3s;
    private int e3;
    private float e31;
    private float e3s;
    private Vector2 spawnPoint;
    void Start()
    {
        f1 = waveConstants1.totalNumberOfFreshie;
        f11 = waveConstants1.firstFreshie;
        f1s = waveConstants1.freshieInterval;
        i1 = waveConstants1.totalNumberOfISTD;
        i11 = waveConstants1.firstISTD;
        i1s = waveConstants1.istdInterval;
        e1 = waveConstants1.totalNumberOfEPD;
        e11 = waveConstants1.firstEPD;
        e1s = waveConstants1.epdInterval;
        f2 = waveConstants2.totalNumberOfFreshie;
        f21 = waveConstants2.firstFreshie;
        f2s = waveConstants2.freshieInterval;
        i2 = waveConstants2.totalNumberOfISTD;
        i21 = waveConstants2.firstISTD;
        i2s = waveConstants2.istdInterval;
        e2 = waveConstants2.totalNumberOfEPD;
        e21 = waveConstants2.firstEPD;
        e2s = waveConstants2.epdInterval;
        f3 = waveConstants3.totalNumberOfFreshie;
        f31 = waveConstants3.firstFreshie;
        f3s = waveConstants3.freshieInterval;
        i3 = waveConstants3.totalNumberOfISTD;
        f31 = waveConstants3.firstISTD;
        f3s = waveConstants3.istdInterval;
        e3 = waveConstants3.totalNumberOfEPD;
        e31 = waveConstants3.firstEPD;
        e3s = waveConstants3.epdInterval;
        spawnPoint = new Vector2(spawnPointX, spawnPointY);
    }
    // Update is called once per frame
    public void Update()
    {
        if (currentGameState.Value == GameConstants.GameState.Level1Wave1)
        {
            if (f11 <= 0f)
            {
                StartCoroutine(SpawnFreshie());
                f11 = f1s;
            }
            f11 -= Time.deltaTime;
            if (i11 <= 0f)
            {
                StartCoroutine(SpawnISTD());
                i11 = i1s;
            }
            i11 -= Time.deltaTime;
            if (e11 <= 0f)
            {
                StartCoroutine(SpawnEPD());
                e11 = e1s;
            }
            e11 -= Time.deltaTime;
        }
        if (currentGameState.Value == GameConstants.GameState.Level1Wave2)
        {
            if (f21 <= 0f)
            {
                StartCoroutine(SpawnFreshie2());
                f21 = f2s;
            }
            f21 -= Time.deltaTime;
            if (i21 <= 0f)
            {
                StartCoroutine(SpawnISTD2());
                i21 = i2s;
            }
            i21 -= Time.deltaTime;
            if (e21 <= 0f)
            {
                StartCoroutine(SpawnEPD2());
                e21 = e2s;
            }
            e21 -= Time.deltaTime;
        }
        if (currentGameState.Value == GameConstants.GameState.Level1Wave3)
        {
            if (f31 <= 0f)
            {
                StartCoroutine(SpawnFreshie3());
                f31 = f3s;
            }
            f31 -= Time.deltaTime;
            if (i31 <= 0f)
            {
                StartCoroutine(SpawnISTD3());
                i31 = i3s;
            }
            i31 -= Time.deltaTime;
            if (e31 <= 0f)
            {
                StartCoroutine(SpawnEPD3());
                e31 = e3s;
            }
            e31 -= Time.deltaTime;
        }
    }

    

    void SpawnWaveL1W1()
    {
        StartCoroutine(SpawnFreshie());
        StartCoroutine(SpawnISTD());
        StartCoroutine(SpawnEPD());
    }
    IEnumerator SpawnFreshie()
    {
        if (f1>0)
        {
            Instantiate(freshieEnemy, spawnPoint, Quaternion.identity);
            f1--;
            Debug.Log($"Freshies left:{f1}");
            yield return null;
        }
    }
    IEnumerator SpawnISTD()
    {
        if (i1>0)
        {
            Instantiate(istdEnemy, spawnPoint, Quaternion.identity);
            i1--;
            Debug.Log($"ISTD left:{i1}");
            yield return null;
        }
    }
    IEnumerator SpawnEPD()
    {
        if (e1 > 0)
        {
            Instantiate(epdEnemy, spawnPoint, Quaternion.identity);
            e1--;
            Debug.Log($"EPD left:{e1}");
            yield return null;
        }
    }
    

    void SpawnWaveL1W2()
    {
        StartCoroutine(SpawnFreshie2());
        StartCoroutine(SpawnISTD2());
        StartCoroutine(SpawnEPD2());
    }
    IEnumerator SpawnFreshie2()
    {
        if (f2 > 0)
        {
            Instantiate(freshieEnemy, spawnPoint, Quaternion.identity);
            f2--;
            Debug.Log($"Freshies left:{f2}");
            yield return null;
        }
    }
    IEnumerator SpawnISTD2()
    {
        if (i2 > 0)
        {
            Instantiate(istdEnemy, spawnPoint, Quaternion.identity);
            i2--;
            Debug.Log($"ISTD left:{i2}");
            yield return null;
        }
    }
    IEnumerator SpawnEPD2()
    {
        if (e2 > 0)
        {
            Instantiate(epdEnemy, spawnPoint, Quaternion.identity);
            e2--;
            Debug.Log($"EPD left:{e2}");
            yield return null;
        }
    }


    void SpawnWaveL1W3()
    {
        StartCoroutine(SpawnFreshie3());
        StartCoroutine(SpawnISTD3());
        StartCoroutine(SpawnEPD3());
    }
    IEnumerator SpawnFreshie3()
    {
        if (f3 > 0)
        {
            Instantiate(freshieEnemy, spawnPoint, Quaternion.identity);
            f3--;
            Debug.Log($"Freshies left:{f3}");
            yield return null;
        }
    }
    IEnumerator SpawnISTD3()
    {
        if (i3 > 0)
        {
            Instantiate(istdEnemy, spawnPoint, Quaternion.identity);
            i3--;
            Debug.Log($"ISTD left:{i3}");
            yield return null;
        }
    }
    IEnumerator SpawnEPD3()
    {
        if (e3 > 0)
        {
            Instantiate(epdEnemy, spawnPoint, Quaternion.identity);
            e3--;
            Debug.Log($"EPD left:{e3}");
            yield return null;
        }
    }
}
