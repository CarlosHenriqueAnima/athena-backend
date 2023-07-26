using System.Globalization;
using System.Text.RegularExpressions;

namespace AthenasAcademy.Services.Core.Extensions;

public static class StringExtensions
{
    public static string FormatarTextoCamelCase(this string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return texto;

        texto = Regex.Replace(texto, @"[^a-zA-Z0-9]", " ");

        string[] palavras = texto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < palavras.Length; i++)
        {
            palavras[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(palavras[i].ToLower());
        }

        texto = string.Concat(palavras);
        texto = char.ToLower(texto[0]) + texto[1..];

        return texto;
    }

    public static string ObterPrimeiroNome(this string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return string.Empty;

        string[] partesNome = nome.Trim().Split(' ');

        string primeiroNome = partesNome[0];

        return primeiroNome;
    }

    public static string ObterSobrenome(this string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return string.Empty;

        string[] partes = nome.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        string sobrenomeCompleto = string.Join(" ", partes.Skip(1));

        return sobrenomeCompleto;
    }

    public static string FormatarCPF(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return string.Empty;

        return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
    }

}