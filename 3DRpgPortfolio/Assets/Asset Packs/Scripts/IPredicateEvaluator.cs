using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPredicateEvaluator
{ 
    //Nullable Boolean
    bool? Evaluate(string predicate, string[] parameters);
}
