﻿using System;
using UnityEngine;
using System.Collections.Generic;
using GiraffeInternal;

public class GiraffeLayer : MonoBehaviour
{

  [NonSerialized]
  private Transform mTransform;

  [NonSerialized]
  private Giraffe mGiraffe;

  [NonSerialized]
  private bool mApplicationIsQuitting;

  [NonSerialized]
  private bool mDirty;

  [NonSerialized]
  private Layer mLayer;

  [SerializeField]
  private int mZOrder;

  [SerializeField]
  private GiraffeAtlas mAtlas;

  [SerializeField]
  private int mScale = 1;

  [NonSerialized]
  private Mesh mesh;

  void Awake()
  {
    mApplicationIsQuitting = false;
    mDirty = false;
    mTransform = GetComponent<Transform>();

    if (mTransform.parent == null)
    {
      Debug.LogException(new Exception("A Giraffe Layer must be attached to a GameObject who's parent contains the Giraffe MonoBehaviour"), this);
    }
    Transform parent = mTransform.parent;
    mGiraffe = parent.GetComponent<Giraffe>();
    if (mGiraffe == null)
    {
      Debug.LogException(new Exception("The parent GameObejct of this Giraffe Layer does not contain a Giraffe monobehaviour"), this);
    }

    mesh = new Mesh();
    mLayer = new Layer(mesh, mAtlas.material, mScale);
    mAtlas.RefreshSprites();

    if (mScale <= 0)
      mScale = 1;

  }

  void OnApplicationQuit()
  {
    mApplicationIsQuitting = true;
  }

  void OnEnable()
  {
    if (mApplicationIsQuitting == false)
    {
      mGiraffe.AddLayer(this);
    }
  }

  void OnDisable()
  {
    if (mApplicationIsQuitting == false)
    {
      mGiraffe.RemoveLayer(this);
    }
  }

  public bool isDirty
  {
    get { return mDirty; }
  }

  public void MarkDirty()
  {
    mDirty = true;
  }

  public GiraffeAtlas atlas
  {
    get
    {
      return mAtlas;
    }

    set
    {
      if (mAtlas == value)
        return;
      mAtlas = value;
    }
  }

  public int zOrder
  {
    get
    {
      return mZOrder;
    }

    set
    {
      if (mZOrder == value)
        return;
      mZOrder = value;
      if (Application.isPlaying && mApplicationIsQuitting == false)
      {
        mGiraffe.DrawOrderChanged();
      }
    }
  }

  public int scale
  {
    get
    {
      return mScale;
    }
    set
    {
      if (mScale == value)
        return;
      mScale = value;
      if (Application.isPlaying && mApplicationIsQuitting == false)
      {
        mLayer.SetScale(mScale);
      }
    }
  }

  public void UpdateLayer()
  {
    mLayer.Update();
  }

  public void DrawLayer()
  {
    mLayer.Draw();
  }

  public void Begin(int nbQuads)
  {
    mLayer.Begin(nbQuads);
  }

  public void SetColour(Color32 colour)
  {
    mLayer.colour = colour;
  }

  public void SetColour(byte r, byte g, byte b, byte a)
  {
    mLayer.colour = new Color32(r, g, b, a);
  }

  public void Add(int x, int y, String spriteName)
  {
    GiraffeSprite sprite = mAtlas.GetSprite(spriteName);
    Add(x, y, sprite.width, sprite.height, sprite);
  }

  public void Add(int x, int y, int w, int h, String spriteName)
  {
    GiraffeSprite sprite = mAtlas.GetSprite(spriteName);
    Add(x, y, w, h, sprite);
  }

  public void Add(int x, int y, GiraffeSprite sprite)
  {
    Add(x, y, sprite.width, sprite.height, sprite);
  }

  public void Add(int x, int y, int w, int h, GiraffeSprite sprite)
  {
    mLayer.Add(x, y, w, h, sprite);
  }

  public void Add(Matrix2D transform, GiraffeSprite sprite)
  {
    mLayer.Add(transform, sprite);
  }

  public void End()
  {
    mLayer.End();
  }

}
