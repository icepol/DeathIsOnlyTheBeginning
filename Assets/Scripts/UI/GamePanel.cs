using UnityEngine;
using UnityEngine.UI;

namespace pixelook
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject introPanel;
        [SerializeField] private Text soulsSaved;

        private void Awake()
        {
            EventManager.AddListener(Events.GAME_STATE_CHANGED, OnGameStateChanged);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.GAME_STATE_CHANGED, OnGameStateChanged);
        }

        void OnGameStateChanged()
        {
            soulsSaved.text = $"Souls saved: {GameState.SoulsSaved}";
        }

        public void Disable()
        {
            introPanel.SetActive(false);
        }
    }
}