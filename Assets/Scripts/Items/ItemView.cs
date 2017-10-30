using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour {

	private ItemModel item = null;

	[SerializeField]
	Sprite healSprite = null;		// sprite for Heal Item
	[SerializeField]
	Sprite bulletsSprite = null;	// sprite for Bullets Item
	[SerializeField]
	Sprite batterySprite = null;	// sprite for Battery Item

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setItem( ItemModel item )
	{
		this.item = item;
		this.updateItem ();
	}
	public void updateItem(){
		this.selectImage();
	}
	private void selectImage()
	{
		switch( this.item.Itemtype )
		{
		case ItemModel.ItemType.Heal:
				this.GetComponent<SpriteRenderer> ().sprite = healSprite;
				break;
			case ItemModel.ItemType.Bullets:
				this.GetComponent<SpriteRenderer> ().sprite = bulletsSprite;
				break;
			case ItemModel.ItemType.Battery: default:
				this.GetComponent<SpriteRenderer> ().sprite = batterySprite;
				break;
		}
	}

	public void obtainItem()
	{
		Debug.Log( "Animation for the item" );
	}
}
