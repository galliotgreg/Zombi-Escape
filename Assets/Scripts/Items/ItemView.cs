using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour {

	private ItemModel item = null;

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
				this.GetComponent<SpriteRenderer>().color = new Color(1,0,0);
				break;
			case ItemModel.ItemType.Bullets:
				this.GetComponent<SpriteRenderer>().color = new Color(0,1,0);
				break;
			case ItemModel.ItemType.Battery: default:
				this.GetComponent<SpriteRenderer>().color = new Color(0,1,1);
				break;
		}
	}

	public void obtainItem()
	{
		Debug.Log( "Animation for the item" );
	}
}
