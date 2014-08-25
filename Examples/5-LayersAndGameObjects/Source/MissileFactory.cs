﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MissileFactory : MonoBehaviour
{

  [NonSerialized]
  private Dictionary<GameObject, List<Missile>> mMissiles;

  void Awake()
  {
    mMissiles = new Dictionary<GameObject, List<Missile>>(4);
  }

  public Missile Add(GameObject prefab)
  {
    List<Missile> missiles = null;
    if (mMissiles.TryGetValue(prefab, out missiles) == false)
    {
      missiles = new List<Missile>(16);
      mMissiles.Add(prefab, missiles);
    }

    Missile missile = null;
    foreach (var m in missiles)
    {
      if (m.isActive == false)
      {
        missile = m;
        break;
      }
    }
    if (missile == null)
    {
      GameObject go = Instantiate(prefab) as GameObject;
      go.transform.parent = transform.parent;
      missile = go.GetComponent<Missile>();
      missiles.Add(missile);
    }
    else
    {
      missile.gameObject.SetActive(true);
    }

    return missile;
  }

}

