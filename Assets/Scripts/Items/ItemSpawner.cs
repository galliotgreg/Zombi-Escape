using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

	[SerializeField]
	private float timeToNewItemInSeconds = 20;			// Time to create a new item in the spawner
	private float timeCounterInSeconds = 0;				// Time remaining to create a new item

	private GameObject item = null;						// Item associated to the Spawner
	[SerializeField]
	private GameObject itemPrefab = null;				// Item prefab used to instatiate the associated to the Spawner

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// check if there is no item associated
		if (item == null) {
			// Check if the time to create a new one is over
			if (timeCounterInSeconds <= 0) {
				// Create new item
				if (itemPrefab != null) {
					this.item = GameObject.Instantiate (itemPrefab, this.transform);
                    // set random item type
                    //int randomIndex = Random.Range(0, System.Enum.GetNames( typeof(ItemModel.ItemType) ).Length );
                    int randomIndex = randomIndexProba();
                    this.item.GetComponent<ItemBehaviour>().setItemType( (ItemModel.ItemType)randomIndex );
				} else {
					Debug.LogError ( "ItemSpawner : missing item prefab" );
				}
			} else {
				// Update time Counter
				timeCounterInSeconds -= Time.deltaTime;
			}
		} else {
			// if the item is associated, the counter must remains untouched
			timeCounterInSeconds = timeToNewItemInSeconds;
		}
	}

    private int randomIndexProba()
    {
        float rand = Random.Range(0f, 1f);
        int randomIndex;

        // Taux de drop du Heal à 20%
        if (rand <= 0.2)
        {
            randomIndex = 0;
        }
        // Taux de drop de Bullets à 40%
        else if (rand > 0.2 && rand <= 0.6)
        {
            randomIndex = 1;
        }
        else
        // Taux de drop de Battery à 40%
        {
            randomIndex = 2;
        }            
        return randomIndex;
    }
}
