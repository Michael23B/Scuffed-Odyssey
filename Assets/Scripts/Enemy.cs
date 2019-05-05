using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private float timeBetweenMovementUpdates = 3f;
    private WaitForSeconds coroutineWait;
    private LerpMovement lerpMovement;
    private Rigidbody2D rgb;

    public override void Initialize(int overrideId = 0)
    {
        rgb = GetComponent<Rigidbody2D>();
        lerpMovement = GetComponent<LerpMovement>();
        coroutineWait = new WaitForSeconds(timeBetweenMovementUpdates);
        StartCoroutine(MovementCoroutine());

        Id = overrideId != 0 ? overrideId : GetInstanceID();
    }

    private void Start()
    {
        Initialize();
    }

    public override void HandleDamamge(GameObject bullet)
    {
        health -= bullet.GetComponent<Bullet>().bulletDmg;
        Destroy(bullet);
    }

    public override void Move(float x, float y, bool applyMovementSpeed = true)
    {
        Vector2 newPos = new Vector2(rgb.position.x + x, rgb.position.y + y);
        rgb.MovePosition(newPos);
    }

    public override void StopMoving()
    {
    }

    public static Enemy CreateEnemy(int overrideId = 0)
    {
        GameObject enemyGO = Instantiate(PrefabHelper.Instance.EnemyPrefab);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Initialize(overrideId);

        return enemy;
    }

    IEnumerator MovementCoroutine()
    {
        while (isActiveAndEnabled)
        {
            // Find target
            Vector2 target = GetNewTargetPosition();

            // Move towards target
            lerpMovement.StartMoving(target);

            // Wait for some number of seconds
            yield return coroutineWait;
        }
    }

    private Vector2 GetNewTargetPosition()
    {
        float x = transform.position.x + Random.Range(-1.0f, 1.0f);
        float y = transform.position.y + Random.Range(-1.0f, 1.0f);
        return new Vector2(x, y);
    }
}
