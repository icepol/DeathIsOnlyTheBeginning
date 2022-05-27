using pixelook;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public void OnUnlockAllSkinsPurchaseComplete()
    {
        GameManager.Instance.GameSetup.AreUnlockedAll = true;

        // will reload the skins setup
        GameManager.Instance.Restart();
        
        EventManager.TriggerEvent(Events.PURCHASE_FINISHED);
    }

    public void OnUnlockAllSkinsPurchaseFailed()
    {
        EventManager.TriggerEvent(Events.PURCHASE_FINISHED);
    }
}
