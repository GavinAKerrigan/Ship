using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public static Rigidbody2D rb;
    public static Effects fx;
    public static Movement mv;
    public static SpriteRenderer sr;

    [SerializeField] float cooldown = 1f;
    [SerializeField] float duration = 0.25f;
    [SerializeField] EType type;

    private bool active = false;
    private float timer = 0f;
    private Dictionary<EType, IType> map = new Dictionary<EType, IType>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        fx = GetComponent<Effects>();
        mv = GetComponent<Movement>();
        MapDictionary();
        timer = cooldown;
    }

    void FixedUpdate()
    {
        // case: special is active
        if (active)
        {
            // case: special continues
            if (timer < duration)
            {
                timer += Time.deltaTime;
                map[type].Continue();
            }
            // case: special ends
            else
            {
                Debug.Log("Special End");
                active = false;
                timer = 0f;
                map[type].End();
            }
        }

        // case: special is not active
        else if (timer < cooldown) 
        {
            timer += Time.deltaTime;
            if (timer > cooldown) Debug.Log("Special Ready");
        }

        // case: special is not active, cooldown is over, and player presses jump
        else if (Input.GetAxisRaw("Jump") > 0)
        {
            Debug.Log("Special Begin");
            active = true;
            timer = 0f;
            map[type].Begin();
        }
    }
    
    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Dictionary Setup
    private void MapDictionary()
    {
        foreach (EType type in Enum.GetValues(typeof(EType)))
        {
            var typeToCreate = Type.GetType(this.GetType().Namespace + "." + this.GetType().Name + "+" + type.ToString());
            if (typeToCreate != null) map[type] = Activator.CreateInstance(typeToCreate) as IType;
        }
    }

    private interface IType 
    {
        void Begin();
        void Continue();
        void End();
    }

    private enum EType
    {
        Dash,
        Phase
    }
    
    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Special Classes
    class Phase : IType 
    {
        public void Begin() 
        {
            mv.paused = true;
            rb.velocity = mv.transform.up * 5f;
        }

        public void Continue() 
        {
            
        }
        
        public void End() 
        {
            mv.paused = false;
        }
    }

    class Dash : IType 
    {
        public void Begin() 
        {
            rb.velocity = mv.transform.up * 5f;
        }
        public void Continue() {}
        public void End() {}
    }

}
