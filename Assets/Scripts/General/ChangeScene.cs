using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class ChangeScene : MonoBehaviour
    {
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}