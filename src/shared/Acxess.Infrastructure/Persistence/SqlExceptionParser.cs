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

        // 1. Buscar Errores Específicos del Módulo (Índices Únicos Personalizados)
        if (customErrors is not null && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            foreach (var key in customErrors.Keys)
            {
                if (sqlEx.Message.Contains(key))
                {
                    return customErrors[key];
                }
            }
        }

        // 2. Errores Genéricos de SQL Server (Aplican para todos los módulos)
        return sqlEx.Number switch
        {
            2601 or 2627 => Error.Conflict("General.Duplicate", "El registro ya existe en el sistema."),
            547 => Error.Conflict("General.Constraint", "La operación viola una restricción de integridad referencial."),
            2628 or 8152 => Error.Validation("General.DataLength", "Uno de los campos excede la longitud máxima permitida."),
            515 => Error.Validation("General.Required", "Un dato requerido no fue proporcionado."),
            
            _ => null
        };
    }
}
