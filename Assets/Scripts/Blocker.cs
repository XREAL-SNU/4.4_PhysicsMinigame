using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public GameObject bullet;
    private int _level;
    private float _firespeed;
    private Vector3 _location;
    private int _hp;
    private bool created;
    public void init(Vector3 locate, int lev){
        _location = locate;
        transform.position += locate;
        _level = lev;
        _firespeed = setFireSpeed(lev);
        _hp = setHp(lev);
    }
    public void levelchange(int delta){
        _level += delta;
        _firespeed = setFireSpeed(_level);
        _hp = setHp(_level);
    }
    public int getLevel(){
        return _level;
    }
    private float setFireSpeed(int lev){
        if(lev == 0)
            return 0;
        return 3.0f / lev;
    }
    private int setHp(int lev){
        return 100 * (lev+1);
    }
    public int hit(int damage){
        _hp -= damage;
        return _hp;
    }
    void Awake(){
        _level = 1;
        _firespeed = 3.0f;
        _hp = 100;
        created = false;
    }
    void Update(){
        if(!created){
            if(GameManager.Instance.getInGame()){
                created = true;
                StartCoroutine(FireBullet());
            }
        }
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){
            int remain = hit(20);
            if(remain<=0)
                Destroy(gameObject);
        }
    }
    IEnumerator FireBullet(){
        GameObject bullets = Instantiate(bullet);
        bullets.transform.position = transform.position;
        yield return new WaitForSecondsRealtime(_firespeed);
        created = false;
    }
}
