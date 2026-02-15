using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace LibraryManagement.Infrastructure;

public static class ObjectConverters
{
    public static readonly FuncValueConverter<object?, bool> IsNotNull = 
        new FuncValueConverter<object?, bool>(obj => obj != null);
}