using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        //
        if (coll.name == "Player" && gameObject.name == "FinalPortal")
        {
            GameManager.instance.completeMenuAnim.SetTrigger("Show");
            return;
        }

        //

        if (coll.name == "Player")
        {
            //teleport the player
            GameManager.instance.SaveState();
            string goToScene = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(goToScene);
            //GameManager.instance.player.transform.position= GameObject.Find("SpawnPoint").transform.position;
        }
    }
}
