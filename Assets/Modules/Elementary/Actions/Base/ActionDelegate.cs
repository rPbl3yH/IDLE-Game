using System;

namespace Modules.Elementary.Actions.Base
{
    public sealed class ActionDelegate : IAction
    {
        private readonly Action action;

        public ActionDelegate(Action action)
        {
            this.action = action;
        }

        public void Do()
        {
            this.action?.Invoke();
        }
    }

    public sealed class ActionDelegate<T> : IAction<T>
    {
        private readonly Action<T> action;

        public ActionDelegate(Action<T> action)
        {
            this.action = action;
        }

        public void Do(T args)
        {
            this.action?.Invoke(args);
        }
    }
}