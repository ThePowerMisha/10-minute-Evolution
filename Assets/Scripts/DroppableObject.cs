using UnityEngine;

public class DroppableObject : MonoBehaviour
{
    [Header("Объект дропа")]
    public Drop[] drops;

    private float dropRandomRange = 1;

    private HeathSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HeathSystem>();

        if (healthSystem != null)
        {
            healthSystem.onCharacterDead.AddListener(DropSomething);
        }
    }

    public void DropSomething()
    {
        foreach (var drop in drops)
        {
            // Проверяем шанс на удачу
            if (Random.Range(0, 101) <= drop.changeToDrop)
            {
                // Получаем количество лута
                int amount = Random.Range(drop.minAmount, drop.maxAmount);

                // Спавним каждый объект лута
                for (int i = 0; i < amount; i++)
                {
                    Vector3 randomize = new Vector3(
                        Random.Range(-dropRandomRange, dropRandomRange),
                        Random.Range(-dropRandomRange, dropRandomRange),
                        Random.Range(-dropRandomRange, dropRandomRange)
                        );

                    Instantiate(drop.prefab, this.transform.position + randomize, Quaternion.identity);
                }
            }
        }
    }
}

[System.Serializable]
public class Drop
{
    [Header("Объект лута")]
    public GameObject prefab;

    [Header("Параметры шанса выпадения")]
    public int changeToDrop;

    [Header("Параметры количества спавна")]
    public int minAmount;
    public int maxAmount;
}