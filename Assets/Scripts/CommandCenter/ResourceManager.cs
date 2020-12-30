using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private int resources;

    [SerializeField]
    private Text resourceText;

    void Start()
    {
        resourceText = GameObject.Find("ResourceText").GetComponent<Text>();
    }

    void Update()
    {
        resourceText.text = resources.ToString();
    }

    public void addResource(int resource)
    {
        Debug.Log("Adding resource: " + resource);
        Interlocked.Add(ref resources, resource);
    }

    public void spendResource(int resource)
    {
        Interlocked.Add(ref resources, resource * -1);
    }

    public int getResources()
    {
        return resources;
    }
}
