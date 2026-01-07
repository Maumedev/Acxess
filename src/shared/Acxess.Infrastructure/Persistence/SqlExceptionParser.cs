using System.Text.RegularExpressions;
using Acxess.Shared.ResultManager;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Infrastructure.Persistence;

public static class SqlExceptionParser
{
public static Error? Parse(DbUpdateException ex, Dictionary<string, Error>? customErrors = null)
    {
        if (ex.InnerException is not SqlException sqlEx)
        {
            return null;
        }

        if (customErrors is not null)
        {
            foreach (var key in customErrors.Keys)
            {
                if (sqlEx.Message.Contains(key))
                {
                    return customErrors[key];
                }
            }
        }

       return sqlEx.Number switch
        {
            2601 or 2627 => Error.Conflict("General.Duplicate", 
                $"El registro ya existe. Detalle: {ExtractQuotedValue(sqlEx.Message)}"),

            547 => Error.Conflict("General.Constraint", 
                "No se puede realizar la acción porque el registro está relacionado con otros datos."),

            2628 or 8152 => Error.Validation("General.DataLength", 
                $"El valor es demasiado largo para el campo: {ExtractColumnName(sqlEx.Message)}"),

            515 => Error.Validation("General.Required", 
                $"El campo {ExtractColumnName(sqlEx.Message)} es obligatorio."),

            _ => null
        };
    }


    private static string ExtractColumnName(string message)
    {
        var match = Regex.Match(message, @"'([^']*)'");
        return match.Success ? match.Groups[1].Value : "desconocido";
    }

    private static string ExtractQuotedValue(string message)
    {
        var matches = Regex.Matches(message, @"'([^']*)'");
        if (matches.Count > 0)
        {
            return string.Join(" - ", matches.Select(m => m.Groups[1].Value));
        }
        return "valor duplicado";
    }
}
