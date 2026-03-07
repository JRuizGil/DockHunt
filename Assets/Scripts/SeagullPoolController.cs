using UnityEngine;

public class SpawnPoolController : MonoBehaviour
{
    public SeagullPoolScriptable seagullPoolScriptable;
    public bool isPoolActive;
    public GameObject seagullPrefab;
    private void Awake()
    {
        StartCoroutine(SeagullPool());
    }
    private void Start()
    {
         
    }    
    private System.Collections.IEnumerator SeagullPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject seagullinstance = Instantiate(seagullPrefab, gameObject.transform);
            var seagullAssign = seagullinstance.GetComponent<Seagull>();
            seagullAssign.spawnPoolScriptable = seagullPoolScriptable;
            yield return new WaitForSeconds(seagullPoolScriptable.spawnInterval);
        }  
    }
}
