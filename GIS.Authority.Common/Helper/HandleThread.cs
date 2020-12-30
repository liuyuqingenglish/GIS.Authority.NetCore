using System;
using System.Threading;
using System.Threading.Tasks;
using GIS.Authority.Log;

namespace GIS.Authority.Core.Common.Helpers
{
    /// <summary>
    /// 线程管理的基类
    /// </summary>
    public class HandleThread
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        protected readonly byte[] LOCKER = new byte[0];

        /// <summary>
        /// 默认休眠的时间间隔
        /// </summary>
        protected readonly int DEFAULT_SEC_INTERVAL = 1000;

        /// <summary>
        /// 线程
        /// </summary>
        private Task _task = null;

        /// <summary>
        /// 取消任务的对象
        /// </summary>
        private CancellationTokenSource _cancelToken = null;

        /// <summary>
        /// 线程是否运行
        /// </summary>
        private bool _threadIsRunning = true;

        /// <summary>
        /// 线程运行间隔
        /// </summary>
        private int _interval = 1000;

        /// <summary>
        /// 线程名称
        /// </summary>
        private string _threadName = string.Empty;

        /// <summary>
        /// 外界注入的周期执行函数
        /// </summary>
        private Action _callback = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval">运行间隔</param>
        /// <param name="name">线程名称</param>
        public HandleThread(int interval, string name)
        {
            InitThread(interval, name, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval">运行间隔</param>
        /// <param name="name">线程名称</param>
        /// <param name="callback">线程需要执行的方法</param>
        public HandleThread(int interval, string name, Action callback)
        {
            InitThread(interval, name, callback);
        }

        /// <summary>
        /// 开启线程
        /// </summary>
        public void Start()
        {
            _cancelToken = new CancellationTokenSource();
            CancellationToken token = _cancelToken.Token;
            _task = new Task(() => { Running(); }, token);
            _task.Start();
        }

        /// <summary>
        /// 终止线程
        /// </summary>
        public void Stop()
        {
            _threadIsRunning = false;
            if (_task != null)
            {
                _cancelToken.Cancel();
                try
                {
                    _task.Dispose();
                }
                catch
                {
                }

                _cancelToken.Dispose();
            }

            _task = null;
        }

        /// <summary>
        /// 线程运行函数
        /// </summary>
        protected virtual void Running()
        {
            while (_threadIsRunning)
            {
                try
                {
                    ThreadWork();
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(ELogLevel.Error, string.Format("Running-{0}-{1}-{2}", _threadName, ex.Message, ex.StackTrace));
                }
                finally
                {
                    //1秒就判断一次,各种不同的协议自己判断自己的发送逻辑和时间间隔
                    Thread.Sleep(_interval);
                }
            }
        }

        /// <summary>
        /// 线程工作内容--由子类自己实现
        /// </summary>
        protected virtual void ThreadWork()
        {
            if (_callback != null)
            {
                _callback();
            }
        }

        /// <summary>
        /// 初始化线程
        /// </summary>
        private void InitThread(int interval, string name, Action callback)
        {
            _callback = callback;
            _interval = interval;
            _threadName = name;
        }
    }
}