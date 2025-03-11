using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerController : Figure
{
    public Animator anim;
    public Data_Infor data_Infor;
    public levelUpConfig levelUpConfig;
    private int countEnemies;
    private int countEnemiesIsKill;
    private int currentEXP = 0;
    public int expNeed;
    private int level;
    public static PlayerController instance { private set; get; }

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
        level = 1;

    }
    void Update()
    {
        MovePlayer();
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
    public void MovePlayer()
    {
        var direction = Vector3.zero;
        float moveSpeed = 5f; 
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
            nextPosition = transform.position + direction.normalized * moveSpeed * Time.deltaTime;

            var bgSize = BgController.instance.GetSizeBg();
            float xLimit = bgSize.x / 2 - 0.3f;
            float yLimit = bgSize.y / 2 - 0.4f;

            nextPosition.x = Mathf.Clamp(nextPosition.x, -xLimit, xLimit);
            nextPosition.y = Mathf.Clamp(nextPosition.y, -yLimit, yLimit);

            transform.position = nextPosition;
        }

        anim.SetFloat("Move", Mathf.Abs(direction.magnitude));
    }
    void PlayerUpEXP(int e)
    {
        currentEXP += e;
        if (currentEXP >= expNeed)
        {
            currentEXP = 0;
            level++;
            this.PostEvent(EventID.PlayerUpLevel,level);
        }
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

        /*DataAccountPlayer.PlayerInfor.expPlayer++;
        DataAccountPlayer.SaveDataPlayerInfor();*/
        this.PostEvent(EventID.PlayerUpScore);
    }
    private void SetDataPlayer(Data_object dO)
    {
        this.damage = dO.damage;
        this.hp = dO.hp;
        this.speed = dO.speed;
    }
    private void SetEXPPlayer(LevelPlayer levelPlayer)
    {
        expNeed = levelPlayer.expNeed;
    }

}
