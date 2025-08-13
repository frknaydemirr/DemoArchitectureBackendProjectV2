using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Performance
{
    public  class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect()
        {
            _interval = 3;
            _stopwatch= ServiceTool.ServiceProvider.GetService<Stopwatch>();

        }


       
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
           _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            _stopwatch.Stop();
            double  second = _stopwatch.Elapsed.TotalSeconds;
            if (second > _interval)
            {

                //mail codes :
                Debug.WriteLine(
                    $"Performance Rapor : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ==> {second}"
                );
            }
            _stopwatch.Reset();
        }


    }

}
