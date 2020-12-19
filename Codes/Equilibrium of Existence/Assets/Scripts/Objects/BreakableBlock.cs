using System.Collections;
using UnityEngine;

namespace Eoe.Objects
{
    public class BreakableBlock : MonoBehaviour
    {
        private Animator anim;
        [SerializeField] private float repairTime;
        [SerializeField] private float breakingTime;
        public bool broken = false;
        private BoxCollider2D boxC;
        [SerializeField] private bool contact;
        [SerializeField] private bool mustRepair;
        private static readonly int AnimationSpeed = Animator.StringToHash("animationSpeed");

        private void Start()
        {
            anim = GetComponent<Animator>();
            anim.SetFloat(AnimationSpeed,0f);
            boxC = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H)) {
                Break();
            }

            if (broken) {
                Collider2D hit = Physics2D.OverlapBox(transform.position,boxC.size,0f,LayerMask.GetMask("ActiveCharacter"));
                if (hit != null) {
                    contact = true;
                }
                else if (contact) {
                    contact = false;
                    if (mustRepair) {
                        mustRepair = false;
                        broken = false;
                        boxC.enabled = true;
                    }
                }
            }
        }

        private void StopAnimation()
        {
            if (anim.GetFloat(AnimationSpeed) == 1f) {
                anim.SetFloat(AnimationSpeed,0f);
            }
        }

        private void StopAnimation2()
        {
            if (anim.GetFloat(AnimationSpeed) == -1f) {
                anim.SetFloat(AnimationSpeed,0f);
                if (!contact) {
                    broken = false;
                    boxC.enabled = true;
                }
                else mustRepair = true;
            }
        }

        public void Break()
        {
            if (!broken) {
                broken = true;
                mustRepair = false;
                anim.SetFloat(AnimationSpeed,1f);
                StartCoroutine(Repair());
            }
        }

        private IEnumerator Repair()
        {
            yield return new WaitForSeconds(breakingTime);
            boxC.enabled = false;
            yield return new WaitForSeconds(repairTime);
            anim.SetFloat(AnimationSpeed,-1f);
        }

        /*private void OnCollisionEnter2D(Collision2D collision)
    {
        contact = true;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!broken) {
            if (other.gameObject.CompareTag("Player")) {
                if (Ext.Sign(Player.player.maxGravity) == -1 && Ext.Sign(Player.verSpeed) == Ext.Sign(Player.player.maxGravity)) {
                    foreach (ContactPoint2D hitPoints in other.contacts) {
                        //if (hitPoints.normal == Vector2.up * Ext.Sign(Player.player.maxGravity)) {
                            Break();
                        //}
                    }
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        contact = false;
    }*/
    }
}
