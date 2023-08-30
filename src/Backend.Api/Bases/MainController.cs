using Backend.Core.Bases;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Api.Bases;

[ApiController]
public class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = Erros.ToArray()
        });
    }

    protected ActionResult CustomResponse(CustomValidationResult customValidationResult)
    {
        if (!customValidationResult.IsValid)
        {
            AdicionarErros(customValidationResult);
            return CustomResponse();
        }

        return CustomResponse(customValidationResult?.Data);
    }

    protected ActionResult CustomResponseError(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) AdicionarErros(modelState);
        return CustomResponse();
    }

    protected ActionResult CustomResponseFile(CustomValidationResult customValidationResult, string fileName)
    {
        if (customValidationResult.Errors.Count > 0)
        {
            AdicionarErros(customValidationResult);
        }

        return CustomResponseFileXls((byte[])customValidationResult.Data, fileName);
    }

    protected ActionResult CustomResponseFileXls(byte[] file, string fileName)
    {
        if (OperacaoValida())
        {
            return File(file, "application/octet-stream", fileName);
        }

        return CustomResponse();
    }

    private bool OperacaoValida()
    {
        return !Erros.Any();
    }

    private void AdicionarErroProcessamento(string erro)
    {
        Erros.Add(erro);
    }

    private void AdicionarErros(CustomValidationResult validationResult)
    {
        var erros = validationResult.Errors.SelectMany(x => x.ErrorMessage.Split("|")).ToList();

        foreach (var erro in erros)
        {
            AdicionarErroProcessamento(erro);
        }
    }

    private void AdicionarErros(ValidationResult validationResult)
    {
        var erros = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        foreach (var erro in erros)
        {
            AdicionarErroProcessamento(erro);
        }
    }

    private void AdicionarErros(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            AdicionarErroProcessamento(errorMsg);
        }
    }

}