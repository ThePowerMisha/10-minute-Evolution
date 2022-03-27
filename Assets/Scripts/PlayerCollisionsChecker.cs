using UnityEngine;

public class PlayerCollisionsChecker : MonoBehaviour
{
    [Header("Ссылка на скрипт игрока")]
    public Player player;

    [Header("Ссылка на Transform игрока")]
    public Transform playerTransform;

    [Header("Основной коллайдер магнита")]
    public CircleCollider2D magnetCollider;

    [Header("Скорость притяжения вещей")]
    public float magnetSpeed = 15;

    [Header("Радиус магнита")]
    public float magnetPower = 0.15f;

    [Header("Радиус сбора предметов игроком")]
    public float itemsCollectRange = 0.2f;

    public void AddMagnetSpeed(float value)
    {
        magnetSpeed += value;
    }

    public void AddMagnetPower(float value)
    {
        magnetPower += value;

        magnetCollider.radius = magnetPower;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Drop") ||
            collision.CompareTag("Coins") ||
            collision.CompareTag("Skill") ||
            collision.CompareTag("Exp") ){

            Vector3 delta = (collision.transform.position - playerTransform.transform.position).normalized;

            collision.transform.position -= delta * Time.deltaTime * magnetSpeed;

            // Тут проверяем все предметы с которыми мы столкнулись
            if (Vector3.Distance(collision.transform.position, playerTransform.transform.position) <= itemsCollectRange)
            {
                if (collision.CompareTag("Coins"))
                {
                    player.coins++;
                    
                    Destroy(collision.gameObject);
                }

                if (collision.CompareTag("Skill"))
                {
                    int skillID = player.locationManager.GetRandomSkill();

                    player.skillsSystem.GainExpToSkill(skillID, 1);

                    Destroy(collision.gameObject);
                }

                if (collision.CompareTag("Exp"))
                {
                    // Повышаем уровень персонажа
                    player.lvlManager.AddExp(1);

                    Destroy(collision.gameObject);
                }
            }
        }
    }
}