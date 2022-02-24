using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Levels> levels;
    [HideInInspector]
    public GameObject container;
    public int currenLevelIndex;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.levelManager = this;
        currenLevelIndex = 0;
        loadLevel(currenLevelIndex);
    }

    //create the scene objects from there prefabs contained in the levels list then apply there prespective positions and rotations and scale
    public void loadLevel(int levelIndex)
    {
        
        container = new GameObject();
        container.name = "Level Objects";
        foreach (var Sobject in levels[levelIndex].sceneObjects)
        {
            GameObject currentObject = GameObject.Instantiate(Sobject.scene_GameObject, container.transform);
            currentObject.transform.position = Sobject.sceneObject_transform.position;
            currentObject.transform.rotation = Sobject.sceneObject_transform.rotation;
            currentObject.transform.localScale = Sobject.sceneObject_transform.localScale;
        }
        GameObject.Instantiate(levels[levelIndex].Maze, container.transform);
        GameManager.Instance.startPlayerTurn();
    }

}

[System.Serializable]
public class SceneObject
{
    public GameObject scene_GameObject;
    public Transform sceneObject_transform;
}

[System.Serializable]
public class Levels
{
    public List<SceneObject> sceneObjects;
    public GameObject Maze;
}
