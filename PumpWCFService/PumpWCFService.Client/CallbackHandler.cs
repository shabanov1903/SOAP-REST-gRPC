﻿using PumpWCFService.Client.PumpServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpWCFService.Client
{
    internal class CallbackHandler : IPumpServiceCallback
    {
        public void UpdateStatistics(StatisticsService statistics)
        {
            Console.Clear();
            Console.WriteLine("Обновление по статистике выполнения скрипта");
            Console.WriteLine($"Всего     тактов: {statistics.AllTacts}");
            Console.WriteLine($"Успешных  тактов: {statistics.SuccessTacts}");
            Console.WriteLine($"Ошибочных тактов: {statistics.ErrorTacts}");
        }
    }
}
