using System.Collections;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    [SerializeField] Transform[] childs;
    bool destroyed;

#if UNITY_EDITOR
    private void OnValidate()
    {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }
#endif

    void Update()
    {
        if (destroyed) return;
        if (Ball.Instance.transform.position.y < transform.position.y)
        {
            FallApart();
        }
    }


    public void FallApart()
    {
        foreach (var child in childs)
        {
            child.GetComponent<Collider>().enabled = false;
        }
        destroyed = true;
        transform.SetParent(null);
        StartCoroutine(DestroyCoroutine());
    }
    IEnumerator DestroyCoroutine()
    {
        float speed = 10.5f;
        float gravity = 5.2f;
        float timer = 0;
        while (timer < 5f)
        {
            foreach (Transform child in childs)
            {

                child.position += (Time.deltaTime * gravity * Vector3.down) + (speed * Time.deltaTime * child.right);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }


}
