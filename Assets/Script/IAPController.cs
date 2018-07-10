using UnityEngine;
using System.Collections;

public class IAPController : MonoBehaviour {
	public void purchaseProduct(string purchaseItem){
		GameObject.Find(GameData.gameObject_PluginController).GetComponent<UnityIAPController>().BuyConsumable(purchaseItem);
	}
}
