using System.Threading.Tasks;
using UnityEngine;

namespace App.Core
{
    class LoadingTask_DelayLoading : ILoadingTask
    {
        public Task Run()
        {
            Debug.Log("Task delay");
            return Task.Delay(1);
        }
    }
}