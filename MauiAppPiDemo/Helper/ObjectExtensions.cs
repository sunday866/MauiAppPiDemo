﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppPiDemo.Helper
{
    internal static class ObjectExtensions
    {
        public static string DumpToConsole<TObject>(this TObject @object)
        {
            var output = "NULL";
            if (@object != null)
            {
                output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }

           return $"[{@object?.GetType().Name}]:\r\n{output}";
            //return @object;
        }
    }
}
