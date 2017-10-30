using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel {
	public enum ItemType{ Heal, Bullets, Battery };

	private ItemType itemType;

	public ItemType Itemtype {
		get {
			return itemType;
		}
		set {
			itemType = value;
		}
	}

}
