using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissileControl : MonoBehaviour {
    public PlayerControl pcon;
    public GameObject target;
    public GameObject uiFrame, missileIconPrefab;

    public Missile defaultMissile;
    public Missile[] missiles;
    public Dictionary<Missile, int> inventory = new Dictionary<Missile, int>();
    private Dictionary<Missile, MissileIcon> inventoryIcons = new Dictionary<Missile, MissileIcon>();
    private static KeyCode[] numkeys = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
        KeyCode.Minus,
        KeyCode.Equals
    };

    public Missile current;
    private const float RELOADTIME = 0.2f;
    private float reload = 0f;
    private bool inputDown = false;

    public const float CHARGETIME = 2f, MAXCHARGE = 1f;
    public float charge = 0f;

    public static MissileControl instance;
    public static int missilesUsed = 0;

    private void Awake() {
        instance = this;
        missilesUsed = 0;
    }

    void Start() {
        pcon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        current = defaultMissile;

        inventoryIcons.Clear();
        for (int i = 0; i < missiles.Length; i++) {
            AddButton(missiles[i], i);
        }

        /*if (true) {
            AddMissile(missiles[1], 10);
            AddMissile(missiles[2], 15);
            AddMissile(missiles[3], 5);
            AddMissile(missiles[4], 20);
            AddMissile(missiles[5], 8);
            AddMissile(missiles[6], 18);
        }*/
    }

    void Update() {
        if(reload < RELOADTIME) {
            reload += Time.deltaTime;
            SwitchMissile();
        }
        else {
            if (Input.GetMouseButton(0) && (inputDown || !EventSystem.current.IsPointerOverGameObject())) {
                inputDown = true;
                charge += Time.deltaTime;
                if (charge > CHARGETIME) charge = CHARGETIME;
            }
            else {
                if (inputDown) {
                    //shoot
                    inputDown = false;
                    if (!Input.GetMouseButton(1) && UseMissile(current)) reload = 0f;
                    charge = 0f;
                }
                SwitchMissile();
            }
        }
    }

    private void SwitchMissile() {
        for (int i = 0; i < missiles.Length; i++) {
            if (Input.GetKeyDown(numkeys[i]) && IsValid(missiles[i])) {
                current = missiles[i];
                break;
            }
        }
    }

    public void AddMissile(Missile missile, int amount) {
        if (missile == defaultMissile) return;
        if (inventory.ContainsKey(missile)) {
            inventory[missile]+=amount;
        }
        else {
            inventory.Add(missile, amount);
            inventoryIcons[missile].gameObject.SetActive(true);
        }
    }

    public bool UseMissile(Missile missile) {
        if (missile == defaultMissile) {
            missile.Fire(pcon, target.transform.position, 1f + charge / CHARGETIME * MAXCHARGE);
            missilesUsed++;
            return true;
        }
        if (!inventory.ContainsKey(missile) || inventory[missile] <= 0) return false;
        missile.Fire(pcon, target.transform.position, 1f + charge / CHARGETIME * MAXCHARGE);
        inventory[missile]--;
        missilesUsed++;

        if (inventory[missile] <= 0) current = defaultMissile;
        return true;
    }

    public bool IsValid(Missile m) {
        return m == defaultMissile || (inventory.ContainsKey(m) && inventory[m] > 0);
    }

    private void AddButton(Missile m, int i) {
        MissileIcon mi = Instantiate(missileIconPrefab, Vector3.zero, Quaternion.identity).GetComponent<MissileIcon>();
        mi.transform.SetParent(uiFrame.transform, false);
        mi.Set(this, m, i+1);

        if(m != defaultMissile) inventoryIcons.Add(m, mi);
    }
}
