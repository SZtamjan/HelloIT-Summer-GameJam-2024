using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class ChangeScene : MonoBehaviour
    {
        public void LoadScene(int index)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(index);
        }
    }
}