using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Omega.Routines.Tests
{
    public class RoutineExceptionTests
    {
        [UnityTest]
        public IEnumerator RoutineShouldSetupErrorStateWhenThrowException()
        {
            var keyMassage =
                $"This exception was thrown specifically for the unit test ({nameof(RoutineShouldSetupErrorStateWhenThrowException)})";

            LogAssert.ignoreFailingMessages = true;
            yield return Routine.Task(() => throw new Exception(keyMassage))
                .GetRoutine(out var routine);
            LogAssert.ignoreFailingMessages = false;

            Assert.True(routine.IsError);
            Assert.AreEqual(keyMassage, routine.Exception.Message);
        }
    }
}