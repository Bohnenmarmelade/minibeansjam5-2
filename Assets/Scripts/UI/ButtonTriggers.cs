using UnityEngine;
using Utils;

namespace UI {
    public class ButtonTriggers : MonoBehaviour
    {
        public void OnRestartButton() {
            EventManager.TriggerEvent(Events.SHOW_TITLE, "");
        }

        public void OnStartButton() {
            EventManager.TriggerEvent(Events.START_GAME, "");
        }
    }
}
