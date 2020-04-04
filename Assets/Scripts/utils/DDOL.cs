using UnityEngine;

namespace Utils {
    public class DDOL : MonoBehaviour
    {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
