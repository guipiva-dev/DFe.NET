﻿using System;
using System.Collections.Generic;
using DFe.Utils.Extensoes;

namespace GNRE.BLL.Configuracao.Entidades.GNREUF.Tocantins
{
    public class Tocantins100099 : GNRE
    {
        public Tocantins100099(Emitente emitente,
            Destinatario destinatario,
            int detalhamentoReceita,
            int produto,
            double notaFiscalNumero,
            string chaveAcessoNFe,
            DateTime periodoReferencia,
            DateTime dataVencimento,
            DateTime dataPagamento,
            decimal valorST,
            decimal valorFECP = 0,
            string convenio = null)
        {
            NotaFiscalReferencia = notaFiscalNumero;
            Versao = DFe.Classes.Flags.VersaoServico.Versao200;
            Receita = Classes.Enumerators.EReceita.ICMSSubstituicaoTributariaOperacao;
            UF = DFe.Classes.Entidades.Estado.TO;

            Emitente = emitente;
            Destinatario = destinatario;

            ValorST = valorST;
            ValorFECP = valorFECP;
            ValorTotal = valorST + valorFECP;

            DataPagamento = dataPagamento;
            DataVencimento = dataVencimento;

            TipoDocumento = Classes.Enumerators.ETipoDocumento.NotaFiscal;
            DocumentoNumero = notaFiscalNumero.ToString();

            DetalhamentoReceita = detalhamentoReceita;
            Produto = produto;
            PeriodoReferencia = periodoReferencia;
            camposExtras = new List<CampoExtra>() { new CampoExtra((int)ETipoCampoExtraTO.ChaveAcessoNFe, ETipoCampoExtraTO.ChaveAcessoNFe.ToString(), chaveAcessoNFe) };
            Convenio = convenio.SanitizeString();
        }
    }
}
