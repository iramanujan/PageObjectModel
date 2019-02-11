using CommonHelper.Helper.Log;
using System;
using System.Diagnostics;
using System.Threading;


namespace CommonHelper.Helper.Wait
{
    public class Waiter
    {
        #region Fields

        private readonly TimeSpan CheckInterval;
        private readonly Stopwatch Stopwatch;
        private readonly TimeSpan Timeout;
        private bool IsSatisfy = true;
        private Exception LastException;

        #endregion

        #region Constructors and Destructors

        public Waiter(TimeSpan timeout) : this(timeout, TimeSpan.FromSeconds(1))
        {
        }

        public Waiter(TimeSpan timeout, TimeSpan checkInterval)
        {
            this.Timeout = timeout;
            this.CheckInterval = checkInterval;
            this.Stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Properties

        public bool IsSatisfied
        {
            get { return this.IsSatisfy; }
        }

        #endregion

        #region Public Methods and Operators

        public static bool SpinWait(Func<bool> condition, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(1);
            return SpinWait(condition, (TimeSpan)timeout, TimeSpan.FromSeconds(1));
        }

        public static bool SpinWait(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
        {
            return WithTimeout(timeout, pollingInterval).WaitFor(condition).IsSatisfied;
        }

        public static void SpinWaitEnsureSatisfied(Func<bool> condition, string exceptionMessage, int timeOutSeconds = 30, int pollIntervalSeconds = 1)
        {
            WithTimeout(TimeSpan.FromSeconds(timeOutSeconds), TimeSpan.FromSeconds(pollIntervalSeconds)).WaitFor(condition).EnsureSatisfied(exceptionMessage);
        }

        public static void SpinWaitEnsureSatisfied(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval, string exceptionMessage)
        {
            WithTimeout(timeout, pollingInterval).WaitFor(condition).EnsureSatisfied(exceptionMessage);
        }

        public static Waiter WithTimeout(TimeSpan timeout, TimeSpan pollingInterval)
        {
            return new Waiter(timeout, pollingInterval);
        }

        public static Waiter WithTimeout(TimeSpan timeout)
        {
            return new Waiter(timeout);
        }

        public void EnsureSatisfied()
        {
            if (!this.IsSatisfy)
            {
                string message = string.Empty;
                if (this.LastException != null)
                {
                    message = "Check inner waiter exception.";
                }
                throw new TimeoutException(message, this.LastException);
            }
        }

        public void EnsureSatisfied(string message)
        {
            if (!this.IsSatisfy)
            {
                if (this.LastException != null)
                {
                    message += " |Check inner waiter exception.";
                }
                throw new TimeoutException(message, this.LastException);
            }
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Logger.LogExecute("Wait for '{0}'", timeSpan);
            Thread.Sleep(timeSpan);
        }

        public Waiter WaitFor(Func<bool> condition)
        {
            if (!this.IsSatisfy)
            {
                return this;
            }

            while (!this.Try(condition))
            {
                var sleepAmount = Min(this.Timeout - this.Stopwatch.Elapsed, this.CheckInterval);

                if (sleepAmount < TimeSpan.Zero)
                {
                    this.IsSatisfy = false;
                    break;
                }
                Thread.Sleep(sleepAmount);
            }

            return this;
        }

        #endregion

        #region Methods
        private bool Try(Func<bool> condition)
        {
            try
            {
                return condition();
            }
            catch (Exception ex)
            {
                this.LastException = ex;
                return false;
            }
        }

        private static T Min<T>(T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) < 0 ? left : right;
        }
        #endregion

    }
}
