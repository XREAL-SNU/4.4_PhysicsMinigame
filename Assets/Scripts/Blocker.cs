using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    private int _level;
    private float _firespeed;
    private Vector3 _location;
    private int _hp;
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
    
}
