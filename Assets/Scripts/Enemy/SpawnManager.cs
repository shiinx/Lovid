using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEV : MonoBehaviour {
    public EnemyConstants enemyConstants;
    public IntVariable buildPhase;

    private void Start() {
        new WaitForSeconds(buildPhase.value);
        Debug.Log("Spawnmanager start");
        for (var j = 0; j < 10; j++)
            spawnFromPooler(ObjectType.freshie);
    }

    private void startSpawn(Scene scene, LoadSceneMode mode) {
        for (var j = 0; j < 10; j++)
            spawnFromPooler(ObjectType.freshie);
    }


    private void spawnFromPooler(ObjectType i) {
        var item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null) {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f),
                enemyConstants.groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy() {
        var i = ObjectType.freshie;
        spawnFromPooler(i);
    }
}