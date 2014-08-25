﻿using System;
using GiraffeInternal;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{
  [SerializeField]
  private int damage;

  [NonSerialized]
  private Transform mTransform;

  [NonSerialized]
  private Rigidbody2D mRigidBody;

  [NonSerialized]
  private BoxCollider2D mCollider;

  [NonSerialized]
  private GiraffeQuadSpriteRenderer mRenderer;

  [NonSerialized]
  private float mTimer;

  [NonSerialized]
  public bool isActive;

  void Awake()
  {
    mTransform = GetComponent<Transform>();
    mRigidBody = GetComponent<Rigidbody2D>();
    mCollider = GetComponent<BoxCollider2D>();
    mRenderer = GetComponent<GiraffeQuadSpriteRenderer>();
  }

  void FixedUpdate()
  {
    mTimer += Time.fixedDeltaTime * Time.timeScale;

    float boundsX0 = 0.0f, boundsY0 = 0.0f;
    float boundsX1 = Screen.width / mRenderer.layer.scale;
    float boundsY1 = Screen.height / mRenderer.layer.scale;

    Vector2 position = mTransform.position;
    if (position.x < boundsX0 || position.x > boundsX1 || position.y < boundsY0 || position.y > boundsY1)
    {
      mRigidBody.velocity = Vector2.zero;
      mRigidBody.angularVelocity = 0.0f;
      isActive = false;
      mRenderer.visible = false;
    }
  }

  public void Fire(Vector2 position, Vector2 velocity)
  {
    mTransform.position = position;
    mTransform.rotation = Quaternion.identity;
    mRigidBody.velocity = velocity;
    mRigidBody.angularVelocity = 0.0f;
    mRenderer.visible = true;
    isActive = true;
    mTimer = 0.0f;
  }

}