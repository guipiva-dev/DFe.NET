﻿using DFe.Classes.Entidades;
using DFe.Utils.Extensoes;
using NFe.BLL.Configuracao.Entidades.Produtos;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using System;
using System.Collections.Generic;

namespace NFe.BLL.Configuracao.Entidades
{
    public class NotaFiscal
    {
        public NotaFiscal(
            int id,
            int serie,
            long numero,
            TipoNFe eTipoNFe,
            string naturezaOperacaoDescricao,
            DateTime dataEmissao,
            DateTime? dataSaida,
            FinalidadeNFe eFinalidadeNFe,
            Emitente emitente,
            Destinatario destinatario,
            Transporte dadosTransporte,
            Totalizador total,
            IReadOnlyList<Produto> produtos,
            IReadOnlyList<Duplicata> duplicatas,
            string dadosAdicionaisFisco,
            string dadosAdicionaisContribuinte,
            PresencaComprador ePresencaComprador,
            IReadOnlyList<Pagamento> formasPagamento = null,
            IReadOnlyList<DocumentoReferenciado> documentosReferenciados = null,
            string numeroPedidoCompraB2B = null,
            bool isZonaFrancaManaus = false)
        {
            var emitenteUf = emitente?.Pessoa?.Endereco?.MunicipioEstadoSigla.GetValueOrDefault();
            var destinatarioUf = destinatario?.Pessoa?.Endereco?.MunicipioEstadoSigla.GetValueOrDefault();

            Id = id;
            Serie = serie;
            Numero = numero;
            Emitente = emitente;
            NaturezaOperacaoDescricao = naturezaOperacaoDescricao.SanitizeString();
            DataEmissao = dataEmissao;
            DataSaida = dataSaida;
            ETipoNFe = eTipoNFe;
            Destinatario = destinatario;
            EPresencaComprador = ePresencaComprador;
            EFinalidadeNFe = eFinalidadeNFe;
            EDestinoOperacao = _obterDestinoOperacao(emitenteUf.GetValueOrDefault(), destinatarioUf);
            IsImportacao = destinatario != null && destinatarioUf.GetValueOrDefault() == Estado.EX && ETipoNFe == TipoNFe.tnEntrada;
            IsExportacao = destinatario != null && (destinatarioUf.GetValueOrDefault() == Estado.EX && ETipoNFe == TipoNFe.tnSaida);
            DadosTransporte = dadosTransporte;
            NumeroPedidoCompraB2B = numeroPedidoCompraB2B.SanitizeString();
            Total = total;
            Duplicatas = duplicatas;
            DadosAdicionaisFisco = dadosAdicionaisFisco.SanitizeString();
            DadosAdicionaisContribuinte = dadosAdicionaisContribuinte.SanitizeString();
            DocumentosReferenciados = documentosReferenciados;
            Produtos = produtos;
            FormasPagamento = formasPagamento;
            IsZonaFrancaManaus = isZonaFrancaManaus;
        }

        public int Id { get; private set; }

        public string ChaveAcesso { get; private set; }

        public int Serie { get; private set; }

        public long Numero { get; private set; }

        public Emitente Emitente { get; private set; }

        public string NaturezaOperacaoDescricao { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public DateTime? DataSaida { get; private set; }

        public TipoNFe ETipoNFe { get; private set; }

        public Destinatario Destinatario { get; private set; }

        public PresencaComprador EPresencaComprador { get; private set; }

        public FinalidadeNFe EFinalidadeNFe { get; private set; }

        public DestinoOperacao EDestinoOperacao { get; private set; }

        public bool IsImportacao { get; private set; }

        public bool IsExportacao { get; private set; }

        public Transporte DadosTransporte { get; private set; }

        public string NumeroPedidoCompraB2B { get; private set; }

        public Totalizador Total { get; private set; }

        public IReadOnlyList<Duplicata> Duplicatas { get; private set; }

        public string DadosAdicionaisFisco { get; private set; }

        public string DadosAdicionaisContribuinte { get; private set; }

        public IReadOnlyList<Pagamento> FormasPagamento { get; private set; }

        public IReadOnlyList<DocumentoReferenciado> DocumentosReferenciados { get; private set; }

        public IReadOnlyList<Produto> Produtos { get; private set; }

        public bool IsZonaFrancaManaus { get; private set; }

        private DestinoOperacao _obterDestinoOperacao(Estado estadoEmitente, Estado? estadoDestinatario)
        {
            if(estadoDestinatario == null
                || estadoEmitente == estadoDestinatario)
            {
                return DestinoOperacao.doInterna;
            }
            else if (estadoDestinatario == Estado.EX || estadoEmitente == Estado.EX)
            {
                return DestinoOperacao.doExterior;
            }
            else
            {
                return DestinoOperacao.doInterestadual;
            }
        }

        public void AtualizarChaveAcesso(string chaveAcesso)
        {
            ChaveAcesso = chaveAcesso;
        }
    }
}
