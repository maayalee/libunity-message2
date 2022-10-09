﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using PlayGem.JawRed.Core.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Message2
{
    public class StringVariableTrigger : MonoBehaviour
    {
        [Serializable]
        public class StringCondition
        {
            public StringVariable Variable;
            public StringCompare Compare;
            public string Value;
        }
        
        public ResponseConditionOperator ConditionOperator;
        [NonReorderable] public List<StringCondition> Conditions = new List<StringCondition>();
        public UnityEvent Listeners;
        
        public void Awake()
        {
            foreach (StringCondition condition in Conditions)
            {
                condition.Variable.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
                {
                    Debug.Log("State Changed:" + condition.Variable);
                    Execute();
                };
            }
        }

        private void Execute()
        {
            ConditionCompareResult result = new ConditionCompareResult();
            foreach (StringCondition condition in Conditions)
            {
                if (condition.Compare == StringCompare.Equal)
                {
                    result.Add(condition.Variable.Value == condition.Value);
                }
                else if (condition.Compare == StringCompare.Contains)
                {
                    result.Add(condition.Variable.Value.Contains(condition.Value));
                }
                else if (condition.Compare == StringCompare.IsNot)
                {
                    result.Add(condition.Variable.Value != condition.Value);
                }    
            }
            if (result.CheckConditionOperator(ConditionOperator))
            {
                Listeners?.Invoke();
            }
        }
    }
}