using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject circleUnder;

    public VariableJoystick joystick;
    public Canvas inputCanva;
    private Vector3 m_director = new Vector3();
    private Quaternion curentCircle;
    private UserData playerData;

    //status: 0 -> dont buy; 1 -> bought; 2 -> using
    public static List<int> listWP = new List<int> {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public static List<int> listHead = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0};
    public static List<int> listPant = new List<int> { 1, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static List<int> listSkin = new List<int> {0, 0, 0, 0};

    //amout of coin player has
    public static int currentCoins = Constant.DEFAULT_COIN;



    private bool isJoystick;

    public VariableJoystick Joystick { get => joystick; set => joystick = value; }
    public Canvas InputCanva { get => inputCanva; set => inputCanva = value; }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetInRange();
        Move();
    }

    public override void OnInit()
    {
        base.OnInit();
        CharacterTeam = Constant.PLAYER_TEAM;
        speed = 5f;
        curentCircle = circleUnder.transform.localRotation;
        LoadDataForPlayer();
        CreateWepon(CharacterWpType);
        CreatePant(characterPantType);
        //Debug.Log(characterPantType);
        EnableJoystick();
    }

    //init data for player when start game
    private void LoadDataForPlayer()
    {
        playerData = new UserData(currentCoins, listWP, listHead, listPant, listSkin);
        DataManager.SaveData(playerData);
        CharacterWpType = SetWpForPlayer(DataManager.LoadData());
        characterPantType = SetPantForPlayer(DataManager.LoadData());
        characterHeadType = SetHeadForPlayer(DataManager.LoadData());
    }

    //set data for wepon to spawn
    private EWeponType.WeponType SetWpForPlayer(UserData playerData)
    {
        for(int i = 0; i < playerData.listStatusWp.Count; i++)
        {
            if (playerData.listStatusWp[i] == 1)
            {
                return listWP_SO.listWepon[i].weponType;
            }
        }
        return EWeponType.WeponType.NONE;
    }

    //set data for pant to spawn
    private EPantsType.pantsType SetPantForPlayer(UserData playerData)
    {
        for(int i = 0; i < playerData.listStatusPant.Count; i++)
        {
            if (playerData.listStatusPant[i] == 1)
            {
                return listPant_SO.listPants[i].pantType;
            }
        }
        return EPantsType.pantsType.NONE;
    }

    private EHeadType.headType SetHeadForPlayer(UserData playerData)
    {
        for (int i = 0; i < playerData.listStatusHead.Count; i++)
        {
            if (playerData.listStatusHead[i] == 1)
            {
                return listHead_SO.listHeads[i].headType;
            }
        }
        return EHeadType.headType.NONE;
    }

    private void EnableJoystick()
    {
        isJoystick = true;
        InputCanva.gameObject.SetActive(true);
    }

    private void Move()
    {
        if (isJoystick)
        {
            if(Joystick.Direction == Vector2.zero)
            {
                rb.velocity = Vector3.zero;
                if (Target != null)
                {
                    SpawBulletPosition.position = transform.position + new Vector3(0, 1f, 0);
                    Throw();
                }
                else
                {
                    ChangeAnim(Constant.IDLE);
                }               
            }
            else
            {
                ChangeAnim(Constant.RUN);
                m_director = new Vector3(Joystick.Direction.x, 0f, Joystick.Direction.y);
                rb.velocity = m_director * speed;

                if (m_director.x != 0 || m_director.y != 0)
                {
                    transform.rotation = Quaternion.LookRotation(rb.velocity);
                }
            }
            
        }

        circleUnder.transform.rotation = curentCircle;

    }

    public void SetJoystick(Canvas input, VariableJoystick joystick)
    {
        this.joystick = joystick;
        this.inputCanva = input;
    }
}
