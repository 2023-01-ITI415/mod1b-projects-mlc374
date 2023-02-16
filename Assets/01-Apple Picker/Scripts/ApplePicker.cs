using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int      numBaskets      =3;
    public float    basketBottomY  =-14f;
    public float    basketSpacingY  =2f;
    public List<GameObject> basketList;
   
    void Start()
    {
        basketList = new List<GameObject>();
        for (int i=0; i<numBaskets; i++){
            GameObject tBasketGO = Instantiate(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + ( basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }
    
    public void AppleMissed(){
        //destroy all falling apples
        GameObject[] appleArray=GameObject.FindGameObjectsWithTag("Apple");
            foreach (GameObject tempGO in appleArray){
                Destroy (tempGO);
            }
        
        //destroy one of the baskets, get the index of the last basket in basket list
        int basketIndex = basketList.Count -1;
        //get a reference to that basket gameobject
        GameObject basketGO = basketList[basketIndex];
        //remove the basket from the list and destroy the go
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        //if no baskets left, restart game
        if (basketList.Count == 0){
            SceneManager.LoadScene("Main-ApplePicker");
        }
    }

}
