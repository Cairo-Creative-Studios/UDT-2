using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IEnumeratorExtensions
{
    /// <summary>
    /// Waits until the specified predicate returns true.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <param name="predicate">The predicate to evaluate.</param>
    /// <returns>An enumerator that waits until the predicate returns true.</returns>
    public static IEnumerator WaitUntil(this IEnumerator enumerator, Func<bool> predicate)
    {
        while(!predicate())
        {
            yield return null;
        }
    }

    /// <summary>
    /// Waits until the specified predicate is true and then performs the specified action.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <param name="predicate">The predicate to evaluate.</param>
    /// <param name="action">The action to perform.</param>
    /// <returns>An IEnumerator that can be used for coroutine execution.</returns>
    public static IEnumerator WaitToPerformAction(this IEnumerator enumerator, Func<bool> predicate, Action action)
    {
        while(!predicate())
        {
            yield return null;
        }
        action();
    }
}