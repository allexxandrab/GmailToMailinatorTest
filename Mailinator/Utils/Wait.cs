using System;
using System.Threading;

namespace Mailinator.Utils
{
    public class Wait
    {
        public static bool UntilTrue(Func<bool> action, TimeSpan? span = null, int pollingInterval = 1000,
string debugActionName = "", bool handleException = true)
        {
            span = span ?? TimeSpan.FromSeconds(10);
            if (debugActionName.Equals(string.Empty)) debugActionName = "action";
            var start = DateTime.Now;
            Exception innerException = null;
            while (DateTime.Now - start < span)
            {
                try
                {
                    var res = action.Invoke();
                    if (res)
                    {
                        if (!string.IsNullOrEmpty(debugActionName))
                            return true;
                    }
                }
                catch (Exception e)
                {
                    innerException = e;
                    if (!handleException)
                        throw innerException;
                }
                if (pollingInterval != 0)
                {
                    Thread.Sleep(pollingInterval);
                }
            }
            if (!string.IsNullOrEmpty(debugActionName))
                if (innerException != null)
                    throw innerException;
            throw new Exception(string.Format("Wait until condition was failed. {0}", debugActionName));
        }



        public static void Assert(Func<bool> constraint, TimeSpan span, string message)
        {
            var result = UntilTrue(constraint, span);
            if (result == false)
                throw new Exception(message);
        }



        public static void Until(Action action, TimeSpan? span = null, int pollingInterval = 1000)
        {
            span = span ?? TimeSpan.FromSeconds(10);
            var start = DateTime.Now;
            Exception innerException = null;
            while (DateTime.Now - start < span)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch (Exception e)
                {
                    innerException = e;
                }
                if (pollingInterval != 0)
                {
                    Thread.Sleep(pollingInterval);
                }
            }



            if (innerException != null)
                throw innerException;
        }
    }



    public class Wait<T>
    {
        private readonly T _source;



        public Wait(T source, TimeSpan time)
        {
            _source = source;
            Timeout = time;
            PollingInterval = TimeSpan.FromSeconds(0);
        }



        private string Message { get; set; }
        public TimeSpan PollingInterval { private get; set; }
        public TimeSpan Timeout { private get; set; }



        public TResult Until<TResult>(Func<T, TResult> condition)
        {
            var start = DateTime.Now;
            Exception innerException = null;
            while (DateTime.Now - start < Timeout)
            {
                try
                {
                    var res = condition(_source);
                    return res;
                }
                catch (Exception e)
                {
                    innerException = e;
                }
                if (PollingInterval.Milliseconds != 0)
                {
                    Thread.Sleep((int)PollingInterval.TotalMilliseconds);
                }
            }
            if (innerException != null)
                throw innerException;



            return default(TResult);
        }



        public void Until(Action<T> condition)
        {
            var start = DateTime.Now;
            while (DateTime.Now - start < Timeout)
            {
                try
                {
                    condition(_source);
                    return;
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
                if (PollingInterval.Duration() > TimeSpan.FromMilliseconds(1))
                {
                    Thread.Sleep((int)PollingInterval.TotalMilliseconds);
                }
            }



            throw new Exception(Message);
        }



        public void UntilTrue(Func<T, bool> condition)
        {
            var start = DateTime.Now;
            Exception innerException = null;
            while (DateTime.Now - start < Timeout)
            {
                try
                {
                    var res = condition(_source);
                    if (res)
                        return;
                }
                catch (Exception e)
                {
                    innerException = e;
                }
                if (PollingInterval.Milliseconds != 0)
                {
                    Thread.Sleep((int)PollingInterval.TotalMilliseconds);
                }
            }



            if (innerException != null)
                throw innerException;
        }
    }
}
