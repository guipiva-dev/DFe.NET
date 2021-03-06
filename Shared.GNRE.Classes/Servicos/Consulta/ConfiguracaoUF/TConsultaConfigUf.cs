﻿using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace GNRE.Classes.Servicos.Consulta.ConfiguracaoUF
{
    [XmlRoot(Namespace = "http://www.gnre.pe.gov.br", IsNullable = false)]
    public class TConsultaConfigUf
    {
        public TConsultaConfigUf(TipoAmbiente ambiente, Estado uf, TConsultaConfigUfReceita receita = null)
        {
            this.ambiente = ambiente;
            this.uf = uf;
            this.receita = receita;
        }

        internal TConsultaConfigUf()
        {

        }

        /// <summary>
        /// Identificação do ambiente: 1=Produção/2=Homologação 
        /// </summary>
        public TipoAmbiente ambiente { get; set; }

        /// <summary>
        /// Contém a sigla da UF favorecida.Campo com 2 dígitos.
        /// </summary>
        [XmlIgnore]
        public Estado uf { get; set; }

        [XmlElement(ElementName = "uf")]
        public string ProxyUF
        {
            get => Enum.GetName(typeof(Estado), uf);
            set => uf = (Estado)Enum.Parse(typeof(Estado), value);
        }

        /// <summary>
        /// Código da receita a ser consultada.
        /// Obs: informar o parâmetro courier caso seja a receita 100056 e deseja a configuração especifica para Empresa de Courier
        /// </summary>
        public TConsultaConfigUfReceita receita { get; set; }
    }
}
