﻿using FluentValidation;
using NFe.BLL.Configuracao.ValueObjects;
using NFe.BLL.Enums;
using DFe.Utils.Extensoes;
using DFe.Classes.Entidades;

namespace NFe.BLL.Validators
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator(EPessoa ePessoa)
        {
            RuleFor(pessoa => pessoa.PessoaTipo).Must(tipo => tipo.EValido()).WithMessage($"{ePessoa.ToString()} tipo pessoa é inválido!");
            RuleFor(pessoa => pessoa.NomeRazaoSocial).NotEmpty().WithMessage($"{ePessoa.ToString()} nome não informado!");
            RuleFor(pessoa => pessoa.RGInscricaoEstadual).NotEmpty().When(pessoa => !_isEstrangeiro(pessoa.Endereco?.MunicipioEstadoSigla) && pessoa.PessoaTipo == ETipoPessoa.Juridica).WithMessage($"{ePessoa.ToString()} RG/IE não informado!");
            RuleFor(pessoa => pessoa.CPFCNPJ).Length(14).When(pessoa => !_isEstrangeiro(pessoa.Endereco?.MunicipioEstadoSigla) && pessoa.PessoaTipo == ETipoPessoa.Juridica).WithMessage($"{ePessoa.ToString()} CNPJ é inválido!");
            RuleFor(pessoa => pessoa.CPFCNPJ).Length(11).When(pessoa => !_isEstrangeiro(pessoa.Endereco?.MunicipioEstadoSigla) && pessoa.PessoaTipo == ETipoPessoa.Fisica).WithMessage($"{ePessoa.ToString()} CPF é inválido!");
            //RuleFor(pessoa => pessoa.Endereco).NotNull().When.WithMessage($"{ePessoa.ToString()} Endereço não informado!").DependentRules(() =>
            //{
                RuleFor(pessoa => pessoa.Endereco).SetValidator(new EnderecoValidator(ePessoa)).When(pessoa => pessoa.Endereco != null);
            //});
        }

        private bool _isEstrangeiro(Estado? uf)
        {
            return uf != null && uf == Estado.EX;
        }
    }
}
