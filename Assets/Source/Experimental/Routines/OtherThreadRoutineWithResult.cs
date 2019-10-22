using System;
using System.Collections;
using System.Threading.Tasks;

namespace Omega.Experimental.Routines
{
    public sealed class OtherThreadRoutine<TResult> : Routine<TResult>
    {
        private readonly Func<TResult> _action;

        public OtherThreadRoutine(Func<TResult> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override IEnumerator RoutineUpdate()
        {
            var task = new Task<TResult>(_action);
            task.Start();

            while (task.Status == TaskStatus.Running || task.Status == TaskStatus.WaitingForActivation ||
                   task.Status == TaskStatus.WaitingToRun)
                yield return null;

            if (task.IsFaulted)
                SetException(task.Exception);
            else SetComplete(task.Result);
        }
    }
}