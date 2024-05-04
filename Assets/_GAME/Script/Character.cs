using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SphereCollider colliderCheckTarget;
    [SerializeField] private Wepon currentWepon;
    [SerializeField] private EWeponType.WeponType characterWpType;
    [SerializeField] private Transform weponTranform;
    [SerializeField] private String characterTeam;
    [SerializeField] private GameObject pant;

    [SerializeField] protected WeponListSO listWP_SO;
    [SerializeField] protected float radiusToAttack;
    [SerializeField] protected EPantsType.pantsType characterPantType;
    [SerializeField] protected ShopPantsButtonSO listPant_SO;
    [SerializeField] protected EHeadType.headType characterHeadType;
    [SerializeField] protected ShopHeadButtonSO listHead_SO;
    [SerializeField] protected LayerMask characterLayer;

    private Transform spawBulletPosition;
    private string currentAnim = Constant.IDLE;

    //private List<Character> listCheckInRange = new List<Character>();
    private Character target;

    //protected Vector3 targetDirector = new Vector3();
    [SerializeField] private bool isDead;

    public static Action<Character> OnLevelDespaw;

    public Character Target { get => target; set => target = value; }
    //public List<Character> ListCheckInRange { get => listCheckInRange; set => listCheckInRange = value; }
    public string CharacterTeam { get => characterTeam; set => characterTeam = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public EWeponType.WeponType CharacterWpType { get => characterWpType; set => characterWpType = value; }
    public Transform SpawBulletPosition { get => spawBulletPosition; set => spawBulletPosition = value; }
    public Wepon CurrentWepon { get => currentWepon; set => currentWepon = value; }


    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnInit()
    {
        IsDead = false;
    }

    public virtual void OnDeSpaw(Character character)
    {
        character.ChangeAnim(Constant.DEAD);
        OnLevelDespaw?.Invoke(character);
    }

    protected void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }

    }

    //function attack
    protected void Throw()
    {
        if(Target != null)
        {
            transform.LookAt(Target.transform.position);
            ChangeAnim(Constant.ATTACK);

            CurrentWepon.Throw(this, SetDirector(), SpawBulletPosition, OnHitTarget);

            //CurrentWepon.gameObject.SetActive(false);

        }
    }

    //logic when hit by bullet
    private void OnHitTarget(Character character, Character victim)
    {
        //CurrentWepon.gameObject.SetActive(true);

        if (character != victim || character == null)
        {
            victim.IsDead = true;
            victim.OnDeSpaw(victim);
            //target.OnDeSpaw();
            CurrentWepon.CancelBullet();
            Target = null;
        }

    }

    protected void CheckTargetInRange()
    {
        Collider[] listColliderEnemy = Physics.OverlapSphere(transform.position, radiusToAttack, characterLayer);
        float nearest = Mathf.Infinity;
        foreach (Collider collider in listColliderEnemy)
        {
            //Debug.Log(listColliderEnemy.Length);

            //Debug.Log("Detected object: " + collider.gameObject.transform.name);

            if (collider.gameObject != gameObject)
            {
                //Debug.Log("object: " + collider.gameObject.name);
                if (collider.CompareTag(Constant.TAG_CHARACTER))
                {
                    float distant = Vector3.Distance(transform.position, collider.transform.position);
                    if (distant < nearest)
                    {
                        nearest = distant;
                        Target = Cache.GetCharater(collider);
                    }
                }                
            }
        }
        //Debug.Log(listColliderEnemy.Length);
        if (listColliderEnemy.Length == 1)
        {
            Target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusToAttack);
    }

    #region spawn wepon________________________________________________________________________________________
    protected void CreateWepon(EWeponType.WeponType wptype)
    {
        if (CheckingHasWepon(wptype))
        {
            SpawWepon(wptype);
        }
        else
        {
            System.Random newRandom = new System.Random();
            int randomWepon = newRandom.Next(0, listWP_SO.listWepon.Count);
            //Debug.Log("wepon number: " + randomWepon);
            EWeponType.WeponType randomWeponType = listWP_SO.listWepon[randomWepon].weponType;
            CharacterWpType = randomWeponType;
            SpawWepon(randomWeponType);
        }
    }

    protected void SpawWepon(EWeponType.WeponType wptype)
    {
        foreach (var wp in listWP_SO.listWepon)
        {
            if (wptype == wp.weponType)
            {
                CurrentWepon.OnInit(wp);
                CurrentWepon.character = this;
                GameObject currentWp = Instantiate(CurrentWepon.wpPrefab, weponTranform);
                if (wptype == EWeponType.WeponType.BOOME)
                {
                    currentWp.transform.localScale = new Vector3(13, 13, 13);

                    currentWp.transform.localPosition = new Vector3(0.25f, 0.07f, 0);

                    currentWp.transform.localRotation = Quaternion.Euler(0, 180, -33.07f);
                }
                else if (wptype == EWeponType.WeponType.Z)
                {
                    currentWp.transform.localScale = new Vector3(13, 13, 13);

                    currentWp.transform.localPosition = new Vector3(-0.07f, 0.15f, 0);

                    currentWp.transform.localRotation = Quaternion.Euler(180, 0, 0);
                }
                currentWp.transform.SetParent(weponTranform);
            }
        }
    }

    protected bool CheckingHasWepon(EWeponType.WeponType wptype)
    {
        if(wptype == EWeponType.WeponType.NONE)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region spawn pant_________________________________________________________________________________
    protected void CreatePant(EPantsType.pantsType pantType)
    {
        if(pantType == EPantsType.pantsType.NONE)
        {
            pant.SetActive(false);
        }
        else
        {
            pant.SetActive(true);
            SetPantMaterial(pantType);
        }
    }

    protected void SetPantMaterial(EPantsType.pantsType pantType)
    {
        foreach(var p in listPant_SO.listPants)
        {
            if (pantType == p.pantType)
            {
                pant.GetComponent<SkinnedMeshRenderer>().material = p.pantMaterial;
            }
        }
    }
    #endregion

    public Vector3 SetDirector()
    {
        if(Target != null)
        {
            Vector3 directer = (Target.transform.position - transform.position).normalized;
            return directer;
        }
        return Vector3.zero;
    }


}
