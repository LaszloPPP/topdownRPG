using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            //teleport the player
            GameManager.instance.SaveState();
            string goToScene = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(goToScene);
        }
    }
}