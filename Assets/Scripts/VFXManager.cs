using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [SerializeField] GameObject splashParticle;
    [SerializeField] GameObject splashFailParticle;
    [SerializeField] GameObject[] splashPrefabs;

    private void Awake()
    {
        Instance = this;
    }
    public void FailSplash(Vector3 position)
    {
        Instantiate(splashFailParticle, position, Quaternion.Euler(-90, 0, 0));
    }
    public void Splash(Vector3 position, Transform parent)
    {
        //TODO: belki pooling yaparýz...
        Instantiate(splashParticle, position, Quaternion.Euler(-90, 0, 0), parent);
        GameObject splash = Instantiate(splashPrefabs[Random.Range(0, splashPrefabs.Length)], position + new Vector3(0, .01f, 0), Quaternion.Euler(90, 0, 0));
        splash.transform.SetParent(parent, true); //scale quick fix!
        Destroy(splash, 2f);
    }
}
