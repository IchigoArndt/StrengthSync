using FluentValidation.Results;
using System.ComponentModel;

namespace StrengthSync.Infra.Exceptions
{
    public class ExceptionPayload
    {
        [Description("Código do erro (ex.: 400, 500, 1001, etc).")]
        public int ErrorCode { get; set; }

        [Description("Mensagem resumida do erro.")]
        public string ErrorMessage { get; set; }

        [Description("Lista de erros de validação, quando aplicável.")]
        public List<ValidationFailure> Errors { get; set; }

        /// <summary>
        /// Método para criar um novo ExceptionPayload de uma exceção de negócio.
        ///
        /// As exceções de negócio, que são providas no StrengthSync.Domain
        /// são identificadas pelos códigos no enum ErrorCodes.
        ///
        /// Assim, esse método monta o ExceptionPayload, que será o código retornado o cliente,
        /// com base na exceção lançada.
        ///
        /// </summary>
        /// <param name="exception">É a exceção lançada</param>
        /// <param name="errorCode">Código HTTP de erro</param>
        /// <param name="failures">Lista de problemas de validação</param>
        /// <returns>ExceptionPayload contendo o código do erro e a mensagem da da exceção que foi lançada </returns>
        public static ExceptionPayload New<T>(T exception, int errorCode, List<ValidationFailure> failures = null) where T : Exception
        {
            return new ExceptionPayload
            {
                ErrorCode = errorCode,
                ErrorMessage = exception.Message,
                Errors = failures,
            };
        }
    }
}
