using System.Linq;
using System.Text.RegularExpressions;

namespace CpfCnpjLibrary
{
    public static partial class Cnpj
	{
		private static readonly int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
		private static readonly int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

		public static bool Validar(string cnpj)
		{
			if (string.IsNullOrWhiteSpace(cnpj))
				return false;

			cnpj = RemoverCaracteresEspeciais(cnpj).ToUpperInvariant();

			if (!ValidarAlfanumerico(cnpj))
				return false;

			cnpj = AjustarComprimentoLegado(cnpj);

			if (cnpj.Length != 14)
				return false;

			if (!char.IsDigit(cnpj[12]) || !char.IsDigit(cnpj[13]))
				return false;

			if (TodosCaracteresIguais(cnpj))
				return false;

			string raiz = cnpj.Substring(0, 12);
			string digitos = CalcularDigitosVerificadores(raiz);
			return cnpj.EndsWith(digitos);
		}

		private static string RemoverCaracteresEspeciais(string numero)
		{
			return Regex.Replace(numero, "[^0-9a-zA-Z]+", "");
		}

		private static string ZeroEsquerda(string numero, int qtdValorCompleto)
		{
			numero = numero.PadLeft(qtdValorCompleto, '0');

			return numero;
		}

		private static bool ValidarNumerico(string numero)
		{
			if (numero.All(char.IsDigit))
				return true;

			return false;
		}

		private static bool ValidarAlfanumerico(string numero)
		{
			return numero.All(char.IsLetterOrDigit);
		}

		private static string AjustarComprimentoLegado(string cnpj)
		{
			if (ValidarNumerico(cnpj))
				return ZeroEsquerda(cnpj, 14);

			return cnpj;
		}

		private static bool TodosCaracteresIguais(string valor)
		{
			char comparar = valor[0];
			foreach (var caractere in valor)
			{
				if (comparar != caractere)
					return false;
			}
			return true;
		}

		private static string CalcularDigitosVerificadores(string raiz)
		{
			int primeiroDigito = CalcularDigito(raiz, multiplicador1);
			int segundoDigito = CalcularDigito(raiz + primeiroDigito, multiplicador2);
			return primeiroDigito.ToString() + segundoDigito.ToString();
		}

		private static int CalcularDigito(string valor, int[] pesos)
		{
			int soma = 0;
			for (int i = 0; i < valor.Length; i++)
				soma += ConverterCaractereParaValor(valor[i]) * pesos[i];

			int resto = soma % 11;
			if (resto < 2)
				return 0;

			return 11 - resto;
		}

		private static int ConverterCaractereParaValor(char caractere)
		{
			if (char.IsDigit(caractere))
				return caractere - '0';

			if (caractere >= 'A' && caractere <= 'Z')
				return caractere - '0';

			return -1;
		}
	}
}
