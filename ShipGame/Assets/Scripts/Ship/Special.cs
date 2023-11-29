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

    [SerializeField] eType type;
    private Dictionary<eType, IType> map = new Dictionary<eType, IType>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        fx = GetComponent<Effects>();
        mv = GetComponent<Movement>();

        MapDictionary();
        map[type].Awake();
    }

    void FixedUpdate() { map[type].Update(); }
    
    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Dictionary Setup
    private void MapDictionary()
    {
        foreach (eType type in Enum.GetValues(typeof(eType)))
        {
            var typeToCreate = Type.GetType(this.GetType().Namespace + "." + this.GetType().Name + "+" + type.ToString());
            if (typeToCreate != null) map[type] = Activator.CreateInstance(typeToCreate) as IType;
        }
    }

    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Special Classes
    class IType : MonoBehaviour
    {
        private bool active = false;
        private float timer;

        public float cooldown = 1f;
        public float duration = 0.25f;
        public float fuelCost = 15f;
        public inputType type = inputType.Press;

        public virtual void Awake() { timer = cooldown; }
        public virtual void Begin() {}
        public virtual void Continue() {}
        public virtual void End() {}
        public void Update()
        {
            switch (type)
            {
                case inputType.Hold:
                    if (active)
                    {
                        if (timer < duration && Input.GetAxisRaw("Jump") > 0)
                        {
                            timer += Time.deltaTime;
                            mv.fuel -= fuelCost * Time.deltaTime;
                            Continue();
                        }
                        else
                        {
                            active = false;
                            timer = 0f;
                            End();
                        }
                    }
                    else if (timer < cooldown) timer += Time.deltaTime;
                    else if (Input.GetAxisRaw("Jump") > 0)
                    {
                        active = true;
                        timer = 0f;
                        Begin();
                    }
                    break;

                case inputType.Press:
                    if (active)
                    {
                        if (timer < duration)
                        {
                            timer += Time.deltaTime;
                            mv.fuel -= fuelCost * Time.deltaTime;
                            Continue();
                        }
                        else
                        {
                            active = false;
                            timer = 0f;
                            End();
                        }
                    }
                    else if (timer < cooldown) timer += Time.deltaTime;
                    else if (Input.GetAxisRaw("Jump") > 0)
                    {
                        active = true;
                        timer = 0f;
                        Begin();
                    }
                    break;
            }
        }
    }

    private enum inputType
    {
        Hold,
        Press
    }

    private enum eType
    {
        Charge,
        Dash,
        Phase
    }

    class Charge : IType 
    {
        public override void Awake() 
        { 
            cooldown = 5f;
            duration = 0.5f;
            type = inputType.Hold;
            base.Awake();
        }

        public override void Begin() 
        {
            mv.paused = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            sr.color = Color.red;
            fx.Stabilize(0.5f, 10f);
        }
        private float speed = 10f;
        public override void Continue() 
        {
            mv.transform.position += mv.transform.up * Time.deltaTime * speed;
        }
        public override void End() 
        {
            mv.paused = false;
            sr.color = Color.white;
            fx.Stabilize(0.5f, 10f);
        }
    }

    class Dash : IType 
    {
        public override void Awake() 
        {
            fuelCost = 5f;
            duration = 0f;
        }
        
        public override void Begin() 
        {
            rb.velocity = mv.transform.up * 5f;
            fx.Thrust(mv.transform.up * 2f, 20f);
            mv.fuel -= fuelCost;
        }
    }
    
    class Phase : IType 
    {
        public override void Awake() 
        { 
            type = inputType.Hold;
            base.Awake();
        }

        private float speed = 15f;
        public override void Begin() 
        {
            mv.paused = true;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            sr.color = Color.blue;
            fx.Stabilize(0.5f, 10f);
        }
        public override void Continue() 
        {
            mv.transform.position += mv.transform.up * Time.deltaTime * speed;
        }
        public override void End() 
        {
            mv.paused = false;
            rb.isKinematic = false;
            sr.color = Color.white;
            fx.Stabilize(0.5f, 10f);
        }
    }

}