using UnityEngine;
using Utils;

namespace UI {
    public class ButtonTriggers : MonoBehaviour
    {
        public void OnRestartButton() {
            EventManager.TriggerEvent(Events.START_GAME);
        }

        public void OnStartButton() {
            EventManager.TriggerEvent(Events.SHOW_COMIC);
        }
    }
}
