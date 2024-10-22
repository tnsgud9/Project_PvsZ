using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GlobalManager : MonoBehaviour
    {
        public void LoadStage(int i)
        {
            SceneManager.LoadScene($"Stage_{i}");
        }
        public void LoadMainTitle()
        {
            SceneManager.LoadScene($"MainTitle");
        }

        public void LoadCurrentStage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}