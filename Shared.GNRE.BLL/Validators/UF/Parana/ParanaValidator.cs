﻿using FluentValidation;

namespace GNRE.BLL.Validators.UF.Parana
{
    public class ParanaValidator : AbstractValidator<Configuracao.Entidades.GNRE>
    {
        public ParanaValidator()
        {
            When(uf => uf.Receita == Classes.Enumerators.EReceita.ICMSSubstituicaoTributariaOperacao, () =>
            {
                RuleFor(uf => uf).SetValidator(new Parana100099Validator());
            });
            When(uf => uf.Receita == Classes.Enumerators.EReceita.ICMSSubstituicaoTributariaOperacao, () =>
            {
                RuleFor(uf => uf).SetValidator(new Parana100102Validator());
            });
        }
    }
}
