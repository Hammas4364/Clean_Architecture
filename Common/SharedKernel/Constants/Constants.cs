using SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Constants;

public static class Constants
{
    public static string GetToken_PostFix(string tableName) => tableName switch
    {
        "Controller" => "cntrl",
        "Schedule" => "schd",
        "Door" => "door",
        "Reader" => "reader",
        "CardFormat" => "crdfrmt",
        "AccessLevel" => "accslvl",
        "QUser" => "qUser",
        "Card" => "crd",
        _ => throw new AppException($"No Token is found against the {tableName} Table.")
    };
}
