using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootGun()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale",Vector3.one * 1.1f,"time", 0.04f,"easetype",iTween.EaseType.easeInOutBack));
        iTween.ScaleTo(gameObject, iTween.Hash("scale",Vector3.one,"time", 0.04f,"delay",0.04f,"easetype",iTween.EaseType.easeInBounce));
        //iTween.PunchScale(gameObject,Vector3.one * 0.5f,0.2f);
        muzzleFlash.Play();
        GameObject cloneBullet = Instantiate(bulletObject,shootPosition.position,Quaternion.identity);
        Rigidbody rb = cloneBullet.GetComponent<Rigidbody>();
        cloneBullet.SetActive(true);
        rb.AddForce(cloneBullet.transform.right * 100,ForceMode.Impulse);
    }

    IEnumerator ShootEffect()
    {
        float counter = 0;

        for(int i=0; i < 20; i++)
        {
            counter = i;
            if(i > 20) counter = counter - 100;
            skinnedMesh.SetBlendShapeWeight(0,counter);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
