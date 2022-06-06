
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public SceneFader fader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(string levelName){
        fader.FadeTo(levelName);
    }
}
