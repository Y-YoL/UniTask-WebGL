using System;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class Sample : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private Button button1;

        [SerializeField]
        private Button button2;

        private int totalSeconds;

        private void Awake()
        {
            this.button1.onClick.AddListener(() => UniTask.RunOnThreadPool(this.HevyMethod).Forget());
            this.button2.onClick.AddListener(() => this.ExecuteAsync().Forget());
        }

        private void Update()
        {
            this.text.text = $"{this.totalSeconds}\n{DateTime.Now}";
        }

        private async UniTask ExecuteAsync()
        {
            await UniTask.SwitchToThreadPool();
            this.HevyMethod();
        }

        private void HevyMethod()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.Elapsed.TotalSeconds < 10)
            {
                this.totalSeconds = (int)sw.Elapsed.TotalMilliseconds;
            }
        }
    }
}
