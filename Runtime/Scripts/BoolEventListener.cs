﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Message2 
{
    public enum BoolCompare
    {
        IsTrue,
        IsFalse
    }

    [Serializable]
    public class BoolCondition
    {
        public BoolCompare Compare;
    }
 
    public class BoolEventListener : GameEventListener
    {
        public BoolCondition Condition;
        protected  override bool CheckCompareCondition()
        {
            BoolEvent e = Event as BoolEvent;
            if (e)
            {
                if (Condition.Compare == BoolCompare.IsTrue)
                {
                    return e.Variable == true;
                }
                else if (Condition.Compare == BoolCompare.IsFalse)
                {
                    return e.Variable == false;
                }

                return false;
            }
            throw new Exception("Event type is wrong:" + Event.ToString());
        }
    }
}