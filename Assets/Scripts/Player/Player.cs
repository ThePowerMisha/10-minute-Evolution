using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Ссылки на скрипты")]
    public HeathSystem heathSystem;
    public SkillsSystem skillsSystem;
    public LocationManager locationManager;
    public PlayerCollisionsChecker playerCollisionsChecker;
    public LvlManager lvlManager;
    public CameraEffects cameraEffects;
    public EscMenu escMenu;

    [Header("Дополнительные объекты")]
    public Rigidbody2D rb;
    public Animator animator;
    public Projectile projectile;

    [Header("Параметры персонажа")]
    public float speed;
    public float cooldown;
    public float damage;
    public int coins;

    [Header("Текст монеток")]
    public Text coinsText;

    private float _timer;
    private Vector2 movement;

    private bool isMoving;

    [Header("Задержка до начала иссушения")]
    public float wastingTimerDelay = 1f;

    [Header("Перезарядка иссушения в секундах")]
    public float wastingCooldown = 1f;

    [Header("Сколько урона наносится")]
    public float wastingDamage = 1f;

    private Coroutine wastingDamageCoroutine;

    private void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        Shoot();
        if(Input.GetKey(KeyCode.Escape)) escMenu.ShowMenu();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if (movement.x == 0 & movement.y == 0)
        {
            if (isMoving)
            {
                isMoving = false;

                wastingDamageCoroutine = StartCoroutine(wastingDamageProcess());
            }
            animator.SetBool("IsIdle", true);
        }
        else
        {
            if (!isMoving)
            {
                cameraEffects.SetDefault();
                
                if (wastingDamageCoroutine != null) StopCoroutine(wastingDamageCoroutine);

                isMoving = true;
            }
            animator.SetBool("IsIdle", false);
        }

        coinsText.text = coins.ToString();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed);
    }

    private void Shoot()
    {
        _timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && _timer > 1 / cooldown)
        {
            projectile.targetTag = "Enemy";
            projectile.minDamage = damage * 0.8f;
            projectile.maxDamage = damage * 1.3f;
            var position = this.transform.position;
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - position).normalized;
            GameObject shoot = Instantiate(projectile.gameObject, position, Quaternion.identity);
            shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            shoot.GetComponent<Rigidbody2D>().velocity = direction * projectile.speed;
            _timer = 0;
        }
    }

    private IEnumerator wastingDamageProcess()
    {
        Debug.Log("Начали таймер иссушения");

        for (float i = 0; i < wastingTimerDelay; i += 0.01f)
        {
            if (isMoving)
            {
                Debug.Log("Начали перемещаться. Останавливаем иссушение!");
                cameraEffects.SetDefault();
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        if (isMoving)
        {
            cameraEffects.SetDefault();
            yield break;
        }
        else
        {
            Debug.Log("Начали иссушать игрока");

            cameraEffects.SetDamaged();

            while (heathSystem.health > 0)
            {
                heathSystem.DealDamage(wastingDamage);

                Debug.Log("Наносим урон иссушением!");

                yield return new WaitForSeconds(wastingCooldown);
            }
        }
    }
}