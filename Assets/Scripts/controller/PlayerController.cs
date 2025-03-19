using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Character
{
    public Animator anim;
    public Data_Infor data_Infor;
    public levelUpConfig levelUpConfig;
    private int countEnemies;
    private int countEnemiesIsKill;
    private int currentEXP = 0;
    public int expNeed;
    private int level;
    private float MAXHP;
    private float MAXSpeed = 12;
    private int MAXEXP;
    public static PlayerController instance;
    public int gold;
    public GameObject shieldEffect;

    private void Awake()
    {
        instance = this;
        data_Infor = Resources.Load<Data_Infor>("CSV_Data/Data_Infor");
        levelUpConfig = Resources.Load<levelUpConfig>("CSV_Data/levelUpConfig");

        this.RegisterListener(EventID.CountEnemy, (sender, param) => SetCountEnemies((int)param));
        this.RegisterListener(EventID.EnemyDie, (sender, param) => PlayerUpScore((int)param));
    }
    private void Start()
    {
        gold = 0;
        level = 1;
        currentEXP = 0;
        MAXEXP = expNeed;
        MAXHP = hp; 
        
        UIController.instance.FillHpPlayer(hp, MAXHP);
        UIController.instance.FillExpPlayer(currentEXP, MAXEXP);


    }
    void Update()
    {
        MovePlayer(this.speed);
        ItemFactory.CheckItem(this.transform);
    }
    public void InitInforPlayer(int level)
    {
        if(data_Infor == null)
        {
            data_Infor = Resources.Load<Data_Infor>("CSV_Data/Data_Infor");
        }

        if (levelUpConfig == null)
        {
            levelUpConfig = Resources.Load<levelUpConfig>("CSV_Data/levelUpConfig");
        }

        var temp = data_Infor.GetInforPlayerByLevel(level);
        var temp2 = levelUpConfig.GetExpPlayerByLevel(level);
        SetDataPlayer(temp);
        SetEXPPlayer(temp2);
    }
    public void SetCountEnemies(int cntE)
    {
        countEnemies = cntE;
    }
    public void MovePlayer(float speed)
    {
        var direction = Vector3.zero;
        Vector3 nextPosition = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (direction != Vector3.zero)
        {
            nextPosition = transform.position + direction.normalized * speed * Time.deltaTime;

            var bgSize = BgController.instance.GetSizeBg();
            float xLimit = bgSize.x / 2 - 0.3f;
            float yLimit = bgSize.y / 2 - 0.4f;

            nextPosition.x = Mathf.Clamp(nextPosition.x, -xLimit, xLimit);
            nextPosition.y = Mathf.Clamp(nextPosition.y, -yLimit, yLimit);

            transform.position = nextPosition;
        }

        anim.SetFloat("Move", Mathf.Abs(direction.magnitude));
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UIController.instance.FillHpPlayer(hp, MAXHP);
        if (hp <= 0)
        {
            this.PostEvent(EventID.PlayerDie);
        }

    }
    void PlayerUpEXP(int e)
    {
        currentEXP += e;
        
        if (currentEXP >= expNeed)
        {
            currentEXP = 0; 
            UIController.instance.FillExpPlayer(currentEXP, MAXEXP);
            level++;
            this.PostEvent(EventID.PlayerUpLevel,level);
        }
        UIController.instance.FillExpPlayer(currentEXP, MAXEXP);
        this.PostEvent(EventID.PlayerUpEXP, currentEXP);
    }
    void PlayerUpScore(int e)
    {
        PlayerUpEXP(e);
        countEnemiesIsKill++;
        if (countEnemiesIsKill == countEnemies)
        {
            this.PostEvent(EventID.OutOffEnemy, countEnemiesIsKill);
            countEnemiesIsKill = 0;
        }

        
        this.PostEvent(EventID.PlayerUpScore);
    }
    public void TakeCoin()
    {
        gold+= 5;
    }
    public void UpHp()
    {
        hp += 20;
        if (hp > MAXHP)
        {
            hp = MAXHP;
        }
        UIController.instance.FillHpPlayer(hp, MAXHP);

    }
    public void SpeedUp()
    {
        var oldSpeed = speed;
        speed = MAXSpeed;
        StartCoroutine(BackToOldSpeed(oldSpeed));
    }
    public void Shield()
    {

        var rig = this.gameObject.GetComponent<Rigidbody2D>();
        if (rig == null)
        {
            return;
        }

        if (shieldEffect == null)
        {
            return;
        }

        shieldEffect.SetActive(true);
        rig.GetComponent<Collider2D>().enabled = false; 

        StartCoroutine(TurnOffShieldEffect());
        StartCoroutine(BackToTakeDamage());
    }

    IEnumerator BackToTakeDamage()
    {
        yield return new WaitForSeconds(5);

        var rig = this.gameObject.GetComponent<Rigidbody2D>();
        if (rig != null)
        {
            rig.GetComponent<Collider2D>().enabled = true; 
        }
    }

    IEnumerator TurnOffShieldEffect()
    {
        yield return new WaitForSeconds(5);

        if (shieldEffect != null)
        {
            shieldEffect.SetActive(false);
        }
    }

    IEnumerator BackToOldSpeed(float oldSpeed)
    {
        yield return new WaitForSeconds(5);
        speed = oldSpeed;
    }
    
    private void SetDataPlayer(Data_object dO)
    {
        this.damage = dO.damage;
        this.hp = dO.hp;
        this.speed = dO.speed;
        MAXHP = dO.hp;
       
    }
    private void SetEXPPlayer(LevelPlayer levelPlayer)
    {
        expNeed = levelPlayer.expNeed;
    }

}
