﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

	ItemModel itemModel;
	private DetectGrab detectPlayer = null;
	[SerializeField]
	ItemModel.ItemType type = ItemModel.ItemType.Heal;

	// Use this for initialization
	void Start () {
		// generating item
		this.itemModel = new ItemModel();
		this.itemModel.Itemtype = type;
		this.gameObject.GetComponentInChildren<ItemView>().setItem( this.itemModel );
		// Collision to the player
		this.detectPlayer = this.gameObject.GetComponentInChildren<DetectGrab>();
	}
	
	// Update is called once per frame
	void Update () {
		if( this.detectPlayer.InCollisionPlayer != null ){
			this.activateItem();
		}
	}

	public ItemModel.ItemType getItemType(){
		return this.itemModel.Itemtype;
	}

	public void activateItem()
	{
		this.detectPlayer.InCollisionPlayer.obtainItem( this );
		// Destroy
		GameObject.Destroy( this.gameObject );
	}
}
