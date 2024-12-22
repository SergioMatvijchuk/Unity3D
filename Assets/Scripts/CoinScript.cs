using UnityEngine;

public class CoinScript : MonoBehaviour
{

    private float minOffset   = 100f;  //минимальное расстояние от краёв мира
    private float minDistanse = 50f;  //минимальное расстояние от предыдущего положения 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static int countCoin;


    [SerializeField]
    private TMPro.TextMeshProUGUI coinTMP;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void OnTriggerEnter(Collider other)
	{
		if(other.name == "Character")
        {
            countCoin++;
			coinTMP.text = countCoin.ToString();
			Vector3 newPosition;

            do
            {
                newPosition = this.transform.position + new Vector3(
                       Random.Range(-minDistanse, minDistanse),
                       this.transform.position.y,
                       Random.Range(-minDistanse, minDistanse));
            }
            while (
            Vector3.Distance(newPosition , this.transform.position) < minDistanse
            || newPosition.x < minOffset 
            || newPosition.z < minOffset
			|| newPosition.x > 1000 - minOffset
			|| newPosition.z > 1000 - minOffset
			);

            float terrainHeight = Terrain.activeTerrain.SampleHeight(newPosition);
            newPosition.y =  + terrainHeight + Random.Range(20f, 40f - terrainHeight);
                
			this.transform.position = newPosition;
        }
	}
}
