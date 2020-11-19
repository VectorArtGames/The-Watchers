using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PortalCore : MonoBehaviour
{
    private MeshRenderer mesh;
    public string ID { get; set; }
    public TransitionCore transition;
    private PortalIdentity identity;

    [ColorUsageAttribute(true, true)]
    public Color PortalColor;

    private MaterialPropertyBlock propertyBlock;

    public bool _onCooldown;
    public bool OnCooldown
    {
        get => _onCooldown;
        set
        {
            _onCooldown = value;
            currentTime = Time.time + cooldownTime;
            if (value)
                StartCoroutine(CheckCooldown());
        }
    }

    public float cooldownTime = 1f;
    private float currentTime;
    public IEnumerator CheckCooldown()
    {
        yield return new WaitUntil(() => currentTime < Time.time);
        OnCooldown = false;
    }

    private void Awake()
    {
        identity = GetComponentInParent<PortalIdentity>();
        propertyBlock = new MaterialPropertyBlock();
        TryGetComponent(out mesh);

    }

    // Start is called before the first frame update
    void Start()
    {
        transition = TransitionCore.Instance;
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnCooldown) return;
        if (!(other.CompareTag("Player") && other.GetComponentInParent<CharacterController>() is CharacterController player)) return;
        var offset = player.transform.position - identity.transform.position;
        Transition(player, offset);
    }

    private void Transition(CharacterController player, Vector3 offset) =>
        TransitionEvent.MakeTransitionEvent(() => TeleportPlayer(player, offset));

    private void TeleportPlayer(CharacterController player, Vector3 offset)
    {
        var id = Portals.Instance.GetByID(identity);
        if (id == null || !(id.GetComponentInChildren<PortalCore>() is PortalCore pCore)) return;

        // Sets Portals on Cooldown
        pCore.OnCooldown = true;
        OnCooldown = true;

        // Turn off Character Controller to be able to Teleport
        player.enabled = false;
        player.transform.position = id.transform.position + offset;
        player.enabled = true;

        Debug.Log($"Teleported!\n {player.name}");
    }

    public void UpdateColor()
    {
        mesh.GetPropertyBlock(propertyBlock);
        if (propertyBlock == null) return;

        propertyBlock.SetColor("PortalColor", PortalColor);
        mesh.SetPropertyBlock(propertyBlock);

    }
}
