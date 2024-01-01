using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace PeachPlayer.Utils
{
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        // 指示当前线程是否正在处理工作项。
        [ThreadStatic]
        private static bool _currentThreadIsProcessingItems;

        // 要执行的任务列表
        private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // 受锁保护 (_tasks)

        // 此计划程序允许的最大并发级别。
        private readonly int _maxDegreeOfParallelism;

        //指示计划程序当前是否正在处理工作项。
        private int _delegatesQueuedOrRunning = 0;

        //通过构造函数传入最大并行数
        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            ThreadPool.SetMinThreads(maxDegreeOfParallelism, maxDegreeOfParallelism);
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        //将一个task加入调度器队列
        protected sealed override void QueueTask(Task task)
        {
            // 将任务添加到要处理的任务列表中。如果不够
            // 代表当前排队或正在运行以处理任务，请安排其他任务。
            lock (_tasks) //这里加锁，如果while循环中的lock还没有释放，新任务无法添加进队列
            {
                _tasks.AddLast(task);
                if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
                {
                    ++_delegatesQueuedOrRunning;
                    //每次加入task时，如果当前正在执行的线程数小于最大并行数，执行下述方法，在该方法中新开一个线程
                    NotifyThreadPoolOfPendingWork();
                }
            }
        }

        // 通知线程池此调度程序有工作要执行。
        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                //请注意，当前线程现在正在处理工作项。
                //这对于将任务内联到此线程中是必要的。
                _currentThreadIsProcessingItems = true;
                try
                {
                    //处理队列中的所有可用项目。
                    while (true)
                    {
                        Task item;
                        lock (_tasks)
                        {
                            //当没有更多要处理的项目时，
                            //请注意，我们已经完成了处理，然后离开。
                            if (_tasks.Count == 0)
                            {
                                --_delegatesQueuedOrRunning;
                                break;
                            }
                            // 从队列中获取下一项
                            item = _tasks.First.Value;
                            //先从队列中删除该任务，然后再执行，如果task开始执行后，存在于队列中已没有意义
                            _tasks.RemoveFirst();
                        }
                        // 执行我们从队列中拉出的任务
                        base.TryExecuteTask(item);
                    }
                }
                // 我们已完成处理当前线程上的项目
                finally { _currentThreadIsProcessingItems = false; }
            }, null);
        }

        //尝试在当前线程上执行指定的任务。
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            //如果此线程尚未处理任务，我们不支持内联
            if (!_currentThreadIsProcessingItems) return false;

            // If the task was previously queued, remove it from the queue
            if (taskWasPreviouslyQueued)
                // 尝试运行任务。
                if (TryDequeue(task))
                    return base.TryExecuteTask(task);
                else
                    return false;
            else
                return base.TryExecuteTask(task);
        }

        // 尝试从计划程序中删除以前计划的任务。
        protected sealed override bool TryDequeue(Task task)
        {
            lock (_tasks) return _tasks.Remove(task);
        }

        //获取此计划程序支持的最大并发级别。
        public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

        // 获取此计划程序上当前计划的任务的可枚举值。
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_tasks, ref lockTaken);
                if (lockTaken) return _tasks;
                else throw new NotSupportedException();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(_tasks);
            }
        }
    }
}
